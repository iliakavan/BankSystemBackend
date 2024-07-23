using BankSystem.Domain.Models;

namespace BankSystem.Application.Repositories;



public interface ILoanRepository
{
    Task AddRequest(Loan loan);

    Task<IEnumerable<Loan>> GetLoans();

    Task<IEnumerable<Loan>> GetAccountLoans(Guid AccountID);
}
