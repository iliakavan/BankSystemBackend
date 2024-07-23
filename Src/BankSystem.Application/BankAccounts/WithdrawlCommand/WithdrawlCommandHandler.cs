using BankSystem.Application.BackgroundServices;
using BankSystem.Application.Common.Response;
using MediatR;

namespace BankSystem.Application.BankAccounts.WithdrawlCommand;



public class WithdrawlCommandHandler(ITransaction transaction) : IRequestHandler<WithdrawlCommandRequest, ResultResponse>
{
    private readonly ITransaction _transaction = transaction; 
    public async Task<ResultResponse> Handle(WithdrawlCommandRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return new() { Success = false };
        }

        var transaction = await _transaction.Withdrawal(request.creditnumber, request.password, request.amount);

        if (transaction.Success)
        {
            return new() { Success = true };
        }
        return new() { Success = false };
    }
}
