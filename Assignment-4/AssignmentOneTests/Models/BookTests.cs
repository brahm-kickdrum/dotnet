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
            var title = "Test Title";
            var author = "Test Author";
            var inventoryCount = 10;

            // Act
            var book = new Book(title, author, inventoryCount);

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
            var book = new Book("Test Title", "Test Author", 10);
            var user = new User("Test User", UserType.Student);

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
            var book = new Book("Test Title", "Test Author", 10);
            var user = new User("Test User", UserType.Student);
            book.IssuedTo.Add(user);

            // Act
            book.IssuedTo.Remove(user);

            // Assert
            Assert.DoesNotContain(user, book.IssuedTo);
            Assert.Empty(book.IssuedTo);
        }
    }
}
