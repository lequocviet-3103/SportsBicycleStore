using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Domain.Enum
{
    public enum ProductStatus
    {
        Pending_inspection = 1,
        Available = 2,
        Inspection_failed = 3,
        Pending_seller_approval = 4,
        Sold = 5,
        Out_of_stock = 6,
    }
}
