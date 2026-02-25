using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Domain.Enum
{
    public enum DisputeResolution
    {
        FullRefund = 1,
        PartialRefund = 2,
        ReturnAndRefund = 3,
        Rejected = 4,
        MutualAgreement = 5
    }
}
