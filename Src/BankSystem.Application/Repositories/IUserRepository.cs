using BankSystem.Domain.Models;

namespace BankSystem.Application.Repositories;


public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<bool> CheckIfUserExist(string UsernameorEmail);
    Task<User?> GetUserInformation(Guid UserId);
    Task<User?> SigninUserAsync(string UsernameOrEmail, string password);
    Task<User?> GetUserByUserName(string Username);
    Task<IEnumerable<Role>> GetRole(Guid UserId);
    Task<User?> FindAsync(Guid userId);
    Task<IEnumerable<User>> GetUsersBankAccountsOrderByFullname();
    Task<UserRole?> FindAsyncUserRole(int ID);
    void AddRole(UserRole userRole);
    void RemoveUserRole(UserRole userRole);
    void Update(User user);
    void Delete(User user);
}
