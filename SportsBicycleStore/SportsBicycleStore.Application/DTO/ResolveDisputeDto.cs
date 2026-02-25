using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class ResolveDisputeDto
    {
        public int Resolution { get; set; }

        public decimal? RefundAmount { get; set; }

        public string? AdminNote { get; set; }

        public string? ResolvedBy { get; set; }
    }
}
