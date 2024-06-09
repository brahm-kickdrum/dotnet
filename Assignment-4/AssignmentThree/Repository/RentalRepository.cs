using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly MovieRentalDbContext _dbContext;

        public RentalRepository(MovieRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool RentalExists(Guid movieId, Guid customerId)
        {
            return _dbContext.Rentals
                            .Any(r => r.CustomerId == customerId && r.MovieId == movieId);
        }

        public Guid AddRental(Rental rental)
        {
            _dbContext.Rentals.Add(rental);
            _dbContext.SaveChanges();
            return rental.RentalId;
        }

        public List<Guid> GetCustomerIdsByMovieId(Guid movieId)
        {
            return _dbContext.Rentals
                            .Where(r => r.MovieId == movieId)
                            .Select(r => r.CustomerId)
                            .ToList();
        }

        public List<string> GetCustomerUsernamesByCustomerIds(List<Guid> customerIds)
        {
            return _dbContext.Customers
                            .Where(c => customerIds.Contains(c.CustomerId))
                            .Select(c => c.Username)
                            .ToList();
        }

        public List<Guid> GetMovieIdsByCustomerId(Guid customerId)
        {
            return _dbContext.Rentals
                            .Where(r => r.CustomerId == customerId)
                            .Select(r => r.MovieId)
                            .ToList();
        }

        public List<string> GetMovieTitlesByMovieIds(List<Guid> movieIds)
        {
            return _dbContext.Movies
                            .Where(m => movieIds.Contains(m.MovieId))
                            .Select(m => m.Title)
                            .ToList();
        }

        public decimal CalculateTotalRentalCost(Guid customerId)
        {
            return _dbContext.Rentals
                             .Where(r => r.CustomerId == customerId)
                             .Join(_dbContext.Movies,
                                   rental => rental.MovieId,
                                   movie => movie.MovieId,
                                   (rental, movie) => movie.Price)
                             .DefaultIfEmpty()
                             .Sum();
        }
    }
}
