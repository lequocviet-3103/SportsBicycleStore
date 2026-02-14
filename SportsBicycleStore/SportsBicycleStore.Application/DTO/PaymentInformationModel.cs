using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class PaymentInformationModel
    {
        public string? OrderId { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
