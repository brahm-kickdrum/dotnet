namespace Assessment_1.Exceptions
{
    public class TripNotFoundException : Exception
    {
        public TripNotFoundException(string message) : base(message) { }
        public TripNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
