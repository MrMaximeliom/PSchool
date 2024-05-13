using PSchool.Backend.Models;

namespace PSchool.Backend.Services
{
    public interface IAuthService
    {
        Task<Auth> RegisterAsync(RegisterUser model);

        Task<Auth> GetTokenAsync(RequestToken model);

        Task<string> AddUserToRoleAsync(AddRole model);

        Task<Auth> RefreshTokenAsync(string token);

        Task<bool> RevokeTokenAsync(string token);

    }
}
