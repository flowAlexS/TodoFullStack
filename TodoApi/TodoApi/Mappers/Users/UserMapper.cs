using System.Runtime.CompilerServices;
using TodoApi.DTOs.Users;
using TodoApi.Models.Users;

namespace TodoApi.Mappers.Users
{
    public static class UserMapper
    {
        public static CreateUserResponse ToCreateUserResponse(this AppUser request, string registrationToken)
        => new ()
        {
            UserName = request.UserName ?? string.Empty,
            Email = request.Email ?? string.Empty,
            FirstName = request.FirstName,
            LastName = request.LastName,
            RegistrationToken = registrationToken
        };
    }
}
