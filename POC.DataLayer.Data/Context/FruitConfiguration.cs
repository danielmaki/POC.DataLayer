using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using POC.DataLayer.Data.Context.Abstractions;
using POC.DataLayer.Data.Models;

namespace POC.DataLayer.Data.Context
{
    public class FruitConfiguration : IEntityTypeConfiguration<FruitEntity>, IDbContextEntityConfig
    {
        public void ApplyConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        public void Configure(EntityTypeBuilder<FruitEntity> entity)
        {
            // Table name
            entity.ToTable("Fruits");

            // Keys
            entity.HasKey(p => p.Id);

            // Properties
            entity.Property(p => p.Name).HasMaxLength(64).IsRequired();
            entity.Property(p => p.Color).HasMaxLength(32).HasDefaultValue("Unknown");
            entity.Property(p => p.Taste).HasDefaultValue(0);

            // Indexes
            entity.HasIndex(p => p.Name);
        }
    }

    public partial class ApplicationDbContext
    {
        public DbSet<FruitEntity> Fruits { get; set; }

        public IDbContextEntityConfig FruitConfig => new FruitConfiguration();
    }
}
