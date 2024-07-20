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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {

            builder.ToTable(TableNames.Clients, Schemas.Clients);

            builder.HasKey(c => c.Id); 
            
            builder.Property(c => c.Id)
                .HasColumnType(DBTypes.Int)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.FirstName)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(c => c.PersonalId)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(c => c.ProfilePhotoUrl)
                .HasColumnType(DBTypes.NvarcharMax);

            builder.Property(c => c.MobileNumber)
                .HasColumnType(DBTypes.Nvarchar)
                .HasMaxLength(20);

            builder.Property(c => c.Gender)
                .HasColumnType(DBTypes.Int)
                .IsRequired();

            builder.HasOne(c => c.Address)
                .WithOne(a => a.Client)
                .HasForeignKey<Address>(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Accounts)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId)           
                .IsRequired();
        }
    }
}
