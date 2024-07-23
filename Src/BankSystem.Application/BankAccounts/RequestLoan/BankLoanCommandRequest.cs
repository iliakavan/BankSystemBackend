using BankSystem.Application.Common.Response;
using MediatR;

namespace BankSystem.Application.BankAccounts.RequestLoan;



public record BankLoanCommandRequest(string username, string password, string CreditNumber, decimal RequestedAmountOfloan) : IRequest<ResultResponse>;
