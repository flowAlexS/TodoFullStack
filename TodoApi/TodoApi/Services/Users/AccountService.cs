using Microsoft.AspNetCore.Identity;
using TodoApi.DTOs.Users;
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

        public async Task<string?> CreateAccount(CreateUserRequest request)
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

            await this._userManager.CreateAsync(user, request.Passsword);
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
    }
}
