using Assignment_2.CustomException;
using Assignment_2.Entity;
using Assignment_2.Repository.IRepository;
using Assignment_2.Service.IService;
using Assignment_2.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentTwoTests.Service
{
    public class UserDataServiceTests
    {
        private readonly Mock<IUserDataRepository> _userDataRepositoryMock;
        private readonly IUserDataService _userDataService;

        public UserDataServiceTests()
        {
            _userDataRepositoryMock = new Mock<IUserDataRepository>();
            _userDataService = new UserDataService(_userDataRepositoryMock.Object);
        }

        [Fact]
        public void AddUserData_Calls_AddUser_Method_Of_UserDataRepository()
        {
            // Arrange
            UserData userData = new UserData { Username = "testuser", Email = "test@test.com" };

            // Act
            _userDataService.AddUserData(userData);

            // Assert
            _userDataRepositoryMock.Verify(repo => repo.AddUser(userData), Times.Once);
        }

        [Fact]
        public void IsEmailUnique_ReturnsTrue_WhenEmailIsUnique()
        {
            // Arrange
            string email = "test@test.com";
            List<UserData> userDataList = new List<UserData>();

            _userDataRepositoryMock.Setup(repo => repo.GetUsersDataList()).Returns(userDataList);

            // Act
            bool result = _userDataService.IsEmailUnique(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmailUnique_ThrowsEmailAlreadyExistsException_WhenEmailIsNotUnique()
        {
            // Arrange
            string email = "test@test.com";
            List<UserData> userDataList = new List<UserData> { new UserData { Email = email } };

            _userDataRepositoryMock.Setup(repo => repo.GetUsersDataList()).Returns(userDataList);

            // Act
            EmailAlreadyExistsException exception = Assert.Throws<EmailAlreadyExistsException>(() => _userDataService.IsEmailUnique(email));

            //Assert
            Assert.Equal("User with given email address already exists.", exception.Message);
        }

        [Fact]
        public void IsUsernameUnique_ReturnsTrue_WhenUsernameIsUnique()
        {
            // Arrange
            string username = "testuser";
            List<UserData> userDataList = new List<UserData>();

            _userDataRepositoryMock.Setup(repo => repo.GetUsersDataList()).Returns(userDataList);

            // Act
            bool result = _userDataService.IsUsernameUnique(username);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsUsernameUnique_ThrowsUsernameAlreadyExistsException_WhenUsernameIsNotUnique()
        {
            // Arrange
            string username = "testuser";
            List<UserData> userDataList = new List<UserData> { new UserData { Username = username } };

            _userDataRepositoryMock.Setup(repo => repo.GetUsersDataList()).Returns(userDataList);

            // Act & Assert
            UsernameAlreadyExistsException exception = Assert.Throws<UsernameAlreadyExistsException>(() => _userDataService.IsUsernameUnique(username));
            Assert.Equal("Username already exists.", exception.Message);
        }

        [Fact]
        public void GetUserData_Returns_UserData_When_UserExists()
        {
            // Arrange
            string username = "testuser";
            List<UserData> userDataList = new List<UserData> { new UserData { Username = username } };

            _userDataRepositoryMock.Setup(repo => repo.GetUsersDataList()).Returns(userDataList);

            // Act
            UserData result = _userDataService.GetUserData(username);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.Username);
        }

        [Fact]
        public void GetUserData_Throws_UserNotFoundException_When_UserDoesNotExist()
        {
            // Arrange
            string username = "nonexistinguser";
            List<UserData> userDataList = new List<UserData>();

            _userDataRepositoryMock.Setup(repo => repo.GetUsersDataList()).Returns(userDataList);

            // Act
            UserNotFoundException exception = Assert.Throws<UserNotFoundException>(() => _userDataService.GetUserData(username));

            // Assert
            Assert.Equal($"User data not found for username: {username}", exception.Message);
        }

    }
}
