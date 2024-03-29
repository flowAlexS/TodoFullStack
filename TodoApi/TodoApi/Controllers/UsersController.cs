using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs.Users;
using TodoApi.Services.Users;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        => _accountService = accountService;

        // Create an account...

        // Steps check the email and username

        // Send conf email...

        // All good then create user.. 
        [HttpPost("/register")]
        public IActionResult Register([FromForm] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = this._accountService.CreateAccount(request);

            return Ok(response);
        }
    }
}
