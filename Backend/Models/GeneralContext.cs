using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Models.DTO;

namespace Learning.Models
{
    public class GeneralContext : DbContext
    {
        public GeneralContext(DbContextOptions<GeneralContext> options)
        : base(options)
        {
        }

        // Adding models to context
        public DbSet<User> Users { get; set; } = null!;   

        public DbSet<Backend.Models.DTO.Driver> Driver { get; set; }

        public DbSet<Backend.Models.Passenger> Passenger { get; set; }

        public DbSet<Backend.Models.Pool> Pool { get; set; }

    }
}
