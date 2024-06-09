using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentOneTests.Models
{
    public class BookTests
    {
        [Fact]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            string title = "Test Title";
            string author = "Test Author";
            int inventoryCount = 10;

            // Act
            Book book = new Book(title, author, inventoryCount);

            // Assert
            Assert.Equal(title, book.Title);
            Assert.Equal(author, book.Author);
            Assert.Equal(inventoryCount, book.InventoryCount);
            Assert.NotNull(book.IssuedTo);
            Assert.Empty(book.IssuedTo);
        }

        [Fact]
        public void AddUserToIssuedTo_AddsUserCorrectly()
        {
            // Arrange
            Book book = new Book("Test Title", "Test Author", 10);
            User user = new User("Test User", UserType.Student);

            // Act
            book.IssuedTo.Add(user);

            // Assert
            Assert.Contains(user, book.IssuedTo);
            Assert.Single(book.IssuedTo);
        }

        [Fact]
        public void RemoveUserFromIssuedTo_RemovesUserCorrectly()
        {
            // Arrange
            Book book = new Book("Test Title", "Test Author", 10);
            User user = new User("Test User", UserType.Student);
            book.IssuedTo.Add(user);

            // Act
            book.IssuedTo.Remove(user);

            // Assert
            Assert.DoesNotContain(user, book.IssuedTo);
            Assert.Empty(book.IssuedTo);
        }
    }
}
