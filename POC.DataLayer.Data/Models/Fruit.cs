using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Data.Models
{
    public class Fruit : IModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public Taste Taste { get; set; }
    }

    public class FruitDto : IModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public string Taste { get; set; }
    }

    public class FruitEntity : IModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public int Taste { get; set; }
    }

    public class FruitConfiguration : IEntityTypeConfiguration<FruitEntity>
    {
        public void Configure(EntityTypeBuilder<FruitEntity> entity)
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
