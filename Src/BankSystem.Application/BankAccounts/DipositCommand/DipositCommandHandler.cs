using BankSystem.Application.BackgroundServices;
using BankSystem.Application.CollectTransaction;
using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.BankAccounts.DipositCommand;


public class DipositCommandHandler(ITransaction transaction, IUnitOfWork _unitOfWork) : IRequestHandler<DipositCommandRequest, ResultResponse>
{
    private readonly ITransaction _transaction = transaction;

    public async Task<ResultResponse> Handle(DipositCommandRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return new() { Success = false };
        }

        var transaction = await _transaction.Diposit(request.creditnumber,request.password,request.amount);

        if(transaction.Success)
        {
            return new() { Success =  true };
        }
        return new() { Success = false };
    }
}
