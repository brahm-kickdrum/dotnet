namespace Assessment_1.Exceptions
{
    public class FailedToAddBookingException : Exception
    {
        public FailedToAddBookingException(string message) : base(message) { }
        public FailedToAddBookingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
