using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.BankAccounts.GetAllAccounts;


public class GetAllAcountQureyHandler(IUnitOfWork uow) : IRequestHandler<GetAllAcountsQureyRequest, IEnumerable<BankAccount>>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<IEnumerable<BankAccount>> Handle(GetAllAcountsQureyRequest request, CancellationToken cancellationToken)
    {
        var Accounts = await _uow.BankAccount.GetAllAccounts();
        return Accounts;
    }
}
