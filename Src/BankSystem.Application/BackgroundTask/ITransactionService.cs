using BankSystem.Domain.Models;

namespace BankSystem.Application.BackgroundTask;



public interface ITransactionService
{
    Task CheckAndProcessTransactions();
    Task ProcessTransactionAsync(Transactionhistory transaction);
}
