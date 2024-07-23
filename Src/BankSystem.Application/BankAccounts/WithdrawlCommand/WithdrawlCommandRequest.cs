using BankSystem.Application.Common.Response;
using MediatR;

namespace BankSystem.Application.BankAccounts.WithdrawlCommand;

public record WithdrawlCommandRequest(string creditnumber, string password, decimal amount) : IRequest<ResultResponse>;

