using System.ComponentModel.DataAnnotations;

namespace BankSystem.Domain.Models;



public class User
{
    public Guid ID { get; set; }
    public required string First_Name { get; set; }
    public required string Last_Name { get; set; }
    public string Fullname
    {
        get { return $"{First_Name} {Last_Name}"; }
    }

    public bool IsDelete { get; set; }


    [EmailAddress]
    public required string Email { get; set; }
    public required string UserName {  get; set; }
    public required string Password {  get; set; }


    public ICollection<UserRole> Roles { get; set; }
    public Guid RefreshTokenID { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public ICollection<BankAccount> BankAccounts { get; set; }

}
