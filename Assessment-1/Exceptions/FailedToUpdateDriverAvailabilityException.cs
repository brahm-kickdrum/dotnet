namespace Assessment_1.Exceptions
{
    public class FailedToUpdateDriverAvailabilityException : Exception
    {
        public FailedToUpdateDriverAvailabilityException(string message) : base(message) { }
        public FailedToUpdateDriverAvailabilityException(string message, Exception innerException) : base(message, innerException) { }

    }
}
