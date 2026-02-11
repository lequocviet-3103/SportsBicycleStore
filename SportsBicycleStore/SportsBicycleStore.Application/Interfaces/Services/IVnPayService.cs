using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.Interfaces.Services
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(string orderId, decimal amount, string returnUrl);
    }
}
