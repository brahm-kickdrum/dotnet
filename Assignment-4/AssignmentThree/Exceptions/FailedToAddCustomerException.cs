namespace Assignment_3.Exceptions
{
    public class FailedToAddCustomerException : Exception
    {
        public FailedToAddCustomerException(string message) : base(message) { }
        public FailedToAddCustomerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
