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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasData(
        //        new User
        //        {
        //UserId = 1,
        //            Name = "William Shakespeare",
        //            Email = "williamShakespeare@gmail.com",
        //            Phone = "4031234567",
        //            Address = "9 Avenue Southwest, Calgary, AB",
        //            Gender = "male",
        //            Age = 20
        //        }
        //    );
        //}


    }
}
