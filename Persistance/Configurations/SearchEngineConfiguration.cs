using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistance.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configurations
{
    public class SearchEngineConfiguration : IEntityTypeConfiguration<SearchEngine>
    {
        public void Configure(EntityTypeBuilder<SearchEngine> builder)
        {
            builder.ToTable(TableNames.SearchEngine,Schemas.Clients);
            builder.HasKey(c => c.Id);
            builder.Property(c => c.SearchField);
            builder.Property(c => c.UserId);
            builder.Property(c => c.SearchDate).HasColumnType(DBTypes.DateTime);
            builder.Property(c => c.SortOption);
            builder.Property(c => c.PageNumber);
            builder.Property(c => c.PageSize);
            builder.Property(c => c.PersonalId);

        }
    }
}
