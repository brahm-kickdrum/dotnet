using Assignment_3.Entities;

namespace Assignment_3.Repository.IRepository
{
    public interface IRentalRepository
    {
        bool RentalExists(Guid movieId, Guid customerId);

        Guid AddRental(Rental rental);

        List<Guid> GetCustomerIdsByMovieId(Guid movieId);

        List<string> GetCustomerUsernamesByCustomerIds(List<Guid> customerIds);

        List<Guid> GetMovieIdsByCustomerId(Guid customerId);

        List<string> GetMovieTitlesByMovieIds(List<Guid> movieIds);

        decimal CalculateTotalRentalCost(Guid customerId);
    }
}
