using Assignment_3.DataAccess;
using Assignment_3.Entities;
using Assignment_3.Exceptions;
using Assignment_3.Mappers;
using Assignment_3.Repository.IRepository;
using Assignment_3.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3.Services
{
    public class RentalService : IRentalService
    {
        private IMovieService _movieService;
        private ICustomerService _customerService;  
        private IRentalRepository _rentalRepository;

        public RentalService(IMovieService movieService, ICustomerService customerService, IRentalRepository rentalRepository)
        {
            _movieService = movieService;
            _customerService = customerService;
            _rentalRepository = rentalRepository;
        }

        public Guid RentMovieById(Guid movieId, Guid customerId)
        {
            Movie? movie = _movieService.GetMovieById(movieId);

            if (movie == null)
            {
                throw new MovieNotFoundException($"Movie does not exist");
            }

            Customer? customer = _customerService.GetCustomerById(customerId);

            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer does not exist.");
            }

            if (_rentalRepository.RentalExists(movieId, customerId))
            {
                throw new RentalAlreadyExistsException("A rental with the same Customer Id and Movie Id already exists.");
            }

            Rental rental = new Rental(movieId, customerId);

            try
            {
                return _rentalRepository.AddRental(rental);
            }
            catch (Exception ex)
            {
                throw new FailedToAddRentalException("Failed to rent movie. An error occurred.", ex);
            }
        }

        public Guid RentMovieByMovieTitleAndUsername(string movieTitle, string username)
        {
            Movie? movie = _movieService.GetMovieByTitle(movieTitle);

            if (movie == null)
            {
                throw new MovieNotFoundException($"Movie with title {movieTitle} does not exist");
            }

            Customer? customer = _customerService.GetCustomerByUsername(username);

            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer with username {username} does not exist.");
            }

            if (_rentalRepository.RentalExists(movie.MovieId, customer.CustomerId))
            {
                throw new RentalAlreadyExistsException("A rental with the same CustomerId and MovieId already exists.");
            }

            return RentMovieById(movie.MovieId, customer.CustomerId);
        }

        public List<string> GetCustomersByMovieId(Guid movieId)
        {
            Movie? movie = _movieService.GetMovieById(movieId);

            if (movie == null)
            {
                throw new MovieNotFoundException($"Movie does not exist");
            }

            List<Guid> customerIdList = GetCustomerIdsByMovieId(movieId);

            return GetCustomerUsernamesByCustomerIds(customerIdList);
        }

        public List<Guid> GetCustomerIdsByMovieId(Guid movieId)
        {
            return _rentalRepository.GetCustomerIdsByMovieId(movieId);
        }

        public List<string> GetCustomerUsernamesByCustomerIds(List<Guid> customerIds)
        {
            return _rentalRepository.GetCustomerUsernamesByCustomerIds(customerIds);
        }

        public List<string> GetMoviesByCustomerId(Guid customerId)
        {
            Customer? customer = _customerService.GetCustomerById(customerId);

            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer does not exist.");
            }

            List<Guid> rentalMovieIdList = GetMovieIdsByCustomerId(customerId);

            return GetMovieTitlesByMovieIds(rentalMovieIdList);
        }

        public List<Guid> GetMovieIdsByCustomerId(Guid customerId)
        {
            return _rentalRepository.GetMovieIdsByCustomerId(customerId);
        }

        public List<string> GetMovieTitlesByMovieIds(List<Guid> movieIds)
        {
            return _rentalRepository.GetMovieTitlesByMovieIds(movieIds);
        }

        public decimal GetTotalCostByCustomerId(Guid customerId)
        {
            Customer? customer = _customerService.GetCustomerById(customerId);

            if (customer == null)
            {
                throw new CustomerNotFoundException($"Customer does not exist.");
            }

            decimal totalCost = _rentalRepository.CalculateTotalRentalCost(customerId);

            return totalCost;
        }
    }
}
