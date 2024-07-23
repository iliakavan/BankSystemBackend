using BankSystem.Domain.Models;

namespace BankSystem.Application.Services;


public interface IUserManagerService
{
    Task<User> Authenticate(string username, string password);
    Task SaveRefreshToken(string username, string refreshToken);
    Task<IEnumerable<RefreshToken?>> GetRefreshTokenAsync(string username);
    Task<IEnumerable<RefreshToken?>> GetRefreshToken(string username);
    Task Delete(string username);
}
