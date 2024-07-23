using BankSystem.Application.Services;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;


namespace BankSystem.Infrastructure.ExternalServices;



public class UserManager(IUnitOfWork uow) : IUserManagerService
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<User> Authenticate(string usernameOrEmail, string password)
    {
        var user = await _uow.Users.SigninUserAsync(usernameOrEmail, password);
        return user;
    }

    public async Task Delete(string username)
    {
        var user = await _uow.Users.GetUserByUserName(username);
        if (user != null)
        {
            var refreshToken = await _uow.RefreshToken.GetRefreshToken(user.ID);
            foreach(var token in refreshToken) 
            {
                _uow.RefreshToken.Delete(token);
            }
            await _uow.Save();
        }
    }

    public async Task<IEnumerable<RefreshToken?>> GetRefreshToken(string username)
    {
        var user = await _uow.Users.GetUserByUserName(username);
        if (user != null)
        {
            var refreshToken = await _uow.RefreshToken.GetRefreshToken(user.ID);
            return refreshToken;
        }
        return null;
    }

    public async Task<IEnumerable<RefreshToken?>> GetRefreshTokenAsync(string username)
    {
        var user = await _uow.Users.GetUserByUserName(username);
        if (user != null)
        {
            var refreshToken = await _uow.RefreshToken.GetRefreshToken(user.ID);
            return refreshToken;
        }
        return null;
    }

    public async Task SaveRefreshToken(string username, string refreshToken)
    {
        var user = await _uow.Users.GetUserByUserName(username);
        if (user != null)
        {
            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.ID,
                Token = refreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7) // Example: 7 days expiry
            };
            //user.RefreshTokens.Add(refreshTokenEntity);
            await _uow.RefreshToken.SaveRefreshtoken(refreshTokenEntity);
            await _uow.Save();
        }
    }
}
