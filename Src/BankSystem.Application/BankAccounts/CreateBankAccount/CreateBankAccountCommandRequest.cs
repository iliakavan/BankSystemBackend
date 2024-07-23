using BankSystem.Application.Common.Response;
using MediatR;

namespace BankSystem.Application.Commands.AccountCommands.CreateAccountCommand;

public class CreateBankAccountCommandRequest : IRequest<ResultResponse>
{
    public required string firstname { get; set; }
    public required string lastname { get; set; }
    public decimal Balance { get; set; }
    public required string AccountPassword { get; set; }
    public Guid UserId { get; set; }

}
