using DrugsAPI_New.Models;

namespace DrugsAPI_New.Repositories
{
public interface IUserRepository
{
    User GetUserByUsername(string username);
    bool ValidateUser(string username, string password);
    bool CreateUser(User user);
}
}
