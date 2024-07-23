using BankSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Infrastructure.Configuration;

public class TransactionHistoryConfig : IEntityTypeConfiguration<Transactionhistory>
{
    public void Configure(EntityTypeBuilder<Transactionhistory> builder)
    {
        builder.Property(x => x.Job).HasMaxLength(50);
        builder.Property(x => x.DestCreditNumber).HasMaxLength(50);
        builder.Property(x => x.OrginCreditNumber).HasMaxLength(50);
    }
}
