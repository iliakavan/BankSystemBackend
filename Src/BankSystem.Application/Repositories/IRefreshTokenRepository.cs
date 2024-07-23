using BankSystem.Domain.Models;

namespace BankSystem.Application.Repositories;


public interface IRefreshTokenRepository
{
    Task<IEnumerable<RefreshToken?>> GetRefreshToken(Guid UserId);

    Task SaveRefreshtoken(RefreshToken RefreshToken);

    void Update(RefreshToken refreshToken);

    void Delete(RefreshToken refreshToken);
}
