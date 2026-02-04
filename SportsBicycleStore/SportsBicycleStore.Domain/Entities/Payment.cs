using System;
using System.Collections.Generic;

namespace SportsBicycleStore.Domain.Entities;

public partial class Payment
{
    public string PaymentId { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public string? PaymentType { get; set; }

    public decimal Amount { get; set; }

    public string? TransactionCode { get; set; }

    public string? PaymentGateway { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime? RefundedAt { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;
}
