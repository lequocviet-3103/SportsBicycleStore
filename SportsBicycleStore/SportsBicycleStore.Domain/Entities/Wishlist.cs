using System;
using System.Collections.Generic;

namespace SportsBicycleStore.Domain.Entities;

public partial class Wishlist
{
    public string WishlistId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string? ListingId { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Listing? Listing { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
