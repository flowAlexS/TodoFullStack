using TodoApi.DTOs;

namespace TodoApi.Services.Users
{
    public interface IAccountService
    {
        bool CreateAccount(CreateUserRequest request); // Need a create account dto...
    }
}
