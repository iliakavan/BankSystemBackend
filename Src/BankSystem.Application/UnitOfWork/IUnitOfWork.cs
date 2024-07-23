using BankSystem.Application.Repositories;

namespace BankSystem.Application.UnitOfWork;

public interface IUnitOfWork
{
    IBankAccountRepository BankAccount {get;}
    IRefreshTokenRepository RefreshToken { get;}
    IUserRepository Users { get;}
    ILoanRepository Loans { get;}
    IRoleRepository Roles { get;}
    ITransactionHistoryRepository TransactionHistory { get;}
    Task Save();

}
