using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class DisputeEvidenceDto
    {
        public string? DisputeId { get; set; }

        public string? SubmittedBy { get; set; }

        public string? ImageUrl { get; set; }

        public string? VideoUrl { get; set; }

        public string? Description { get; set; }
    }
}
