namespace Assignment_3.Exceptions
{
    public class FailedToAddMovieException : Exception
    {
        public FailedToAddMovieException(string message) : base(message) { }
        public FailedToAddMovieException(string message, Exception innerException): base(message, innerException) { }
    }
}
