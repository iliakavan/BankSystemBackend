using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using MediatR;

namespace BankSystem.Application.Authentication.Commands.RemoveUserRole;


public class RemoveUserCommandHandler(IUnitOfWork uow) : IRequestHandler<RemoveUserRoleCommand, ResultResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<ResultResponse> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userRole = await _uow.Users.FindAsyncUserRole(request.Id);
        if (userRole is null)
        {
            return new() { Massage = "There is no user with this role", Success = false };
        }

        _uow.Users.RemoveUserRole(userRole);
        await _uow.Save();

        return new() { Massage = "This role has been successfully removed from the user", Success = true };
    }
}
