namespace TodoApi.DTOs.Users
{
    public class CreateUserResponse
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string RegistrationToken { get; set; } = string.Empty;
    }
}
