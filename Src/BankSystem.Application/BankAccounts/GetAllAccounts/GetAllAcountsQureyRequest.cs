using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.BankAccounts.GetAllAccounts;


public class GetAllAcountsQureyRequest : IRequest<IEnumerable<BankAccount>>
{
}
