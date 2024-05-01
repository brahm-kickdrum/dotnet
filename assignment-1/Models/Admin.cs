public class Admin
{
    private string username;
    private string password;

    public Admin(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public bool Authenticate(string enteredUsername, string enteredPassword)
    {
        return username == enteredUsername && password == enteredPassword;
    }
}