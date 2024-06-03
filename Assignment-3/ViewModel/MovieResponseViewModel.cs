using System.ComponentModel.DataAnnotations;

namespace Assignment_3.Dtos
{
    public class MovieResponseViewModel
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }

        public MovieResponseViewModel(Guid movieId, string title, string director, string genre, decimal price)
        {
            MovieId = movieId;
            Title = title;
            Director = director;
            Genre = genre;
            Price = price;
        }
    }
}
