using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class PaymentDto
    {
        public string? OrderId { get; set; }

        //public int? PaymentMethod { get; set; }

        //public int? PaymentType { get; set; }

        public decimal Amount { get; set; }

        public string? TransactionCode { get; set; }

        public string? PaymentGateway { get; set; }

        //public int? PaymentStatus { get; set; }

        //public DateTime? PaidAt { get; set; }

        public string? Note { get; set; }

        //public DateTime? CreatedAt { get; set; }
    }
}
