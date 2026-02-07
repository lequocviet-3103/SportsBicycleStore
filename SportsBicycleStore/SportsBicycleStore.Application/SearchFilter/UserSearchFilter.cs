namespace SportsBicycleStore.Application.SearchFilter
{
    public class UserSearchFilter
    {
        public string? UserId { get; set; } 

        public string? UserName { get; set; } 

        public string? Email { get; set; } 

        public string? PhoneNumber { get; set; }

        public string? FullName { get; set; }

        public string? Address { get; set; }

        //public DateOnly? DateOfBirth { get; set; }

        public string? RoleId { get; set; } 
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
