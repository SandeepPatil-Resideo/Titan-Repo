using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Titan.Ufc.Addresses.API.Entities.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("Address", "dbo");


            builder.Property(e => e.Address1).HasMaxLength(255);

            builder.Property(e => e.Address2).HasMaxLength(255);

            builder.Property(e => e.AddressLine3).HasMaxLength(255);

            builder.Property(e => e.City).HasMaxLength(50);

            builder.Property(e => e.ContactName).HasMaxLength(255);

            builder.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.CountryCode).HasMaxLength(3);

            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e => e.Latitude).HasMaxLength(100);

            builder.Property(e => e.Longitude).HasMaxLength(100);

            builder.Property(e => e.PinCode).HasMaxLength(10);

            builder.Property(e => e.StateID).HasMaxLength(3);

            builder.Property(e => e.UpdatedDate).HasColumnType("datetime");            

            builder.Property(e => e.AddressUID).HasColumnName("AddressUID");
        }
    }
}
