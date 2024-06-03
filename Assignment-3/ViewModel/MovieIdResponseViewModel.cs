namespace Assignment_3.Dtos
{
    public class MovieIdResponseViewModel
    {
        public Guid MovieId { get; set; }

        public MovieIdResponseViewModel(Guid id)
        {
            MovieId = id;
        }
    }
}
