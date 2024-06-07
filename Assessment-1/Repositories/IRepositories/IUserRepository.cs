using Assessment_1.Entities;
using Assessment_1.Enums;
using Assessment_1.Models;

namespace Assessment_1.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Guid AddUser(User user);

        bool CheckIfUserExists(User user, UserRole role);

        User? GetUserByEmailAndRole(string email, UserRole role);

        User? GetUserById(Guid id);
    }
}
