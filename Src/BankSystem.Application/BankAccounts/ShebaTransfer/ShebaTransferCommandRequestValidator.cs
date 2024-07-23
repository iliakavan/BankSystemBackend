using BankSystem.Application.Authentication.Command.RegisterUser;
using FluentValidation;

namespace BankSystem.Application.BankAccounts.ShebaTransfer;



public class ShebaTransferCommandRequestValidator : AbstractValidator<ShebaTransferCommandRequest>
{
    public ShebaTransferCommandRequestValidator()
    {
        RuleFor(s => s.Amount).GreaterThan(0).WithMessage("amount of money must be greater than zero"); ;
    }
}
