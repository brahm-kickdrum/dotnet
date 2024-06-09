namespace Assessment_1.Exceptions
{
    public class FailedToAddDriverException : Exception
    {
        public FailedToAddDriverException(string message) : base(message) { }
        public FailedToAddDriverException(string message, Exception innerException) : base(message, innerException) { }
    }
}
