using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Domain.Enum
{
    public enum PaymentStatus
    {
        //Unpaid = 1,
        //Paid = 2,
        Pending = 1,
        Success = 2,
        Failed = 3,
        Refunded = 4
    }
}
