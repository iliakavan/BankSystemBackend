
using BankSystem.Application.Services;
using MediatR;

namespace BankSystem.Application.Authentication.Command.Logout;


public class LogoutUserCommandHandler(IUserManagerService userManager) : IRequestHandler<LogoutUserCommand,string>
{
    private readonly IUserManagerService _userManager = userManager;
    public async Task<string> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _userManager.Delete(request.username);
        
        return $"{request.username} Logout Successfully.";
        
    }
}
