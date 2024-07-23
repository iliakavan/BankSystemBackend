using BankSystem.Domain.Models;
using BankSystem.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using BankSystem.Infrastructure.SeedData;

namespace BankSystem.Infrastructure.Data;


public class AppDbContext(DbContextOptions<AppDbContext> Option) : DbContext(Option)
{
    public DbSet<BankAccount> Accounts { get; set; }

    public DbSet<Loan> Loans { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Transactionhistory> TransactionHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AccountConfig());
        modelBuilder.ApplyConfiguration(new RoleConfig());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfig());
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new TransactionHistoryConfig());
        modelBuilder.SeedDataModel();
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(ur => ur.UserId);
    }

}
