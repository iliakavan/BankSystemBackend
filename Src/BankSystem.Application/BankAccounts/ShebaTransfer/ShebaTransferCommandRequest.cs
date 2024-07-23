using BankSystem.Application.Common.Response;
using BankSystem.Domain.Enum;
using BankSystem.Domain.Models;
using MediatR;

namespace BankSystem.Application.BankAccounts.ShebaTransfer;


public class ShebaTransferCommandRequest : IRequest<ResultResponse>
{
    public string Password {  get; set; }
    public string OrginCreditNumber { get; set; }
    public string DestCreditNumber { get; set; }
    public decimal Amount { get; set; }
}
