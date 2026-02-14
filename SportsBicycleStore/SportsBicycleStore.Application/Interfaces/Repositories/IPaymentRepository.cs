using SportsBicycleStore.Application.DTO;
using SportsBicycleStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        Mpayment CreatePayment(PaymentDto paymentDto);
    }
}
