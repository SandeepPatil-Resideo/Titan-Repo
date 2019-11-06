using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Titan.UFC.Addresses.API.Entities.Configurations;

namespace Titan.UFC.Addresses.API.Entities
{
    /// <summary>
    /// Entity context class
    /// </summary>
    public partial class AddressContext : DbContext
    {
        public AddressContext(DbContextOptions<AddressContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<CountryStateEntity> CountryStateEntities { get; set; }

        /// <summary>
        /// Model Builder
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CountryStateConfiguration());           
        }
    }
}
