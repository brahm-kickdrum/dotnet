namespace Assessment_1.Exceptions
{
    public class DriverNotFoundException : Exception
    {
        public DriverNotFoundException(string message) : base(message) { }
        public DriverNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
