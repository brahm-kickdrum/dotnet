namespace Assignment_3.Services.IServices
{
    public interface IRentalService
    {
        Guid RentMovieById(Guid movieId, Guid customerId);
        Guid RentMovieByMovieTitleAndUsername(string movieTitle, string username);
        List<string> GetCustomersByMovieId(Guid movieId);
        List<Guid> GetCustomerIdsByMovieId(Guid movieId);
        List<string> GetCustomerUsernamesByCustomerIds(List<Guid> customerIds);
        List<string> GetMoviesByCustomerId(Guid customerId);
        List<Guid> GetMovieIdsByCustomerId(Guid customerId);
        List<string> GetMovieTitlesByMovieIds(List<Guid> movieIds);
        decimal GetTotalCostByCustomerId(Guid customerId);
    }
}
