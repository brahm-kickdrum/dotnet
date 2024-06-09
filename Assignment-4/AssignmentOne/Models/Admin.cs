public class Admin
{
    public string Username { get; set; }
    public string Password { get; set; }

    public Admin(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }

    public bool Authenticate(string enteredUsername, string enteredPassword)
    {
        return Username == enteredUsername && Password == enteredPassword;
    }
}