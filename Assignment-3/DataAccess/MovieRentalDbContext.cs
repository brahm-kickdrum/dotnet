using Assignment_3.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3.DataAccess
{
    public class MovieRentalDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public MovieRentalDbContext(DbContextOptions<MovieRentalDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Rental>().ToTable("Rentals");

        }
    }
}
