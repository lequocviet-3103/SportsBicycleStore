using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class DisputeDto
    {
        public string? OrderId { get; set; }

        public string? BuyerId { get; set; }

        public int Reason { get; set; }

        public string? Description { get; set; }

        public string? EvidenceUrls { get; set; }
    }
}
