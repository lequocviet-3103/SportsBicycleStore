using System;
using System.Collections.Generic;

namespace SportsBicycleStore.Domain.Entities;

public partial class Listing
{
    public string ListingId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? FeaturedImage { get; set; }

    public string? Status { get; set; }

    public string? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ExpiredAt { get; set; }

    public virtual User? ApprovedByNavigation { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User Seller { get; set; } = null!;

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
