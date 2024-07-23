using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace BankSystem.Application.Authentication.Command.RegisterUser;

public class RegisterUserCommandHandler(IUnitOfWork uow) : IRequestHandler<RegisterUserCommand, ResultResponse>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<ResultResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return new ResultResponse() { Success = false };
        }
        User user = new User()
        {
            First_Name = request.firstname,
            Last_Name = request.lastname,
            Email = request.emailaddress,
            UserName = request.UserName,
            Password = BC.HashPassword(request.password),
        };
        await _uow.Users.CreateAsync(user);
        await _uow.Save();
        return new ResultResponse() { Success = true};
    }
}
