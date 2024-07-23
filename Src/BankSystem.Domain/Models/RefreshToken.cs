using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.Domain.Models;

public class RefreshToken
{
    public Guid Id { get; set; }

    [ForeignKey(name: "UserId")]
    public Guid UserId { get; set; }
    public User User { get; set; }
    public required string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public DateTime CreatedAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public bool IsActive => RevokedAt == null && !IsExpired;

}
