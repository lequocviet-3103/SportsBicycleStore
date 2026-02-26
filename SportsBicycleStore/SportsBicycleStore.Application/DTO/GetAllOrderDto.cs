using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class GetAllOrderDto
    {
        public string OrderId { get; set; } = null!;

        public string BuyerId { get; set; } = null!;

        public string SellerId { get; set; } = null!;

        public decimal TotalAmount { get; set; }

        public string? ShippingAddress { get; set; }

        public string? ReceiverName { get; set; }

        public string? ReceiverPhone { get; set; }

        public int? DeliveryMethod { get; set; }

        public int? OrderStatus { get; set; }

        public int? PaymentStatus { get; set; }

        public string? Note { get; set; }

        public DateTime? CreatedAt { get; set; }
        public string ProductId { get; set; } = null!;
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public string OrderDetailId { get; set; } = null!;

        public int? Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Subtotal { get; set; }
        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;
        public string? FullName { get; set; }

    }
}
