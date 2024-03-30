using Microsoft.AspNetCore.Identity;
using TodoApi.DTOs.Users;
using TodoApi.Mappers.Users;
using TodoApi.Models.Users;
using TodoApi.Services.MinioService;

namespace TodoApi.Services.Users
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMinioService _minioService;

        public AccountService(UserManager<AppUser> userManager, IMinioService minioService)
        {
            this._userManager = userManager;
            this._minioService = minioService;
        }

        public async Task<bool> ConfirmEmail(ConfirmEmailRequest request)
        {
            var user = await this._userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return false;
            }

            var response = await this._userManager.ConfirmEmailAsync(user, request.ConfirmationToken);

            return response.Succeeded;
        }

        public async Task<CreateUserResponse?> CreateAccount(CreateUserRequest request)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null ||
                await _userManager.FindByNameAsync(request.UserName) != null)
            {
                return null;    
            }

            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                ProfilePictureName = request.ProfilePicture is null ? "" : await _minioService.UploadFile(request.ProfilePicture),
                TwoFactorEnabled = true
            };

            var response = await this._userManager.CreateAsync(user, request.Passsword);

            if (response.Succeeded)
            {
                return user.ToCreateUserResponse(await _userManager.GenerateEmailConfirmationTokenAsync(user));
            }

            return null;
        }

        public async Task DeleteUser(string email)
        {
            var user = await this._userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                await this._userManager.DeleteAsync(user);
            }
        }

        public async Task<ForgotPasswordResponse?> GetForgotPasswordToken(ForgotPasswordRequest request)
        {
            if (request.Email is null && request.UserName is null)
            {
                return null;
            }

            var user = request.Email is null
                ? await this._userManager.FindByNameAsync(request.UserName!)
                : await this._userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return null;
            }

            var response = await this._userManager.GeneratePasswordResetTokenAsync(user);

            return new ForgotPasswordResponse()
            {
                Email = user.Email ?? string.Empty,
                UserName = user.UserName ?? string.Empty,
                ResetPasswordToken = response,
            };
        }

        public async Task<ResetPasswordResponse?> ResetPassword(ResetPasswordRequest request)
        {
            if (request.Email is null && request.UserName is null)
            {
                return null;
            }

            var user = request.Email is null
                ? await this._userManager.FindByNameAsync(request.UserName!)
                : await this._userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return null;
            }

            var response = await this._userManager.ResetPasswordAsync(user, request.ResetPasswordToken, request.NewPassword);

            return response.Succeeded
                ? new ResetPasswordResponse()
                {
                    Email = user.Email ?? string.Empty,
                    UserName = user.UserName ?? string.Empty
                }
                : null;
        }
    }
}
