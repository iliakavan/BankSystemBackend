using BankSystem.Application.Common.Response;
using MediatR;

namespace BankSystem.Application.Commands.AccountCommands.DeleteAccountCommand;


public class DeleteCommandRequest : IRequest<ResultResponse>
{
    public Guid Id { get; set; }

}
