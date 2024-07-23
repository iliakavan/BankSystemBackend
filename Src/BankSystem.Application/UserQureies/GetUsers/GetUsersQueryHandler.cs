using BankSystem.Application.Common.Response;
using BankSystem.Application.Repositories;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.UserQureies.GetUsers;


public class GetUsersQueryHandler(IUnitOfWork uow) : IRequestHandler<GetUsersQureyRequest, List<User>>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<List<User>> Handle(GetUsersQureyRequest request, CancellationToken cancellationToken)
    {
        var users = await _uow.Users.GetUsersBankAccountsOrderByFullname();
        return users.ToList();
    }
}
