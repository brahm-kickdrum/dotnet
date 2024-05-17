namespace Assignment_3.Exceptions
{
    public class MovieNotFoundException : Exception
    {
        public MovieNotFoundException(string message) : base(message) { } 
    }
}
