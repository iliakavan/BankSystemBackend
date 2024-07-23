using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Configuration;


internal sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(U => U.UserName).HasMaxLength(50).IsRequired();

        builder.Property(U => U.Password).HasMaxLength(100).IsRequired();

        builder.Property(U => U.Email).HasMaxLength(50).IsRequired();

        builder.Property(U => U.First_Name).HasMaxLength(50).IsRequired();

        builder.Property(U => U.Last_Name).HasMaxLength(50).IsRequired();

    }
}
