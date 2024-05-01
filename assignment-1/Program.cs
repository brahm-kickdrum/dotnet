using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        bool exit = false;

        Console.WriteLine("Welcome to the Library Management System");
        while (!exit)
        {
            Console.WriteLine("\n1. Create Admin Account");
            Console.WriteLine("2. Add Book");
            Console.WriteLine("3. Register User");
            Console.WriteLine("4. Issue Book");
            Console.WriteLine("5. Return Book");
            Console.WriteLine("6. List books issued by User");
            Console.WriteLine("7. Get book inventory info");
            Console.WriteLine("8. List users with the book");
            Console.WriteLine("9. Calculate fine for user");
            Console.WriteLine("10. Manage Inventory (Increase)");
            Console.WriteLine("11. Manage Inventory (Reduce)");
            Console.WriteLine("12. Exit\n");

            Console.Write("Please select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAdmin(library);
                    break;
                case "2":
                    AddBook(library);
                    break;
                case "3":
                    RegisterUser(library);
                    break;
                case "4":
                    IssueBook(library);
                    break;
                case "5":
                    ReturnBook(library);
                    break;
                case "6":
                    ListUserBooks(library);
                    break;
                case "7":
                    GetBookInventoryInfo(library);
                    break;
                case "8":
                    ListIssuersOfBook(library);
                    break;
                case "9":
                    CalculateFine(library);
                    break;
                case "10":
                    ManageInventory(library);
                    break;
                case "11":
                    ReduceInventory(library);
                    break;
                case "12":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void CreateAdmin(Library library)
    {
        string username;
        do
        {
            Console.Write("\nEnter admin username: ");
            username = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(username))
            {
                Console.WriteLine("Username cannot be empty.");
            }
        } while (string.IsNullOrWhiteSpace(username));

        Console.Write("Enter admin password: ");
        string password = Console.ReadLine();

        library.CreateAndAddAdmin(username, password);

        Console.WriteLine("\nAdmin account created successfully.");
    }


    static void RegisterUser(Library library)
    {
        Console.Write("\nEnter user name: ");
        string name = Console.ReadLine();
        Console.Write("Enter user type (Student/Teacher): ");
        string userTypeInput = Console.ReadLine();
        UserType type;
        if (Enum.TryParse<UserType>(userTypeInput, out type))
        {
            User user = new User(name, type);
            library.RegisterUser(user);
        }
        else
        {
            Console.WriteLine("\nInvalid user type.");
        }
    }

    static void AddBook(Library library)
    {
        Console.Write("\nEnter admin username: ");
        string adminUsername = Console.ReadLine();
        Console.Write("Enter admin password: ");
        string adminPassword = Console.ReadLine();

        Console.Write("Enter book title: ");
        string title = Console.ReadLine();
        Console.Write("Enter book author: ");
        string author = Console.ReadLine();
        Console.Write("Enter initial inventory count: ");
        int inventoryCount;
        if (!int.TryParse(Console.ReadLine(), out inventoryCount))
        {
            Console.WriteLine("\nInvalid inventory count. Please enter a valid number.");
            return;
        }

        Book book = new Book(title, author, inventoryCount);
        library.AddBook(adminUsername, adminPassword, book);
    }

    static void IssueBook(Library library)
    {
        Console.Write("\nEnter admin username: ");
        string adminUsername = Console.ReadLine();
        Console.Write("Enter admin password: ");
        string adminPassword = Console.ReadLine();

        Console.Write("Enter user name: ");
        string userName = Console.ReadLine();
        User user = library.FindUser(userName);
        if (user == null)
        {
            Console.WriteLine("\nUser not found.");
            return;
        }

        Console.Write("Enter book title: ");
        string bookTitle = Console.ReadLine();
        Book book = library.FindBook(bookTitle);
        if (book == null)
        {
            Console.WriteLine("\nBook not found.");
            return;
        }

        library.IssueBook(adminUsername, adminPassword, user, book);
    }

    static void ReturnBook(Library library)
    {
        Console.Write("\nEnter admin username: ");
        string adminUsername = Console.ReadLine();
        Console.Write("Enter admin password: ");
        string adminPassword = Console.ReadLine();

        Console.Write("Enter user name: ");
        string userName = Console.ReadLine();
        Console.Write("Enter book title: ");
        string bookTitle = Console.ReadLine();

        library.ReturnBook(adminUsername, adminPassword, userName, bookTitle);
    }

    static void ListUserBooks(Library library)
    {
        Console.Write("\nEnter user name: ");
        string userName = Console.ReadLine();

        library.ListUserBooks(userName);
    }

    static void GetBookInventoryInfo(Library library)
    {
        Console.Write("\nEnter book title: ");
        string bookTitle = Console.ReadLine();

        library.GetBookInventoryInfo(bookTitle);
    }

    static void ListIssuersOfBook(Library library)
    {
        Console.Write("\nEnter book title: ");
        string bookTitle = Console.ReadLine();

        library.ListIssuersOfBook(bookTitle);
    }

    static void CalculateFine(Library library)
    {
        Console.Write("\nEnter user name: ");
        string userName = Console.ReadLine();
        User user = library.FindUser(userName);
        if (user == null)
        {
            Console.WriteLine("\nUser not found.");
            return;
        }

        library.CalculateFine(user);
    }

    static void ManageInventory(Library library)
    {
        Console.Write("\nEnter admin username: ");
        string adminUsername = Console.ReadLine();
        Console.Write("Enter admin password: ");
        string adminPassword = Console.ReadLine();

        Console.Write("Enter book title: ");
        string bookTitle = Console.ReadLine();
        Book book = library.FindBook(bookTitle);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        Console.Write("Enter count to increase: ");
        int count;
        if (!int.TryParse(Console.ReadLine(), out count))
        {
            Console.WriteLine("\nInvalid count. Please enter a valid number.");
            return;
        }

        library.ManageInventory(adminUsername, adminPassword, book, count);
    }

    static void ReduceInventory(Library library)
    {
        Console.Write("\nEnter admin username: ");
        string adminUsername = Console.ReadLine();
        Console.Write("Enter admin password: ");
        string adminPassword = Console.ReadLine();

        Console.Write("Enter book title: ");
        string bookTitle = Console.ReadLine();
        Book book = library.FindBook(bookTitle);
        if (book == null)
        {
            Console.WriteLine("\nBook not found.");
            return;
        }

        Console.Write("Enter count to reduce: ");
        int count;
        if (!int.TryParse(Console.ReadLine(), out count))
        {
            Console.WriteLine("\nInvalid count. Please enter a valid number.");
            return;
        }

        library.ReduceInventory(adminUsername, adminPassword, book, count);
    }
}
