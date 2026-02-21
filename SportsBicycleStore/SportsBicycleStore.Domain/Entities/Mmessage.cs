using System;
using System.Collections.Generic;

namespace SportsBicycleStore.Domain.Entities;

public partial class Mmessage
{
    public string MessageId { get; set; } = null!;

    public string SenderId { get; set; } = null!;

    public string ReceiverId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Muser Sender { get; set; } = null!;

    public virtual Muser Receiver { get; set; } = null!;
}
