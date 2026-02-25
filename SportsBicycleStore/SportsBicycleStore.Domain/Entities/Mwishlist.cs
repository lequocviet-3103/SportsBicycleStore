using System;
using System.Collections.Generic;

namespace SportsBicycleStore.Domain.Entities;

public partial class Mwishlist
{
    public string WishlistId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string? ListingId { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? DeleteFlag { get; set; }

    public virtual Mlisting? Listing { get; set; }

    public virtual Mproduct Product { get; set; } = null!;

    public virtual Muser User { get; set; } = null!;
}
