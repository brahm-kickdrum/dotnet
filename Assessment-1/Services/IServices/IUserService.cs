using Assessment_1.Entities;
using Assessment_1.Enums;

namespace Assessment_1.Services.IServices
{
    public interface IUserService
    {
        User GetUserFromEmailAndRole(string email, UserRole userRole);

        User GetUserById(Guid id);
    }
}
