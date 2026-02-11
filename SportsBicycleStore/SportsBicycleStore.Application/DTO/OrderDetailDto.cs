using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class OrderDetailDto
    {
        public string ProductId { get; set; } = null!;

        public int? Quantity { get; set; }

        public string? Note { get; set; }
    }
}
