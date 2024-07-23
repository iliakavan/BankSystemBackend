using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystem.Infrastructure.Configuration;


internal sealed class RoleConfig : IEntityTypeConfiguration<Role>
{ 
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(R => R.Name).HasMaxLength(25).IsRequired();
    }
}
