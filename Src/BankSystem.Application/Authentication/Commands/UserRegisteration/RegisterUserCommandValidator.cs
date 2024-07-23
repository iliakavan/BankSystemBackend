using BankSystem.Application.UnitOfWork;
using FluentValidation;

namespace BankSystem.Application.Authentication.Command.RegisterUser;


public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IUnitOfWork _uow;
    public RegisterUserCommandValidator(IUnitOfWork uow)
    {
        _uow = uow;
        RuleFor(u => u.firstname)
            .NotNull().WithMessage("Set your information.");
        
        RuleFor(u => u.lastname)
            .NotNull().WithMessage("Set your information.");

        RuleFor(u => u.UserName)
            .MustAsync(UserNameAlreadyExist).WithMessage("This user name already exist.");

        RuleFor(u => u.emailaddress)
            .NotEmpty().WithMessage("Your Email cannot be empty.")
            .EmailAddress().WithMessage("Your email not valid.");

        RuleFor(u => u.password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(6).WithMessage("Your password length must be at least 6.")
            .MaximumLength(12).WithMessage("Your password length must not exceed 12.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");

    }

    private async Task<bool> UserNameAlreadyExist(string userName, CancellationToken cancellationToken) 
    {
        var result = await _uow.Users.GetUserByUserName(userName);
        if (result == null) 
        {
            return true;
        }
        return false;
    }
       
}
