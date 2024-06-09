namespace Assessment_1.Exceptions
{
    public class FailedToAddUserException : Exception
    {
        public FailedToAddUserException(string message) : base(message) { }
        public FailedToAddUserException(string message, Exception innerException) : base(message, innerException) { }
    }
}
