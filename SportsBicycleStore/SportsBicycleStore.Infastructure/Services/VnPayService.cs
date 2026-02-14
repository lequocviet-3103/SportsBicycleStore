using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Application.Interfaces.Services;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
using SportsBicycleStore.Libraries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportsBicycleStore.Infastructure.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public VnPayService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }


        public async Task<string> CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var order = await _unitOfWork.MOrderRepository.GetOrderByIdAsync(model.OrderId);
            if (order == null)
                throw new Exception("Order not found");


            var pay = new VnPayLibrary();

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((decimal)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{model.FullName} {model.Description} {model.Amount}");
            pay.AddRequestData("vnp_OrderType", "other");
            pay.AddRequestData("vnp_ReturnUrl", _configuration["Vnpay:PaymentBackReturnUrl"]);
            pay.AddRequestData("vnp_TxnRef", order.OrderId);

            string paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;

        }

        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            try
            {
                var vnpay = new VnPayLibrary();
                
                foreach (var (key, value) in collections)
                {
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(key, value);
                    }
                }
                var response = vnpay.GetFullResponseData(
                       collections,
                       _configuration["Vnpay:HashSecret"]);
                var vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100; // QUAN TRỌNG: chia 100
                var vnp_TransactionId = vnpay.GetResponseData("vnp_TransactionNo");
                var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                var vnp_SecureHash = collections["vnp_SecureHash"];
                
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _configuration["Vnpay:HashSecret"]);
                
                if (!checkSignature)
                {
                    return new PaymentResponseModel
                    {
                        Success = false,
                    };
                }




                var paymentDto = new PaymentDto
                {
                    OrderId = response.OrderId,
                    Amount = vnp_Amount,
                    TransactionCode = response.TransactionId,
                    PaymentGateway = "VNPay",
                    Note = "Thanh toán thành công"
                };

                 _unitOfWork.PaymentRepository.CreatePayment(paymentDto);

                 _unitOfWork.MOrderRepository.UpdateOrderStatus(response.OrderId, OrderStatus.Paid);
                 _unitOfWork.MOrderRepository.UpdateOrderPaymentStatus(response.OrderId, OrderPaymentStatus.Paid);

                return new PaymentResponseModel
                {
                    Success = vnp_ResponseCode == "00",
                    PaymentMethod = "VnPay",
                    Amount = (decimal)vnp_Amount, // Đã chia 100
                    TransactionId = vnp_TransactionId,
                    VnPayResponseCode = vnp_ResponseCode,
                    Message = vnp_ResponseCode == "00" ? "Payment successful" : "Payment failed"
                };
            }
            catch (Exception ex)
            {
                return new PaymentResponseModel
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
