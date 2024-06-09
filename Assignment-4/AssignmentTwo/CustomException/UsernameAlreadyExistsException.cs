namespace Assignment_2.CustomException
{
    public class UsernameAlreadyExistsException : Exception
    {
        public UsernameAlreadyExistsException(string message): base(message) {  } 
    }
}
