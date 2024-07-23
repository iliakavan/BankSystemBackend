using BankSystem.Application.Repositories;
using BankSystem.Domain.Models;
using BankSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repositories;


public class RefreshTokenRepository(AppDbContext context) : IRefreshTokenRepository
{
    private readonly AppDbContext _context = context;

    public void Delete(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
    }

    public async Task<IEnumerable<RefreshToken?>> GetRefreshToken(Guid UserId)
    {
        return await _context.RefreshTokens.AsQueryable().Where(x => x.UserId == UserId).ToListAsync();
    }


    public async Task SaveRefreshtoken(RefreshToken RefreshToken)
    {
        if (RefreshToken != null)
        {
            await _context.RefreshTokens.AddAsync(RefreshToken);
        }
    }

    public void Update(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
    }
}
