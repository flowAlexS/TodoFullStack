namespace TodoApi.DTOs.Users
{
    public class CreateUserRequest
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Passsword { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public IFormFile? ProfilePicture { get; set; }
    }
}
