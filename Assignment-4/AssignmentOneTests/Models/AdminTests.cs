namespace AssignmentOneTests.Models
{
    public class AdminTests
    {
        [Fact]
        public void Authenticate_ValidCredentials_ReturnsTrue()
        {
            // Arrange
            Admin admin = new Admin("admin", "password");

            // Act
            bool result = admin.Authenticate("admin", "password");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Authenticate_InvalidUsername_ReturnsFalse()
        {
            // Arrange
            Admin admin = new Admin("admin", "password");

            // Act
            bool result = admin.Authenticate("wrongAdmin", "password");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Authenticate_InvalidPassword_ReturnsFalse()
        {
            // Arrange
            Admin admin = new Admin("admin", "password");

            // Act
            bool result = admin.Authenticate("admin", "wrongPassword");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Authenticate_InvalidUsernameAndPassword_ReturnsFalse()
        {
            // Arrange
            Admin admin = new Admin("admin1", "password1");

            // Act
            bool result = admin.Authenticate("wrongAdmin", "wrongPassword");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Authenticate_EmptyUsername_ReturnsFalse()
        {
            // Arrange
            Admin admin = new Admin("admin1", "password1");

            // Act
            bool result = admin.Authenticate(string.Empty, "password1");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Authenticate_EmptyPassword_ReturnsFalse()
        {
            // Arrange
            Admin admin = new Admin("admin1", "password1");

            // Act
            bool result = admin.Authenticate("admin1", string.Empty);

            // Assert
            Assert.False(result);
        }

    }
}
