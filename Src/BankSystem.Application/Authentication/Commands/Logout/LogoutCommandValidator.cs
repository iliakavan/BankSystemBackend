using FluentValidation;

namespace BankSystem.Application.Authentication.Command.Logout;



public class LogoutCommandValidator : AbstractValidator<LogoutUserCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(u => u.username).NotEmpty().WithMessage("username cant be null");
    }
}
