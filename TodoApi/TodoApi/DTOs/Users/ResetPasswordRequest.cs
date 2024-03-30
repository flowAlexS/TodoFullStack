namespace TodoApi.DTOs.Users
{
    public class ResetPasswordRequest
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string NewPassword { get; set; } = string.Empty;

        public string ResetPasswordToken { get; set; } = string.Empty;
    }
}
