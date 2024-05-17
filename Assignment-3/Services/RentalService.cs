using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3.Services
{
    public class RentalService
    {
        private readonly MovieRentalDbContext _dbContext;

        public RentalService(MovieRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid RentMovieById(Guid movieId, Guid customerId)
        {
            Movie? movie = _dbContext.Movies
                                    .FirstOrDefault(m => m.MovieId == movieId);

            if (movie == null)
            {
                throw new MovieNotFoundException($"Movie does not exist");
            }

            Customer? customer = _dbContext
                .Customers
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer does not exist.");
            }

            if (_dbContext.Rentals.Any(r => r.CustomerId == customerId && r.MovieId == movieId))
            {
                throw new RentalAlreadyExistsException("A rental with the same Customer Id and Movie Id already exists.");
            }

            Rental rental = new Rental(movieId, customerId);

            try
            {
                _dbContext.Rentals.Add(rental);
                _dbContext.SaveChanges();
                return rental.RentalId;
            }
            catch (Exception ex)
            {
                throw new FailedToAddRentalException("Failed to rent movie. An error occurred.", ex);
            }
        }

        public Guid RentMovieByMovieTitleAndUsername(string movieTitle, string username)
        {
            Movie? movie = _dbContext.Movies
                                    .FirstOrDefault(m => m.Title == movieTitle);

            if (movie == null)
            {
                throw new MovieNotFoundException($"Movie with title {movieTitle} does not exist");
            }

            Customer? customer = _dbContext
                .Customers
                .FirstOrDefault(c => c.Username == username);

            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer with username {username} does not exist.");
            }

            if (_dbContext.Rentals.Any(r => r.CustomerId == customer.CustomerId && r.MovieId == movie.MovieId))
            {
                throw new RentalAlreadyExistsException("A rental with the same CustomerId and MovieId already exists.");
            }

            return RentMovieById(movie.MovieId, customer.CustomerId);
        }

        public List<string> GetCustomersByMovieId(Guid movieId)
        {
            Movie? movie = _dbContext.Movies
                                   .FirstOrDefault(m => m.MovieId == movieId);

            if (movie == null)
            {
                throw new MovieNotFoundException($"Movie does not exist");
            }

            List<Guid> customerIdList = GetCustomerIdsByMovieId(movieId);

            return GetCustomerUsernamesByCustomerIds(customerIdList);
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

        public List<string> GetMoviesByCustomerId(Guid customerId)
        {
            Customer? customer = _dbContext
                .Customers
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer does not exist.");
            }

            List<Guid> rentalMovieIdList = GetMovieIdsByCustomerId(customerId);

            return GetMovieTitlesByMovieIds(rentalMovieIdList);
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

        public decimal GetTotalCostByCustomerId(Guid customerId)
        {
            Customer? customer = _dbContext
               .Customers
               .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer does not exist.");
            }

            decimal totalCost = _dbContext.Rentals
                                       .Where(r => r.CustomerId == customerId)
                                       .Join(_dbContext.Movies, 
                                             rental => rental.MovieId,
                                             movie => movie.MovieId,
                                             (rental, movie) => movie.Price)
                                       .DefaultIfEmpty() 
                                       .Sum();

            return totalCost;
        }
    }
}
