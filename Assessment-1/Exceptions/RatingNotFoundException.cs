namespace Assessment_1.Exceptions
{
    public class RatingNotFoundException : Exception
    {
        public RatingNotFoundException(string message) : base(message) { }
        public RatingNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
