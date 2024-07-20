using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Persistance.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable(TableNames.Addresses, Schemas.Misc);

            builder.HasKey(a => a.Id); 

            builder.Property(a => a.Country)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.City)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.Street)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(a => a.ZipCode)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(20)
                .IsRequired();

            
            builder.HasOne(a => a.Client)
                .WithOne(c => c.Address)
                .HasForeignKey<Address>(a => a.ClientId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
