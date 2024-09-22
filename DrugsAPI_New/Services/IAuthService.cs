using DrugsAPI_New.Models;

namespace DrugsAPI_New.Services
{
public interface IAuthService
{
    string GenerateToken(User user);
    bool ValidateToken(string token);
    (bool isSuccess, string error) CreateUser(User user, string password);
    bool InvalidateToken(string token); // Add this method
}

}