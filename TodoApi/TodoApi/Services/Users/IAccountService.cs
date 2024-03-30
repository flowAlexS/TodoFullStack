using TodoApi.DTOs.Users;

namespace TodoApi.Services.Users
{
    public interface IAccountService
    {
        Task<CreateUserResponse?> CreateAccount(CreateUserRequest request);

        Task<bool> ConfirmEmail(ConfirmEmailRequest request);

        Task DeleteUser(string email);

        Task<ForgotPasswordResponse?> GetForgotPasswordToken(ForgotPasswordRequest request);

        Task<ResetPasswordResponse?> ResetPassword(ResetPasswordRequest request);
    }
}
