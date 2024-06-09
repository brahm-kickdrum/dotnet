namespace Assessment_1.Exceptions
{
    public class InvalidOtpException : Exception
    {
        public InvalidOtpException(string message) : base(message) { }
        public InvalidOtpException(string message, Exception innerException) : base(message, innerException) { }

    }
}
