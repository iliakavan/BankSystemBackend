using BankSystem.Application.Common.Response;
using BankSystem.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.BankAccounts.FetchAllLoanRequestofAccount;

public class GetAllAccountLoanRequests : IRequest<ResultResponses<Loan>>
{
    public Guid ID { get; set; }

}

