using BankSystem.Application.Repositories;
using BankSystem.Domain.Models;
using BankSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace BankSystem.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> CheckIfUserExist(string UsernameorEmail)
    {
        return await _context.Users
                        .AsQueryable()
                        .Where(u => u.UserName == UsernameorEmail || u.Email == UsernameorEmail).AnyAsync();
    }

    public void AddRole(UserRole userRole)
    {
        _context.UserRoles.Add(userRole);
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void Delete(User user)
    {
        if(!user.IsDelete) 
        {
            user.IsDelete = true;
            _context.Users.Update(user);
        }
    }

    public async Task<User?> FindAsync(Guid userId)
    {
        return await _context.Users
                        .AsQueryable()
                        .Where(x => x.ID == userId)
                        .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Role>> GetRole(Guid UserId)
    {
        return await _context.UserRoles
                        .AsQueryable()
                        .Include(x => x.Role)
                        .AsNoTracking()
                        .Where(x => x.UserId == UserId)
                        .Select(x => x.Role)
                        .OrderBy(x => x.Name)
                        .ToListAsync();

    }

    public async Task<User?> GetUserByUserName(string Username)
    {
            return await _context.Users.AsQueryable()
                                    .Where(u => u.UserName == Username)
                                    .FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserInformation(Guid UserId)
    {
        return await _context.Users.AsQueryable()
                                    .Where(U => U.ID == UserId).Include(U => U.BankAccounts)
                                    .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetUsersBankAccountsOrderByFullname()
    {
            var user = await _context.Users
                                .AsQueryable()
                                .Include(u => u.BankAccounts)
                                .Where(u => !u.IsDelete)
                                .ToListAsync();
        return user.OrderBy(u => u.Fullname);
    }


    public async Task<User?> SigninUserAsync(string UsernameOrEmail, string password)
    {
        var user = await _context.Users.AsQueryable().Where(user =>
                            user.UserName.ToLower() == UsernameOrEmail.ToLower() ||
                            user.Email.ToLower() == UsernameOrEmail.ToLower()).FirstOrDefaultAsync();
        if (BC.Verify(password, user.Password))
        {
            return user;
        }
        return null;
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public void RemoveUserRole(UserRole userRole)
    {
        _context.UserRoles.Remove(userRole);
    }

    public async Task<UserRole?> FindAsyncUserRole(int ID)
    {
        return await _context.UserRoles
                        .AsQueryable()
                        .Where(x => x.Id == ID)
                        .FirstOrDefaultAsync();
    }
}
