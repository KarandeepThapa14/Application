using dotnet_app.Models.Dto;
using dotnet_app.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using dotnet_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace dotnet_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
         private readonly UserManager<ApplicationUser> _userManager;

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result == "User registered") return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var token = await _authService.LoginAsync(model);
            if (token == null) return Unauthorized("Invalid credentials");
            return Ok(new { token });
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userManager.Users.Select(c => new
            {
                c.Id,
                c.UserName,
                c.Email,
                c.PasswordHash
            }
            ).ToListAsync();

            return Ok(user);
        }
    }
}
