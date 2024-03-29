using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Minio;
using TodoApi.DTOs.Users;
using TodoApi.Models.Users;
using TodoApi.Services.MinioService;
using TodoApi.Services.Users;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMinioService _minioService;
        private readonly IMinioClient _minioClient;

        public UsersController(IAccountService accountService, UserManager<AppUser> userManager, IMinioService minioService, IMinioClient minioClient)
        {
            _accountService = accountService;
            _userManager = userManager;
            _minioService = minioService;
            _minioClient = minioClient;
        }

        // Create an account...

        // Steps check the email and username

        // Send conf email...

        // All good then create user.. 
        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromForm] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await this._accountService.CreateAccount(request);

            return Ok(response);
        }
    }
}
