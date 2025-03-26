using Microsoft.EntityFrameworkCore;
using RoadSense.Api.Models;

namespace RoadSense.Api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Define your DbSet properties here (representing tables)
        public DbSet<LoginModel> Users { get; set; }
        public DbSet<ManageViolation> Violations { get; set; }
        public DbSet<UserModel> ManageUsers { get; set; }
    }
}