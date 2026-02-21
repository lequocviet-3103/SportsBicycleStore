using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Domain.Enum
{
    public enum DisputeStatus
    {
        Open = 1,
        UnderReview = 2,
        WaitingForEvidence = 3,
        Resolved = 4,
        Closed = 5
    }
}
