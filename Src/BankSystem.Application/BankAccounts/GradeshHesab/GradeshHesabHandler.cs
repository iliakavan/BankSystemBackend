using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.BankAccounts.GradeshHesab;



public class GradeshHesabHandler(IUnitOfWork uow) : IRequestHandler<GradeshHesabRequest, ResultResponses<Transactionhistory>>
{
    private readonly IUnitOfWork _unitOfWork = uow;
    public async Task<ResultResponses<Transactionhistory>> Handle(GradeshHesabRequest request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.BankAccount.Authenticate(request.CreditNumber, request.Password);

        if(account == null) 
        {
            return new() { Success = false };
        }
        var transations = await _unitOfWork.TransactionHistory.GetAllTransactionsOfAccount(account.Id);
        return new()
        {
            Success = true,
            Result = transations
        };
    }
}
