using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class CategoryDto
    {
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
