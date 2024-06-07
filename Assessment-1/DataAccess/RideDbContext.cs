using Assessment_1.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment_1.DataAccess
{
    public class RideDbContext : DbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public RideDbContext(DbContextOptions<RideDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Trip>().ToTable("Trips");
            modelBuilder.Entity<Rating>().ToTable("Ratings");
            modelBuilder.Entity<Booking>().ToTable("Booking");

            modelBuilder.Entity<User>()
               .HasIndex(u => new { u.Email, u.Role })
               .IsUnique();

            modelBuilder.Entity<User>()
               .HasIndex(u => new { u.Phone, u.Role })
               .IsUnique();

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.TripId) 
                .IsUnique();
        }
    }
}
