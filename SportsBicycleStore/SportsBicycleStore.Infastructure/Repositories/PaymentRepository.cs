using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Application.Interfaces.Repositories;
using SportsBicycleStore.Domain.Entities;
using SportsBicycleStore.Domain.Enum;
using SportsBicycleStore.Infastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Infastructure.Repositories
{
    public class PaymentRepository : Repository<Mpayment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context) : base(context)
        {
        }

        public Mpayment CreatePayment(PaymentDto paymentDto)
        {
            var payment = new Mpayment
            {
                PaymentId = Guid.NewGuid().ToString(),
                OrderId = paymentDto.OrderId!,
                PaymentMethod = (int)PaymentMethod.Cash,
                PaymentType = (int)2,
                Amount = paymentDto.Amount,
                TransactionCode = paymentDto.TransactionCode,
                PaymentGateway = paymentDto.PaymentGateway,
                PaymentStatus = (int)PaymentStatus.Success,
                PaidAt = DateTime.Now,
                Note = paymentDto.Note,
                CreatedAt = DateTime.Now
            };
             _context.Mpayments.Add(payment);
             _context.SaveChanges();
            return payment;
        }
    }
}
