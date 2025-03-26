using Microsoft.AspNetCore.Mvc;
using RoadSense.Api.Models;

namespace RoadSense.Api.Controllers
{
    [Route("api/auth/user")]
    [ApiController]
    public class ManageUserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ManageUserController(AppDbContext context)
        {
            _context = context;

        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Invalid user data");

             
            var violator = new UserModel
            {
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.UtcNow,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                UserId = user.UserId,
                PlateNumber = user.PlateNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Suffix = user.Suffix,
                UserName = user.UserName,
            };

            _context.ManageUsers.Add(violator);

            await _context.SaveChangesAsync();
            return Ok(new { Message = $"{user.UserName} successfully added!" });
        }
       

        [HttpGet("list")]
        public IActionResult GetList(
            [FromQuery] int skip = 0,
            [FromQuery] int take = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? status = null)
        {
            var query = _context.ManageUsers.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(v => v.UserName.Contains(search)
                                      || v.UserId.Contains(search)
                                      || v.Email.Contains(search)
                                      || v.PlateNumber.Contains(search)
                                      || v.FirstName.Contains(search)
                                      || v.LastName.Contains(search)
                                      || v.MiddleName.Contains(search)
                                      || v.Suffix.Contains(search));
            }

            //if (!string.IsNullOrEmpty(status))
            //{
            //    query = query.Where(v => v.Status == status);
            //}

            // Execute query synchronously to avoid IDbAsyncEnumerable issues
            var totalRecords = query.Count();
            var paginatedResults = query.Skip(skip).Take(take).ToList();

            return Ok(new
            {
                TotalRecords = totalRecords,
                Data = paginatedResults
            });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] User user)
        {
            var users = await _context.ManageUsers.FindAsync(id);
            if (users == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            users.UserId = user.UserId;
            users.UserName = user.UserName;
            users.Password = user.Password;
            users.Email = user.Email;
            users.PlateNumber = user.PlateNumber;
            users.PhoneNumber = user.PhoneNumber;
            users.FirstName = user.FirstName;
            users.LastName = user.LastName;
            users.MiddleName = user.MiddleName;
            users.Suffix = user.Suffix;
            users.Role = user.Role;

            _context.ManageUsers.Update(users);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"{user.UserName} successfully updated!" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.ManageUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            _context.ManageUsers.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"{user.UserName} successfully deleted!" });
        }

    }

    public class User
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PlateNumber { get; set; }
        public string? Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Suffix { get; set; }
    }
     
}