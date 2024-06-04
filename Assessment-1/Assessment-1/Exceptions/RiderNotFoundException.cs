namespace Assessment_1.Exceptions
{
    public class RiderNotFoundException : Exception
    {
        public RiderNotFoundException(string message) : base(message) { }
        public RiderNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
