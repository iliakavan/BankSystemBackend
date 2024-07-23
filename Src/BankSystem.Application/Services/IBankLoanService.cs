using BankSystem.Application.Common.Response;

namespace BankSystem.Application.Services;


public interface IBankLoanService
{
    Task<ResultResponse> RequestLoan(string username, string password,string CreditNumber,decimal RequestedAmountOfloan);


}
