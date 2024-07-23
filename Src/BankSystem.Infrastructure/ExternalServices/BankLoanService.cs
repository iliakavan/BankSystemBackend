using BankSystem.Application.Common.Response;
using BankSystem.Application.Services;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;

namespace BankSystem.Infrastructure.ExternalServices;

public class BankLoanService(IUnitOfWork uow) : IBankLoanService
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<ResultResponse> RequestLoan(string username, string password, string CreditNumber, decimal RequestedAmountOfloan)
    {
        var user = await _uow.Users.SigninUserAsync(username, password);
        if (user == null)
        {
            return new ResultResponse { Massage = "User Not Authenticated", Success = false };
        }

        var BankAccount = await _uow.BankAccount.GetAccountByCredit_Number(CreditNumber);
        if (BankAccount == null)
        {
            return new ResultResponse { Massage = "BankAccount Is not valid", Success = false };
        }

        // Additional validation for balance and blocked status
        if (BankAccount.IsBlocked)
        {
            return new ResultResponse { Massage = "BankAccount is blocked", Success = false };
        }

        if (RequestedAmountOfloan > BankAccount.Balance / 2)
        {
            return new ResultResponse { Massage = "Requested loan amount exceeds allowed limit", Success = false };
        }

        Loan loan = new Loan
        {
            DateRequested = DateTime.UtcNow,
            BankAccountID = BankAccount.Id,
            Requested_Amount = RequestedAmountOfloan
        };

        await _uow.Loans.AddRequest(loan);
        await _uow.Save();

        return new ResultResponse { Massage = "We Accepted Your Loan Request", Success = true };
    }
}
