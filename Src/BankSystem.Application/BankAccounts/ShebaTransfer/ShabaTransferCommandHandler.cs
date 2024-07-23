using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.BankAccounts.ShebaTransfer;

public class ShabaTransferCommandHandler(IUnitOfWork uow) : IRequestHandler<ShebaTransferCommandRequest, ResultResponse>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<ResultResponse> Handle(ShebaTransferCommandRequest request, CancellationToken cancellationToken)
    {
        var account = await _uow.BankAccount.Authenticate(request.OrginCreditNumber, request.Password);
        var destaccount = await _uow.BankAccount.GetAccountByCredit_Number(request.DestCreditNumber);
        
        if (account != null && !account.IsBlocked && destaccount != null) 
        {
            var ShabaTransaction = new Transactionhistory()
            {
                AccountID = account.Id,
                Amount = request.Amount,
                DestCreditNumber = request.DestCreditNumber,
                Status = Domain.Enum.TransactionStatus.Pending,
                Created = DateTime.UtcNow,
                OrginCreditNumber = request.OrginCreditNumber
            };
            await _uow.TransactionHistory.CreateTransactionHistory(ShabaTransaction);
            await _uow.Save();
            return new() { Success = true };
        }
        return new() { Success = false };
    }
}
