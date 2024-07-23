using BankSystem.Application.Repositories;
using BankSystem.Domain.Models;
using BankSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace BankSystem.Infrastructure.Repositories;



public class BankAccountRepository(AppDbContext context) : IBankAccountRepository
{
    private readonly AppDbContext _context = context;

    public async Task<BankAccount?> Authenticate(string Credit_number, string Password)
    {
        var account = await GetAccountByCredit_Number(Credit_number);
        if (BC.Verify(Password, account.AccountPassword))
        {
            return account;
        }
        return null;
    }

    public async Task CreateAccount(BankAccount account)
    {
        await _context.Accounts.AddAsync(account);
    }

    public void DeleteAccount(BankAccount account)
    {
        _context.Accounts.Remove(account);
    }

    public async Task<BankAccount?> GetAccount(Guid ID)
    {
        return await _context.Accounts.AsQueryable().Where(A => A.Id == ID).FirstOrDefaultAsync();
    }

    public async Task<BankAccount?> GetAccountByCredit_Number(string Credit_number)
    {
        return await _context.Accounts.AsQueryable().Where(A => A.CreditNumber.Equals(Credit_number)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<BankAccount>> GetAllAccounts()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<decimal> GetBalance(string Credit_number)
    {
        return await _context.Accounts.AsQueryable().Where(A => A.CreditNumber == Credit_number).Select(A => A.Balance).FirstOrDefaultAsync();
    }

    public void UpdateAccount(BankAccount account)
    {
        _context.Accounts.Update(account);
    }
}
