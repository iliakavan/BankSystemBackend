using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using MediatR;

namespace BankSystem.Application.Commands.AccountCommands.DeleteAccountCommand;



public class DeleteCommandHandler(IUnitOfWork uow) : IRequestHandler<DeleteCommandRequest, ResultResponse>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<ResultResponse> Handle(DeleteCommandRequest request, CancellationToken cancellationToken)
    {
        var Account = await _uow.BankAccount.GetAccount(request.Id);
        if (Account != null)
        {
            _uow.BankAccount.DeleteAccount(Account);
            await _uow.Save();
            return new ResultResponse { Success = true };
        }
        return new ResultResponse { Success = false };
    }
}
