public class Library
{
    public List<Book> Books { get; set; }
    public List<User> Users { get; set; }
    public List<Admin> Admins { get; set; }

    public Library()
    {
        Books = new List<Book>();
        Users = new List<User>();
        Admins = new List<Admin>();
    }

    public void CreateAndAddAdmin(string username, string password)
    {
        Admin admin = new Admin(username, password);
        Admins.Add(admin);
    }

    public User FindUser(string name)
    {
        return Users.Find(user => user.Name == name);
    }

    public Book FindBook(string title)
    {
        return Books.Find(book => book.Title == title);
    }

    public void AddBook(string adminUsername, string adminPassword, Book book)
    {
        Admin admin = FindAdmin(adminUsername, adminPassword);
        if (admin == null)
        {
            Console.WriteLine("\nAdmin authentication failed.");
            return;
        }

        Books.Add(book);
        Console.WriteLine($"\nAdded book: {book.Title}");
    }

    public void RegisterUser(User user)
    {
        Users.Add(user);
        Console.WriteLine($"\nRegistered user: {user.Name}");
    }

    public void IssueBook(string adminUsername, string adminPassword, User user, Book book)
    {
        Admin admin = FindAdmin(adminUsername, adminPassword);
        if (admin == null)
        {
            Console.WriteLine("\nAdmin authentication failed.");
            return;
        }

        if (user.UserType == UserType.Teacher && user.IssuedBooks.Count >= Constants.MaxBooksPerTeacher)
        {
            Console.WriteLine("\nTeacher has already reached maximum issuance limit.");
            return;
        }
        else if (user.UserType == UserType.Student && user.IssuedBooks.Count >= Constants.MaxBooksPerUser)
        {
            Console.WriteLine("\nStudent has already reached maximum issuance limit.");
            return;
        }

        if (book.InventoryCount == 0)
        {
            Console.WriteLine("\nBook is out of stock.");
            return;
        }

        user.IssuedBooks.Add(book);
        book.IssuedTo.Add(user);
        book.IssueDate = DateTime.Now;
        book.InventoryCount--;
        user.ReturnDate = DateTime.Now.AddDays(7);

        Console.WriteLine($"\nBook '{book.Title}' issued to {user.Name}. Return by {user.ReturnDate.ToShortDateString()}");
    }

    public void ReturnBook(string adminUsername, string adminPassword, string userName, string bookTitle)
    {
        Admin admin = FindAdmin(adminUsername, adminPassword);
        if (admin == null)
        {
            Console.WriteLine("\nAdmin authentication failed.");
            return;
        }

        User user = FindUser(userName);
        if (user == null)
        {
            Console.WriteLine("\nUser not found.");
            return;
        }

        Book book = FindBook(bookTitle);
        if (book == null)
        {
            Console.WriteLine("\nBook not found.");
            return;
        }

        if (!user.IssuedBooks.Contains(book))
        {
            Console.WriteLine($"\nBook '{book.Title}' not issued to {user.Name}.");
            return;
        }

        user.IssuedBooks.Remove(book);
        book.IssuedTo.Remove(user);
        book.InventoryCount++;
        Console.WriteLine($"\nBook '{book.Title}' returned by {user.Name}");
    }

    public void ManageInventory(string adminUsername, string adminPassword, Book book, int inventoryCount)
    {
        Admin admin = FindAdmin(adminUsername, adminPassword);
        if (admin == null)
        {
            Console.WriteLine("\nAdmin authentication failed.");
            return;
        }

        book.InventoryCount += inventoryCount;
        Console.WriteLine($"\nInventory of book '{book.Title}' managed. New count: {book.InventoryCount}");
    }

    public decimal CalculateFine(User user)
    {
        decimal fine = 0;
        foreach (Book book in user.IssuedBooks)
        {
            TimeSpan elapsed = DateTime.Now - book.IssueDate;
            if (elapsed.Days > Constants.MaxDaysBookIssued)
            {
                fine += (elapsed.Days - Constants.MaxDaysBookIssued) * Constants.FinePerDay;
            }
        }
        Console.WriteLine($"\nFine for user {user.Name}: {fine}");
        return fine;
    }

    public void ListIssuers(Book book)
    {
        Console.WriteLine($"\nIssuers of book '{book.Title}':");
        foreach (User user in book.IssuedTo)
        {
            Console.WriteLine($"\n{user.Name}");
        }
    }

    public void GetBookInventory(Book book)
    {
        Console.WriteLine($"\nInventory information for book '{book.Title}':");
        Console.WriteLine($"Inventory Count: {book.InventoryCount}");
        Console.WriteLine($"Issued To: {book.IssuedTo.Count}");
    }

    public Admin FindAdmin(string username, string password)
    {
        foreach (Admin admin in Admins)
        {
            if (admin.Authenticate(username, password))
            {
                return admin;
            }
        }
        return null;
    }

    public void ListUserBooks(string userName)
    {
        User user = FindUser(userName);
        if (user == null)
        {
            Console.WriteLine("\nUser not found.");
            return;
        }

        Console.WriteLine($"\nBooks issued to {user.Name}:");
        foreach (Book book in user.IssuedBooks)
        {
            Console.WriteLine($"{book.Title} by {book.Author}");
        }
    }

    public void GetBookInventoryInfo(string bookTitle)
    {
        Book book = Books.Find(b => b.Title == bookTitle);
        if (book != null)
        {
            Console.WriteLine($"\nInventory information for book '{book.Title}':");
            Console.WriteLine($"Inventory Count: {book.InventoryCount}");
            Console.WriteLine($"Issued To: {book.IssuedTo.Count}");
        }
        else
        {
            Console.WriteLine("\nBook not found in the library.");
        }
    }

    public void ListIssuersOfBook(string bookTitle)
    {
        Book book = Books.Find(b => b.Title == bookTitle);
        if (book != null)
        {
            Console.WriteLine($"\nIssuers of book '{book.Title}':");
            foreach (User user in book.IssuedTo)
            {
                Console.WriteLine(user.Name);
            }
        }
        else
        {
            Console.WriteLine("\nBook not found in the library.");
        }
    }

    public void ReduceInventory(string adminUsername, string adminPassword, Book book, int count)
    {
        Admin admin = FindAdmin(adminUsername, adminPassword);
        if (admin == null)
        {
            Console.WriteLine("\nAdmin authentication failed.");
            return;
        }

        if (book.InventoryCount < count)
        {
            Console.WriteLine($"\nError: Not enough inventory. Available count: {book.InventoryCount}");
            return;
        }

        book.InventoryCount -= count;
        Console.WriteLine($"\nInventory of book '{book.Title}' reduced. New count: {book.InventoryCount}");
    }
}