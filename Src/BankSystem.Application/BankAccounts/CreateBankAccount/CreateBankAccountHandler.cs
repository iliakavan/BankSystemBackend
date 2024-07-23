using BankSystem.Application.Common.GenratingUnique;
using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using MediatR;
using BC = BCrypt.Net.BCrypt;
namespace BankSystem.Application.Commands.AccountCommands.CreateAccountCommand;



public class CreateBankAccountHandler(IUnitOfWork uow) : IRequestHandler<CreateBankAccountCommandRequest, ResultResponse>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<ResultResponse> Handle(CreateBankAccountCommandRequest request, CancellationToken cancellationToken)
    {
        if (request == null) 
        {
            return new ResultResponse { Success = false };
        }
        BankAccount account = new()
        {
            Owner_Name = $"{request.firstname} {request.lastname}",
            AccountPassword = BC.HashPassword(request.AccountPassword),
            CreditNumber = UniqueString.GetUniqueKey(16),
            Balance = request.Balance,
            Cvv2 = UniqueString.GetUniqueKey(4),
            UserID = request.UserId
        };
        
        await _uow.BankAccount.CreateAccount(account);
        
        await _uow.Save();

        return new ResultResponse { Success = true };
    }
    
}
