using System;
using System.Collections.Generic;
using System.Text;

namespace SportsBicycleStore.Application.DTO
{
    public class UpdateUserDto
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? FullName { get; set; }

        public string? AvatarUrl { get; set; }

        public string? Address { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        public int? Gender { get; set; }

        public int? Status { get; set; }

        public string RoleId { get; set; } = null!;
    }
}
