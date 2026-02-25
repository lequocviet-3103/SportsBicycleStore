using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.SearchFilter
{
    public class ProductSearchFilter
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? SellerId { get; set; }
        public string? CategoryId { get; set; }
        public string? BrandId { get; set; }
        public int? Status { get; set; }
        public int? Condition { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
