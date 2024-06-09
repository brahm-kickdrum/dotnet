namespace Assignment_3.Exceptions
{
    public class FailedToAddRentalException : Exception
    {
        public FailedToAddRentalException(string message) : base(message) { }
        public FailedToAddRentalException(string message, Exception innerException) : base(message, innerException) { }
    }
}
