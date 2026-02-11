using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class VnPaymentResponseModel
    {
        public string? PaymentMethod { get; set; } 
        public string? OrderDescription { get; set; }
        public string? OrderId { get; set; }
        public string? PaymentId { get; set; }
        public string? TransactionId { get; set; }
        public string? Token { get; set; }
        public string? VnPayResponseCode { get; set; }
    }
}
