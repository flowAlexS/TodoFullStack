using Microsoft.AspNetCore.Identity;
using TodoApi.DTOs;
using TodoApi.Models.Users;

namespace TodoApi.Services.Users
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public bool CreateAccount(CreateUserRequest request)
        {
            // Check for duplicates..
            var duplicate = this._userManager.FindByEmailAsync(request.Email)
                        ?? this._userManager.FindByNameAsync(request.UserName);

            if (duplicate is null)
            {
                return false;
            }

            // If No Duplicate.. Create user...

            var user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            return true;
        }
    }
}
