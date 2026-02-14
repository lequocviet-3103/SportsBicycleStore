using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Domain.Enum
{
    public enum OrderStatus
    {
        Pending = 1,
        Confirmed = 2,
        Paid = 3,
        Completed = 4,
        Cancelled = 5,
        Refunded = 6
    }
}
