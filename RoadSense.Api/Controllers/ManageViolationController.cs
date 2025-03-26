using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoadSense.Api.Models;
using RoadSense.Api.Services;
using System.Data.Entity;

namespace RoadSense.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class ManageViolationController : ControllerBase
    {
        private readonly AppDbContext _context;
        
        public ManageViolationController(AppDbContext context)
        {
            _context = context;
           
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Violator user)
        {
            var violator = new ManageViolation
            {
               Id = Guid.NewGuid().ToString(),
               Created = DateTime.UtcNow,
               Info = user.Info,
               PlateNumber = user.PlateNumber,
               Name = user.Name,
               Type = user.Type,
               Status = user.Status,
            };
 
            _context.Violations.Add(violator);
            
            await _context.SaveChangesAsync();
            return Ok(new { Message = $"{user.Name} successfully added!" });
        }

        [HttpGet("List")]
        public IActionResult GetList(
            [FromQuery] int skip = 0,
            [FromQuery] int take = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? status = null)
        {
            var query = _context.Violations.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(v => v.Name.Contains(search)
                                      || v.PlateNumber.Contains(search)
                                      || v.Type.Contains(search));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(v => v.Status == status);
            }

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
        public async Task<IActionResult> Update(string id, [FromBody] Violator user)
        {
            var violator = await _context.Violations.FindAsync(id);
            if (violator == null)
            {
                return NotFound(new { Message = "Violator not found" });
            }

            violator.Info = user.Info;
            violator.PlateNumber = user.PlateNumber;
            violator.Name = user.Name;
            violator.Type = user.Type;
            violator.Status = user.Status;

            _context.Violations.Update(violator);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"{user.Name} successfully updated!" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var violator = await _context.Violations.FindAsync(id);
            if (violator == null)
            {
                return NotFound(new { Message = "Violator not found" });
            }

            _context.Violations.Remove(violator);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"{violator.Name} successfully deleted!" });
        }

    }
    public class Violator
    {
        public string? Info { get; set; }
        public string? PlateNumber { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
    }
}
