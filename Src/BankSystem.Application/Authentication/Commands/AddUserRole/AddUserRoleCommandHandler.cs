using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.Authentication.Commands.AddUserRole;

public sealed class AddUserRoleCommandHandler(IUnitOfWork uow) : IRequestHandler<AddUserRoleCommand, ResultResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<ResultResponse> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _uow.Users.FindAsync(request.UserId);
        if (user is null)
        {
            return new() { Massage = "user not found!", Success = false };
        }

        if (user.Roles.Any(u => u.Role.Id == request.RoleId))
        {
            return new() { Massage = "this role already exist!", Success = false };
        }

        var role = await _uow.Roles.FindAsync(request.RoleId);
        if (role is null)
        {
            return new() { Massage = "role not found!", Success = false };
        }

        UserRole userRole = new() { User = user, Role = role };
        _uow.Users.AddRole(userRole);

        await _uow.Save();

        return new() { Massage = $"The drawing with this ID {role.Id} has been successfully added", Success = true };
    }
}
