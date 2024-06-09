namespace Assessment_1.Exceptions
{
    public class TripOperationException : Exception
    {
        public TripOperationException(string message) : base(message) { }
        public TripOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
