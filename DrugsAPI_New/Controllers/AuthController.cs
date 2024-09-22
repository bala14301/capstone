using DrugsAPI_New.Models;
using DrugsAPI_New.Repositories;
using DrugsAPI_New.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DrugsAPI_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public AuthController(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (_userRepository.ValidateUser(model.Username, model.Password))
            {
                var user = _userRepository.GetUserByUsername(model.Username);
                var token = _authService.GenerateToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpPost("verify")]
        [AllowAnonymous]
        public IActionResult VerifyToken([FromBody] string token)
        {
            var isValid = _authService.ValidateToken(token);
            return Ok(new { IsValid = isValid });
        }

        [HttpPost("signup")]
        [AllowAnonymous] // Allow anonymous access for signup
        public IActionResult Signup([FromBody] SignupModel model)
        {
            if (_userRepository.GetUserByUsername(model.Username) != null)
            {
                return BadRequest("Username already exists");
            }

            var newUser = new User
            {
                Username = model.Username,
                Email = model.Email,
                MobileNo = model.MobileNo
            };

            var (isSuccess, error) = _authService.CreateUser(newUser, model.Password);

            if (isSuccess)
            {

                return Ok(new ResponseSignup()
                {
                    message = "User created successfully"
                });
            }
            else
            {
                return BadRequest(error);
            }
        }

        [HttpPost("logout")]
        [Authorize] // Require authorization for logout
        public IActionResult Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var isLoggedOut = _authService.InvalidateToken(token); // Use _authService

            if (isLoggedOut)
            {
                return Ok(new ResponseSignup()
                {
                    message = "User logged out successfully"
                });
            }

            return BadRequest("Logout failed");
        }
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class SignupModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string MobileNo { get; set; }
}

public class ResponseSignup
{
    public string message { get; set; }
}