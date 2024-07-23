using BankSystem.Application.Common.Response;
using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.BankAccounts.GradeshHesab;

public record GradeshHesabRequest(string CreditNumber,string Password) : IRequest<ResultResponses<Transactionhistory>>;
