using BankSystem.Domain.Enum;


namespace BankSystem.Domain.Models;


public class Transactionhistory
{
    public int Id { get; set; }
    public Guid AccountID {  get; set; }
    public string? OrginCreditNumber {  get; set; }
    public BankAccount Account { get; set; }
    public string? DestCreditNumber { get; set; }
    public DateTime Created { get; set; }
    public TransactionStatus Status { get; set; }
    public decimal Amount { get; set; }
    public string? Job {  get; set; }

}
