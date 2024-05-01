public class User
{
    public string Name { get; set; }
    public UserType UserType { get; set; }
    public List<Book> IssuedBooks { get; set; }
    public DateTime ReturnDate { get; set; }

    public User(string name, UserType userType)
    {
        Name = name;
        UserType = userType;
        IssuedBooks = new List<Book>();
    }
}