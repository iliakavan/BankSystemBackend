using BankSystem.Application.Common.Response;
using MediatR;

namespace BankSystem.Application.Authentication.Commands.AddUserRole;


public sealed class AddUserRoleCommand : IRequest<ResultResponse>
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
}
