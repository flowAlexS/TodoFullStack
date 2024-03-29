using Microsoft.AspNetCore.Identity;

namespace TodoApi.Models.Users
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string ProfilePictureName { get; set; } = string.Empty;
    }
}
