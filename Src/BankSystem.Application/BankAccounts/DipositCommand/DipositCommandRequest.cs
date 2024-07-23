using BankSystem.Application.Common.Response;
using MediatR;

namespace BankSystem.Application.BankAccounts.DipositCommand;



public record DipositCommandRequest(string creditnumber, string password, decimal amount) : IRequest<ResultResponse>;
