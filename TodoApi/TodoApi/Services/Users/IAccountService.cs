using TodoApi.DTOs.Users;

namespace TodoApi.Services.Users
{
    public interface IAccountService
    {
        Task<CreateUserResponse?> CreateAccount(CreateUserRequest request); // Need a create account dto...
    }
}
