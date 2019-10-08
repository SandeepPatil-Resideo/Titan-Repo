using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanTemplate.titanaddressapi.Entities.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("Address", "ADDRESS");

            builder.HasIndex(e => e.AddressId)
                .HasName("UK_AddressId")
                .IsUnique();

            builder.Property(e => e.AddressLine1).HasMaxLength(255);

            builder.Property(e => e.AddressLine2).HasMaxLength(255);

            builder.Property(e => e.AddressLine3).HasMaxLength(255);

            builder.Property(e => e.City).HasMaxLength(50);

            builder.Property(e => e.ContactName).HasMaxLength(255);

            builder.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.CountryCode).HasMaxLength(3);

            builder.Property(e => e.CreatedOn).HasColumnType("datetime");

            builder.Property(e => e.Latitude).HasMaxLength(100);

            builder.Property(e => e.Longitude).HasMaxLength(100);

            builder.Property(e => e.ZipPostalCode).HasMaxLength(10);

            builder.Property(e => e.StateProvinceRegion).HasMaxLength(3);

            builder.Property(e => e.UpdatedOn).HasColumnType("datetime");

            builder.Property(e => e.AddressId).HasColumnName("AddressId");
        }
    }
}
