using BankSystem.Application.Common.Response;
using MediatR;

namespace BankSystem.Application.BankAccounts.TransferCommand;



public record TransferCommandRequest(string creditnumber, string password, string creditnumber2, decimal amount) : IRequest<ResultResponse>;

