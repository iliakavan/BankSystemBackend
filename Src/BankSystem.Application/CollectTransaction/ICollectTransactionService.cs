using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;

namespace BankSystem.Application.CollectTransaction;



public interface ICollectTransactionService 
{
    Task CollectTransaction(Guid AccountID, string OrginCreditNumber, string? DestCreditNumber, decimal Amount, string? Job);
}