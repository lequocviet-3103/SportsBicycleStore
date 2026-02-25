using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class GetAllListing
    {
        public string ListingId { get; set; } = null!;

        public string ProductId { get; set; } = null!;
        public string? ProductName { get; set; }

        public string SellerId { get; set; } = null!;
        public string? SellerName { get; set; }

        public string Title { get; set; } = null!;

        public string? FeaturedImage { get; set; }

        public int? Status { get; set; }

        public string? ApprovedBy { get; set; }
        public string? ApprovedByName { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
