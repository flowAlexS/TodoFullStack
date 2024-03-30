namespace TodoApi.DTOs.Users
{
    public class ForgotPasswordResponse
    {
        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string ResetPasswordToken { get; set; } = string.Empty;
    }
}
