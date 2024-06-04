namespace Assessment_1.Exceptions
{
    public class DriverAlreadyExistsException : Exception
    {
        public DriverAlreadyExistsException(string message) : base(message) { }
        public DriverAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
