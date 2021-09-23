using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using POC.DataLayer.Data.ORM;

namespace POC.DataLayer.Data.Configurations
{
    public class FruitConfiguration : IEntityTypeConfiguration<FruitORM>
    {
        public void Configure(EntityTypeBuilder<FruitORM> entity)
        {
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
}
