using FluentValidation;

namespace BankSystem.Application.Authentication.Qurey.Login;

public class LoginQureyValidator : AbstractValidator<LoginQurey>
{
    public LoginQureyValidator()
    {
        RuleFor(u => u).NotNull().WithMessage("user is not set.");

        RuleFor(u => u.UsernameOrEmail).NotEmpty().NotNull()
            .WithMessage("User name can't be empty or null");
        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Your password cannot be empty");

    }
}
