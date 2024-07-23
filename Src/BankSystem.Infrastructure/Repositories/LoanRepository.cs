using BankSystem.Application.Repositories;
using BankSystem.Domain.Models;
using BankSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BankSystem.Infrastructure.Repositories;


public class LoanRepository(AppDbContext context) : ILoanRepository
{
    private readonly AppDbContext _context = context;
    public async Task AddRequest(Loan loan)
    {
        await _context.Loans.AddAsync(loan);
    }

    public async Task<IEnumerable<Loan>> GetAccountLoans(Guid AccountID)
    {
        var loans = await _context.Loans.Where(A => A.BankAccountID == AccountID).ToListAsync();
        return loans;
    }

    public async Task<IEnumerable<Loan>> GetLoans()
    {
        return await _context.Loans.ToListAsync();
    }
}
