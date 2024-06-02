using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentOneTests.Models
{
    public class UserTests
    {
        [Fact]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            // Arrange
            var name = "Test User";
            var userType = UserType.Student;

            // Act
            var user = new User(name, userType);

            // Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(userType, user.UserType);
            Assert.NotNull(user.IssuedBooks);
            Assert.Empty(user.IssuedBooks);
            Assert.Equal(default(DateTime), user.ReturnDate);
        }

        [Fact]
        public void AddBookToIssuedBooks_AddsBookCorrectly()
        {
            // Arrange
            var user = new User("Test User", UserType.Student);
            var book = new Book("Test Title", "Test Author", 10);

            // Act
            user.IssuedBooks.Add(book);

            // Assert
            Assert.Contains(book, user.IssuedBooks);
            Assert.Single(user.IssuedBooks);
        }

        [Fact]
        public void RemoveBookFromIssuedBooks_RemovesBookCorrectly()
        {
            // Arrange
            var user = new User("Test User", UserType.Student);
            var book = new Book("Test Title", "Test Author", 10);
            user.IssuedBooks.Add(book);

            // Act
            user.IssuedBooks.Remove(book);

            // Assert
            Assert.DoesNotContain(book, user.IssuedBooks);
            Assert.Empty(user.IssuedBooks);
        }
    }
}
