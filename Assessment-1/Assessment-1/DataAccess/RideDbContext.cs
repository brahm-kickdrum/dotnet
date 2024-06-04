using Assessment_1.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment_1.DataAccess
{
    public class RideDbContext : DbContext
    {
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Ride> Rides { get; set; }

        public RideDbContext(DbContextOptions<RideDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rider>().ToTable("Riders");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<Ride>().ToTable("Rides");
        }
    }
}
