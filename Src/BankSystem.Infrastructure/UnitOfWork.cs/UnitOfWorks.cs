using BankSystem.Application.Repositories;
using BankSystem.Application.UnitOfWork;
using BankSystem.Infrastructure.Data;

namespace BankSystem.Infrastructure.UnitOfWork.cs;


public class UnitOfWorks
    (
        AppDbContext context,
        IUserRepository userRepository,
        IBankAccountRepository bankAccountRepository,
        IRefreshTokenRepository tokenRepository,
        ILoanRepository loanRepository,
        IRoleRepository roleRepository,
        ITransactionHistoryRepository historyRepository
    ) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;
    private readonly IRefreshTokenRepository _tokenRepository = tokenRepository;
    private readonly ILoanRepository _loanRepository = loanRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly ITransactionHistoryRepository _TransactionhistoryRepository = historyRepository;

    public IBankAccountRepository BankAccount => _bankAccountRepository;

    public IRefreshTokenRepository RefreshToken => _tokenRepository;

    public IUserRepository Users => _userRepository;

    public ILoanRepository Loans => _loanRepository;

    public IRoleRepository Roles => _roleRepository;

    public ITransactionHistoryRepository TransactionHistory => _TransactionhistoryRepository;

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
