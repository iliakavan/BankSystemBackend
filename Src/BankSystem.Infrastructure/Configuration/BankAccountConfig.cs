using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankSystem.Domain.Models;

namespace BankSystem.Infrastructure.Configuration;

internal sealed class AccountConfig : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.Property(A => A.Balance)
                .IsRequired();

        builder.Property(A => A.CreditNumber).HasMaxLength(76).IsRequired();

        builder.Property(A => A.AccountPassword).HasMaxLength(72).IsRequired();

        builder.Property(A => A.Cvv2).HasMaxLength(4).IsRequired();

    }
}