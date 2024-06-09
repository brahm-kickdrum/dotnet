public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int InventoryCount { get; set; }
    public List<User> IssuedTo { get; set; }
    public DateTime IssueDate { get; set; }

    public Book(string title, string author, int inventoryCount)
    {
        Title = title;
        Author = author;
        InventoryCount = inventoryCount;
        IssuedTo = new List<User>();
    }
}