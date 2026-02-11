using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class OrderDto
    {
        public string? BuyerId { get; set; }

        public string? SellerId { get; set; }

        public string? ShippingAddress { get; set; }

        public string? ReceiverName { get; set; }

        public string? ReceiverPhone { get; set; }

        public string? Note { get; set; }
        public List<OrderDetailDto> Products { get; set; } = new();
    }
}
