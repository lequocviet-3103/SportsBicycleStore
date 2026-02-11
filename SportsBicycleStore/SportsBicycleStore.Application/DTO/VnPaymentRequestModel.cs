using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class VnPaymentRequestModel
    {
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
    }
}
