using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class BrandDto
    {
        public string BrandName { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
