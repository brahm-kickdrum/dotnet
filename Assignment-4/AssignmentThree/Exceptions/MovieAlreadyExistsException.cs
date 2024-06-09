namespace Assignment_3.Exceptions
{
    public class MovieAlreadyExistsException : Exception
    {
        public MovieAlreadyExistsException(string message) : base(message) { }
    }
}
