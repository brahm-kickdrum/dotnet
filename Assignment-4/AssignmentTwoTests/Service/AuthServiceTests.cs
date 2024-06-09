using Assignment_2.CustomException;
using Assignment_2.Entity;
using Assignment_2.Repository.IRepository;
using Assignment_2.Service;
using Assignment_2.Service.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace AssignmentTwoTests.Service
{
    public class AuthServiceTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly IAuthService _authService;

        public AuthServiceTests()
        {
            _authRepositoryMock = new Mock<IAuthRepository>();
            _configurationMock = new Mock<IConfiguration>();

            _configurationMock.Setup(config => config["Jwt:Key"]).Returns("thisisaverysecurekeywith256bits!");
            _configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("testIssuer");
            _configurationMock.Setup(config => config["Jwt:Audience"]).Returns("testAudience");

            _authService = new AuthService(_authRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public void AuthenticateUser_ValidUser_ReturnsUsername()
        {
            // Arrange
            UserAuth user = new UserAuth { Username = "testuser", Password = "testpassword" };
            List<UserAuth> userAuthList = new List<UserAuth> { user };
            _authRepositoryMock.Setup(repo => repo.GetUsersAuthList()).Returns(userAuthList);

            // Act
            string result = _authService.AutheticateUser(user);

            // Assert
            Assert.Equal(user.Username, result);
        }

        [Fact]
        public void AuthenticateUser_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            UserAuth user = new UserAuth { Username = "nonexistinguser", Password = "testpassword" };
            List<UserAuth> userAuthList = new List<UserAuth>();
            _authRepositoryMock.Setup(repo => repo.GetUsersAuthList()).Returns(userAuthList);

            // Act 
            UserNotFoundException exception = Assert.Throws<UserNotFoundException>(() => _authService.AutheticateUser(user));

            // Assert
            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public void AuthenticateUser_InvalidPassword_ThrowsInvalidPasswordException()
        {
            // Arrange
            UserAuth user = new UserAuth { Username = "testuser", Password = "invalidpassword" };
            UserAuth userWithValidPassword = new UserAuth { Username = "testuser", Password = "correctpassword" };
            List<UserAuth> userAuthList = new List<UserAuth> { userWithValidPassword };
            _authRepositoryMock.Setup(repo => repo.GetUsersAuthList()).Returns(userAuthList);

            // Act 
            InvalidPasswordException exception = Assert.Throws<InvalidPasswordException>(() => _authService.AutheticateUser(user));

            // Assert
            Assert.Equal("Invalid password", exception.Message);
        }

        [Fact]
        public void AddUserAuth_AddsUserToList()
        {
            // Arrange
            UserAuth userAuth = new UserAuth { Username = "testuser", Password = "testpassword" };

            // Act
            _authService.AddUserAuth(userAuth);

            // Assert
            _authRepositoryMock.Verify(repo => repo.AddUser(userAuth), Times.Once);
        }
    }
}
