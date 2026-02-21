using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Domain.Enum
{
    public enum DisputeReason
    {
        NotAsDescribed = 1,
        DamagedInShipping = 2,
        MissingAccessories = 3,
        DifferentFromInspection = 4,
        CounterfeitProduct = 5,
        Other = 99
    }
}
