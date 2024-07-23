using MediatR;

namespace BankSystem.Application.Authentication.Command.Logout;


public record LogoutUserCommand(string username) : IRequest<string>;
