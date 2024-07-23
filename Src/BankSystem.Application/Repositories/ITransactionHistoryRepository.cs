using BankSystem.Domain.Models;

namespace BankSystem.Application.Repositories;



public interface ITransactionHistoryRepository 
{
    Task CreateTransactionHistory(Transactionhistory transaction);

    void Update(Transactionhistory transaction);

    Task<IEnumerable<Transactionhistory>> GetAllTransactionsOfAccount(Guid ID);

    Task<IEnumerable<Transactionhistory>> GetPendingTransactions();
}
