using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.BankAccounts.FetchAllLoanRequestofAccount;


public class GetAllAccountLoanHandler(IUnitOfWork uow) : IRequestHandler<GetAllAccountLoanRequests, ResultResponses<Loan>>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<ResultResponses<Loan>> Handle(GetAllAccountLoanRequests request, CancellationToken cancellationToken)
    {
        var Loan = await _uow.Loans.GetAccountLoans(request.ID);
        return new() { Result = Loan ,Success = true };
    }
}
