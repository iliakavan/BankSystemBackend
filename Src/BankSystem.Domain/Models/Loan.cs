using System.Text.Json.Serialization;

namespace BankSystem.Domain.Models;


public class Loan
{
    public int Id { get; set; }
    [JsonIgnore]
    public BankAccount BankAccount { get; set; }
    
    public Guid BankAccountID { get; set; }
    public DateTime DateRequested {  get; set; }
    public decimal Requested_Amount {  get; set; }
    public int UserValidityPoint { get; set; } = 700;
    public bool IsValid => UserValidityPoint >= 640;

}
