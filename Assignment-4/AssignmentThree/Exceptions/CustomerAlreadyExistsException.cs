namespace Assignment_3.Exceptions
{
    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException(string message) : base(message) { }
    }
}
