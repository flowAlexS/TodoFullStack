namespace TodoApi.DTOs.Users
{
    public class ForgotPasswordRequest
    {
        public string? Email { get; set; }

        public string? UserName { get; set; }
    }
}
