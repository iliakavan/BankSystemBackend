using BankSystem.Application.Repositories;
using BankSystem.Domain.Enum;
using BankSystem.Domain.Models;
using BankSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Repositories;

public class TransactionHistoryRepository(AppDbContext context) : ITransactionHistoryRepository
{
    private readonly AppDbContext _context = context;
    public async Task CreateTransactionHistory(Transactionhistory transaction)
    {
        await _context.TransactionHistory.AddAsync(transaction);
    }

    public async Task<IEnumerable<Transactionhistory>> GetAllTransactionsOfAccount(Guid AccountID)
    {
        return await _context.TransactionHistory.Where(T => T.AccountID == AccountID && T.Status == TransactionStatus.Complete).ToListAsync();
    }

    public async Task<IEnumerable<Transactionhistory>> GetPendingTransactions()
    {
        return await _context.TransactionHistory.Where(t => t.Status == TransactionStatus.Pending).ToListAsync();
    }

    public void Update(Transactionhistory transaction)
    {
        _context.TransactionHistory.Update(transaction);
    }
}
