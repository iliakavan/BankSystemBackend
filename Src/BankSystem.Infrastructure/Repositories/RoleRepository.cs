using BankSystem.Domain.Models;
using BankSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BankSystem.Application;
using BankSystem.Application.Repositories;

namespace BankSystem.Infrastructure.Repositories;



public sealed class RoleRepository(AppDbContext context) : IRoleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Role?> FindAsync(int roleId)
        => await _context.Roles.AsQueryable()
                            .Where(r => r.Id == roleId)
                            .FirstOrDefaultAsync();
}