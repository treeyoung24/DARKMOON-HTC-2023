using Backend.Models;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<Backend.Models.Passenger> Passenger { get; set; }

        public DbSet<Backend.Models.Pool> Pool { get; set; }

        public DbSet<Backend.Models.Routes> Routes { get; set; }

        public DbSet<Backend.Models.RouteOrder> RouteOrder { get; set; }

        public DbSet<Backend.Models.Driver> Driver { get; set; }

        public DbSet<Backend.Models.RequestJoin> RequestJoin { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RequestJoin>()
                .HasKey(rj => new { rj.PoolId, rj.MemId
            });

            modelBuilder.Entity<RouteOrder>()
                .HasKey(rj => new {
                    rj.Order,
                    rj.UserId,
                    rj.RouteId
                });

            modelBuilder.Entity<Passenger>()
                .HasKey(rj => new {
                    rj.PoolId,
                    rj.PassengerId,
                });

            modelBuilder.Entity<Driver>()
                .HasKey(rj => new {
                    rj.PoolId,
                    rj.DriverId,
                });

        }


    }
}
