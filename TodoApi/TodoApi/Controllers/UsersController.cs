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

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("/confirm")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await this._accountService.ConfirmEmail(request);

            if (response)
            {
                return Ok(); // We might need to implement smth else here
            }

            return BadRequest(response); // We might want to return the error here...
        }


        [HttpPost("/forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await this._accountService.GetForgotPasswordToken(request);

            return response is null
                ? BadRequest()
                : Ok(response);
        }

        // Work on errors later..
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

        [HttpPost("/resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await this._accountService.ResetPassword(request);

            return response is null
                ? BadRequest()
                : Ok(response);
        }

        // Will require authorization...
        [HttpDelete("/delete/{email}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string email)
        {
            await this._accountService.DeleteUser(email);
            return NoContent();
        }

        // Put Delete (Update/Delete Account) -> Authorisation required...
    }
}
