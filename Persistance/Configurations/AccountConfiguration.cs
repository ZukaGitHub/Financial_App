using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Constants;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
         
            builder.ToTable(TableNames.Accounts, Schemas.Accounts);

          
            builder.HasKey(i => i.Id);

           
            builder.Property(i => i.Id)
                .HasColumnType(DBTypes.Int)
                .IsRequired();

            builder.Property(i => i.AccountNumber)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(50)
                .IsRequired();

          

            builder.Property(i => i.Balance)
                .HasColumnType(DBTypes.Decimal)
                .IsRequired();

          
            builder.HasOne(i => i.Client)
                .WithMany(c => c.Accounts)
                .HasForeignKey(i => i.ClientId)               
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
