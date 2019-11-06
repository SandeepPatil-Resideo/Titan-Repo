using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.UFC.Addresses.API.Entities.Configurations
{
    public class CountryStateConfiguration : IEntityTypeConfiguration<CountryStateEntity>
    {
        public void Configure(EntityTypeBuilder<CountryStateEntity> builder)
        {
            
            builder.ToTable("CountryState", "dbo");
            builder.Property(e => e.StateName).HasMaxLength(50);
            builder.Property(e => e.CountryCode).HasMaxLength(2);
            builder.Property(e => e.AbbreviatedName).HasMaxLength(2);


        }
    }
}
