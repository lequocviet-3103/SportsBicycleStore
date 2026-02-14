using Microsoft.AspNetCore.Http;
using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;


namespace SportsBicycleStore.Application.Interfaces.Services
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
        //Task<Mpayment> CreatePayment(PaymentDto paymentDto);

    }
}
