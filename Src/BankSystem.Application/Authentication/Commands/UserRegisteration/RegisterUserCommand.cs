using BankSystem.Application.Common.Response;
using MediatR;

namespace BankSystem.Application.Authentication.Command.RegisterUser;

public record RegisterUserCommand(string firstname,string lastname,string emailaddress, string UserName, string password): IRequest<ResultResponse>;

