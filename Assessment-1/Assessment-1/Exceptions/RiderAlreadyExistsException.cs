namespace Assessment_1.Exceptions
{
    public class RiderAlreadyExistsException : Exception
    {
        public RiderAlreadyExistsException(string message) : base(message) { }
        public RiderAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
