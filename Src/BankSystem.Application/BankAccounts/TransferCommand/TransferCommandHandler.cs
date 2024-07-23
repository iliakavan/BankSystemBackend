using BankSystem.Application.BackgroundServices;
using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using MediatR;

namespace BankSystem.Application.BankAccounts.TransferCommand;


public class TransferCommandHandler(ITransaction transaction,IUnitOfWork uow) : IRequestHandler<TransferCommandRequest, ResultResponse>
{
    private readonly ITransaction _transaction = transaction;
    private readonly IUnitOfWork _uow = uow;
    public async Task<ResultResponse> Handle(TransferCommandRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return new() { Success = false };
        }

        var transaction = await _transaction.Transfer(request.creditnumber, request.password,request.creditnumber2 ,request.amount);

        if (transaction.Success)
        {

            return new() { Success = true };
        }
        return new() { Success = false };
    }
}
