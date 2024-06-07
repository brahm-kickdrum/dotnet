namespace Assessment_1.Exceptions
{
    public class UserUnavailableForRoleException : Exception
    {
        public UserUnavailableForRoleException(string message) : base(message) { }
        public UserUnavailableForRoleException(string message, Exception innerException) : base(message, innerException) { }

    }
}
