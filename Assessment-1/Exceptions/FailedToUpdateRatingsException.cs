namespace Assessment_1.Exceptions
{
    public class FailedToUpdateRatingsException : Exception
    {
        public FailedToUpdateRatingsException(string message) : base(message) { }
        public FailedToUpdateRatingsException(string message, Exception innerException) : base(message, innerException) { }

    }
}
