using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadSense.Api;
using RoadSense.Api.Models;
using RoadSense.Api.Services;
using System.Threading.Tasks;

namespace RoadSense.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AuthService _authService; // ✅ Inject AuthService

        public AuthController(AppDbContext context, AuthService authService)
        {
            _context = context;
            _authService = authService; // ✅ Assign AuthService
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                return BadRequest("User already exists.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor: 12);
            user.UserId = Guid.NewGuid().ToString();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "User registered successfully!" });
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, existingUser.Password))
            {
                return NotFound("User not found!");
            }

            var token = _authService.GenerateJwtToken(existingUser.Email, existingUser.Role);

            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
