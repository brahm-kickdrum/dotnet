using Assessment_1.DataAccess;
using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Exceptions;
using Assessment_1.Repositories.IRepositories;
using System.Linq;

namespace Assessment_1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RideDbContext _dbContext;

        public UserRepository(RideDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid AddUser(User user)
        {            
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user.UserId;
        }

        public bool CheckIfUserExists(User user, UserRole role)
        {
            if (_dbContext.Users.Any(r => r.Role == role && (r.Email == user.Email || r.Phone == user.Phone)))
            {
                return true; 
            }

            return false;
        }

        public User? GetUserByEmailAndRole(string email, UserRole role)
        {
            User? user  = _dbContext.Users.FirstOrDefault(r => r.Role == role && r.Email == email);
            return user;
        }

        public User? GetUserById(Guid id)
        {
            User? user = _dbContext.Users.Find(id);
            return user;
        }
    }
}
