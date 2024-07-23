using BankSystem.Application.BackgroundServices;
using BankSystem.Application.Repositories;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;

namespace BankSystem.Application.BackgroundTask;



public class TransactionService(IUnitOfWork uow, ITransaction transaction) : ITransactionService
{
    private readonly IUnitOfWork _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    private readonly ITransaction _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));

    public async Task CheckAndProcessTransactions()
    {
        var pendingTransactions = await _uow.TransactionHistory.GetPendingTransactions();

        foreach (var transactions in pendingTransactions)
        {
            await ProcessTransactionAsync(transactions);
            Console.WriteLine($"Transaction {transactions.Id} processed successfully.");
        }
    }

    public async Task ProcessTransactionAsync(Transactionhistory transaction)
    {
        var result = await _transaction.Transfer(transaction.OrginCreditNumber, transaction.DestCreditNumber, transaction.Amount);

        if (result.Success)
        {
            transaction.Status = Domain.Enum.TransactionStatus.Complete;
            _uow.TransactionHistory.Update(transaction);
        }
        else
        {
            transaction.Status = Domain.Enum.TransactionStatus.Failed;
            _uow.TransactionHistory.Update(transaction);
        }
        await _uow.Save();
    }
}

