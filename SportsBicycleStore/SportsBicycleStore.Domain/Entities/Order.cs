using System;
using System.Collections.Generic;

namespace SportsBicycleStore.Domain.Entities;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public string BuyerId { get; set; } = null!;

    public string SellerId { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string? ShippingAddress { get; set; }

    public string? ReceiverName { get; set; }

    public string? ReceiverPhone { get; set; }

    public string? DeliveryMethod { get; set; }

    public string? OrderStatus { get; set; }

    public string? PaymentStatus { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? ConfirmedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? CancelledAt { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User Seller { get; set; } = null!;
}
