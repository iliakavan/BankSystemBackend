using BankSystem.Domain.Models;
using System.Security.Principal;

namespace BankSystem.Application.Repositories;


public interface IBankAccountRepository
{
    Task CreateAccount(BankAccount account);

    void UpdateAccount(BankAccount account);

    void DeleteAccount(BankAccount account);
    Task<decimal> GetBalance(string Credit_number);
    Task<BankAccount?> GetAccount(Guid ID);

    Task<BankAccount?> Authenticate(string Credit_number, string Password);

    Task<IEnumerable<BankAccount>> GetAllAccounts();

    Task<BankAccount?> GetAccountByCredit_Number(string Credit_number);

}
