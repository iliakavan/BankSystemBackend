using BankSystem.Application.CollectTransaction;
using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;

namespace BankSystem.Infrastructure.ExternalServices;


public class CollectTransactionService(IUnitOfWork uow) : ICollectTransactionService
{
    private readonly IUnitOfWork _unitOfWork = uow;
    public async Task CollectTransaction(Guid AccountID, string OrginCreditNumber, string? DestCreditNumber , decimal Amount, string? Job)
    {
        var tran = new Transactionhistory()
        {
            AccountID = AccountID,
            OrginCreditNumber = OrginCreditNumber,
            DestCreditNumber = DestCreditNumber,
            Created = DateTime.UtcNow,
            Amount = Amount,
            Job = Job,
            Status = Domain.Enum.TransactionStatus.Complete
        };
        await _unitOfWork.TransactionHistory.CreateTransactionHistory(tran);
        await _unitOfWork.Save();
        
    }
}
