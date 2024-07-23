namespace BankSystem.Domain.Models;


public class BankAccount
{
    public Guid Id { get; set; }
    public required string CreditNumber { get; set; }
    public required string Owner_Name { get; set; }
    public required string AccountPassword { get; set; }
    public required decimal Balance { get; set; }
    public required string Cvv2 {  get; set; }
    public bool IsBlocked { get; set; }
    public ICollection<Loan> Loans { get; set; }
    public ICollection<Transactionhistory> TransactionHistory { get; set; }
    public User User { get; set; }
    public Guid UserID { get; set; }

}
