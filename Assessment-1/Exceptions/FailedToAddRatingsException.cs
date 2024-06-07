namespace Assessment_1.Exceptions
{
    public class FailedToAddRatingsException : Exception
    {
        public FailedToAddRatingsException(string message) : base(message) { }
        public FailedToAddRatingsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
