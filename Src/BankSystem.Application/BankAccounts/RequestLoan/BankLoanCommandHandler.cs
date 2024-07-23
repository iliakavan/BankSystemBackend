using BankSystem.Application.Commands.AccountCommands.CreateAccountCommand;
using BankSystem.Application.Common.Response;
using BankSystem.Application.Services;
using MediatR;

namespace BankSystem.Application.BankAccounts.RequestLoan;


public class BankLoanCommandHandler(IBankLoanService loanService) : IRequestHandler<BankLoanCommandRequest, ResultResponse>
{
    private readonly IBankLoanService _loanService = loanService;
    public async Task<ResultResponse> Handle(BankLoanCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _loanService.RequestLoan(request.username, request.password, request.CreditNumber, request.RequestedAmountOfloan);

        if (!result.Success) 
        {
            return new ResultResponse { Success = false , Massage = result.Massage};
        }
        return new ResultResponse { Success = true, Massage = result.Massage };
    }
}
