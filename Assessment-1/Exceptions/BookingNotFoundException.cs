namespace Assessment_1.Exceptions
{
    public class BookingNotFoundException : Exception
    {
        public BookingNotFoundException(string message) : base(message) { }
        public BookingNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
