using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentOneTests.Models
{
    public class LibraryTests
    {
        [Fact]
        public void CreateAndAddAdmin_WhenAdminDoesNotExist_ShouldAddAdminToLibrary()
        {
            // Arrange
            Library library = new Library();

            // Act
            library.CreateAndAddAdmin("admin1", "password1");

            // Assert
            Assert.Single(library.Admins);
            Assert.Equal("admin1", library.Admins[0].Username);
        }

        [Fact]
        public void FindUser_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            Library library = new Library();
            User user = new User("John", UserType.Student);
            library.RegisterUser(user);

            // Act
            User foundUser = library.FindUser("John");

            // Assert
            Assert.NotNull(foundUser);
            Assert.Equal(user, foundUser);
        }

        [Fact]
        public void FindUser_WhenUserDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            Library library = new Library();

            // Act
            User foundUser = library.FindUser("NonExistentUser");

            // Assert
            Assert.Null(foundUser);
        }

        [Fact]
        public void FindUser_WhenMultipleUsersExistWithSameName_ShouldReturnFirstUserFound()
        {
            // Arrange
            Library library = new Library();
            User user1 = new User("John", UserType.Student);
            User user2 = new User("John", UserType.Teacher);
            library.RegisterUser(user1);
            library.RegisterUser(user2);

            // Act
            User foundUser = library.FindUser("John");

            // Assert
            Assert.NotNull(foundUser);
            Assert.Equal(user1, foundUser);
        }

        [Fact]
        public void FindBook_WhenBookDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            Library library = new Library();

            // Act
            Book foundBook = library.FindBook("NonExistentBook");

            // Assert
            Assert.Null(foundBook);
        }

        [Fact]
        public void RegisterUser_WhenUserIsNotNull_ShouldAddUserToLibrary()
        {
            // Arrange
            Library library = new Library();
            User user = new User("John Doe", UserType.Student);

            // Act
            library.RegisterUser(user);

            // Assert
            Assert.Contains(user, library.Users);
        }

        [Fact]
        public void IssueBook_WhenAdminAuthenticationFails_ShouldNotIssueBook()
        {
            // Arrange
            Library library = new Library();
            User user = new User("John Doe", UserType.Student);
            Book book = new Book("Introduction to Programming", "John Smith", 20);

            // Act
            library.IssueBook("admin", "wrongpassword", user, book);

            // Assert
            Assert.Empty(user.IssuedBooks);
        }

        [Fact]
        public void IssueBook_WhenInventoryCountIsZero_ShouldNotIssueBook()
        {
            // Arrange
            Library library = new Library();
            Admin admin = new Admin("admin", "password");
            User user = new User("John Doe", UserType.Student);
            Book book = new Book("Introduction to Programming", "John Smith", 0);

            // Act
            library.CreateAndAddAdmin(admin.Username, admin.Password);
            library.AddBook(admin.Username, admin.Password, book);
            library.IssueBook(admin.Username, admin.Password, user, book);

            // Assert
            Assert.Empty(user.IssuedBooks);
        }

        [Fact]
        public void ReturnBook_WhenBookNotIssuedToUser_ShouldNotReturnBook()
        {
            // Arrange
            Library library = new Library();
            Admin admin = new Admin("admin", "password");
            User user1 = new User("John Doe", UserType.Student);
            User user2 = new User("Jane Doe", UserType.Teacher);
            Book book = new Book("Introduction to Programming", "John Smith", 20);

            // Act
            library.CreateAndAddAdmin(admin.Username, admin.Password);
            library.AddBook(admin.Username, admin.Password, book);
            library.RegisterUser(user1);
            library.RegisterUser(user2);
            library.IssueBook(admin.Username, admin.Password, user1, book);
            library.ReturnBook(admin.Username, admin.Password, user2.Name, book.Title);

            // Assert
            Assert.DoesNotContain(book, user2.IssuedBooks);
        }

        [Fact]
        public void ReturnBook_WhenBookReturned_ShouldUpdateInventoryCount()
        {
            // Arrange
            Library library = new Library();
            Admin admin = new Admin("admin", "password");
            User user = new User("John Doe", UserType.Student);
            Book book = new Book("Introduction to Programming", "John Smith", 20);

            // Act
            library.CreateAndAddAdmin(admin.Username, admin.Password);
            library.AddBook(admin.Username, admin.Password, book);
            library.RegisterUser(user);
            library.IssueBook(admin.Username, admin.Password, user, book);
            library.ReturnBook(admin.Username, admin.Password, user.Name, book.Title);

            // Assert
            Assert.Equal(20, book.InventoryCount);
        }

        [Fact]
        public void ManageInventory_WhenAdminAuthenticationFails_ShouldNotChangeInventory()
        {
            // Arrange
            Library library = new Library();
            Book book = new Book("Introduction to Programming", "John Smith", 20);

            // Act
            library.ManageInventory("admin", "wrongpassword", book, 5);

            // Assert
            Assert.Equal(20, book.InventoryCount);
        }

        [Fact]
        public void ManageInventory_WhenInventoryManaged_ShouldUpdateInventoryCount()
        {
            // Arrange
            Library library = new Library();
            Admin admin = new Admin("admin", "password");
            Book book = new Book("Introduction to Programming", "John Smith", 20);

            // Act
            library.CreateAndAddAdmin(admin.Username, admin.Password);
            library.AddBook(admin.Username, admin.Password, book);
            library.ManageInventory(admin.Username, admin.Password, book, 5);

            // Assert
            Assert.Equal(25, book.InventoryCount);
        }

        [Fact]
        public void CalculateFine_UserWithNoIssuedBooks_ShouldReturnZero()
        {
            // Arrange
            Library library = new Library();
            User user = new User("John Doe", UserType.Student);

            // Act
            decimal fine = library.CalculateFine(user);

            // Assert
            Assert.Equal(0, fine);
        }

        [Fact]
        public void CalculateFine_UserWithIssuedBooks_ShouldReturnZero()
        {
            // Arrange
            User user = new User("John Doe", UserType.Student);
            Book book1 = new Book("Book 1", "Author 1", 10);
            Book book2 = new Book("Book 2", "Author 2", 5);
            user.IssuedBooks.Add(book1);
            user.IssuedBooks.Add(book2);
            book1.IssueDate = DateTime.Now.AddDays(-5); 
            book2.IssueDate = DateTime.Now.AddDays(-3);

            Library library = new Library();

            // Act
            decimal fine = library.CalculateFine(user);

            // Assert
            Assert.Equal(0, fine);
        }

        [Fact]
        public void CalculateFine_UserWithIssuedBooks_ShouldReturnFine()
        {
            // Arrange
            User user = new User("Jane Smith", UserType.Teacher);
            Book overdueBook = new Book("Overdue Book", "Author 3", 10);
            user.IssuedBooks.Add(overdueBook);
            overdueBook.IssueDate = DateTime.Now.AddDays(-10); // More than allowed time

            Library library = new Library();

            // Act
            decimal fine = library.CalculateFine(user);

            // Assert
            decimal expectedFine = (10 - Constants.MaxDaysBookIssued) * Constants.FinePerDay;
            Assert.Equal(expectedFine, fine);
        }

        [Fact]
        public void ListIssuers_WhenBookExists_ShouldListIssuers()
        {
            // Arrange
            Library library = new Library();
            Book book = new Book("Introduction to Programming", "John Smith", 20);
            User user1 = new User("Alice", UserType.Student);
            User user2 = new User("Bob", UserType.Teacher);
            book.IssuedTo.Add(user1);
            book.IssuedTo.Add(user2);

            // Act
            string consoleOutput = CaptureConsoleOutput(() => library.ListIssuers(book));

            // Assert
            Assert.Contains("Alice", consoleOutput);
            Assert.Contains("Bob", consoleOutput);
        }

        [Fact]
        public void GetBookInventory_WhenBookExists_ShouldDisplayInventoryInfo()
        {
            // Arrange
            Library library = new Library();
            Book book = new Book("Introduction to Programming", "John Smith", 20);
            book.IssuedTo.Add(new User("Alice", UserType.Student));
            book.IssuedTo.Add(new User("Bob", UserType.Teacher));

            // Act
            string consoleOutput = CaptureConsoleOutput(() => library.GetBookInventory(book));

            // Assert
            Assert.Contains($"Inventory Count: {book.InventoryCount}", consoleOutput);
            Assert.Contains($"Issued To: {book.IssuedTo.Count}", consoleOutput);
        }

        [Fact]
        public void FindAdmin_WhenAdminExists_ShouldReturnAdminObject()
        {
            // Arrange
            Library library = new Library();
            library.CreateAndAddAdmin("admin", "password");

            // Act
            Admin foundAdmin = library.FindAdmin("admin", "password");

            // Assert
            Assert.NotNull(foundAdmin);
            Assert.Equal("admin", foundAdmin.Username);
        }

        [Fact]
        public void FindAdmin_WhenAdminDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            Library library = new Library();
            library.CreateAndAddAdmin("admin", "password");

            // Act
            Admin foundAdmin = library.FindAdmin("unknown", "password");

            // Assert
            Assert.Null(foundAdmin);
        }

        [Fact]
        public void ListUserBooks_WhenUserExists_ShouldListBooks()
        {
            // Arrange
            Library library = new Library();
            User user = new User("John Doe", UserType.Student);
            Book book1 = new Book("Introduction to Programming", "John Smith", 20);
            Book book2 = new Book("Data Structures and Algorithms", "Jane Johnson", 15);
            user.IssuedBooks.Add(book1);
            user.IssuedBooks.Add(book2);
            library.RegisterUser(user);

            // Act
            String output = CaptureConsoleOutput(() => library.ListUserBooks("John Doe"));

            // Assert
            Assert.Contains("Books issued to John Doe:", output);
            Assert.Contains("Introduction to Programming by John Smith", output);
            Assert.Contains("Data Structures and Algorithms by Jane Johnson", output);
        }

        [Fact]
        public void ListUserBooks_WhenUserDoesNotExist_ShouldDisplayErrorMessage()
        {
            // Arrange
            Library library = new Library();

            // Act
            String output = CaptureConsoleOutput(() => library.ListUserBooks("Unknown User"));

            // Assert
            Assert.Contains("User not found.", output);
        }

        [Fact]
        public void GetBookInventoryInfo_WhenBookDoesNotExist_ShouldDisplayErrorMessage()
        {
            // Arrange
            Library library = new Library();

            // Act
            String output = CaptureConsoleOutput(() => library.GetBookInventoryInfo("Unknown Book"));

            // Assert
            Assert.Contains("Book not found in the library.", output);
        }

        [Fact]
        public void ListIssuersOfBook_WhenBookDoesNotExist_ShouldDisplayErrorMessage()
        {
            // Arrange
            Library library = new Library();

            // Act
            String output = CaptureConsoleOutput(() => library.ListIssuersOfBook("Unknown Book"));

            // Assert
            Assert.Contains("Book not found in the library.", output);
        }

        private string CaptureConsoleOutput(Action action)
        {
            StringWriter consoleOutputWriter = new System.IO.StringWriter();
            Console.SetOut(consoleOutputWriter);

            action();

            return consoleOutputWriter.ToString();
        }
    }
}
