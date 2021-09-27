using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Mappings;

namespace POC.DataLayer.Data
{
    public interface IRequiredProperties
    {
        public long Id { get; set; }
    }

    public class FruitDTO : IRequiredProperties
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public string Taste { get; set; }
    }

    public class FruitModel : IRequiredProperties
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public Taste Taste { get; set; }
    }

    public class FruitORM : IRequiredProperties
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public int Taste { get; set; }
    }

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

    public class FruitMapping : DataMapping<FruitModel, FruitORM, FruitDTO>
    {
        public override FruitORM CopyORM(FruitORM orm)
        {
            return new FruitORM()
            {
                Id = 0,
                Name = orm.Name,
                Color = orm.Color,
                Taste = orm.Taste
            };
        }

        public override FruitModel DTOToModel(FruitDTO dto)
        {
            return new FruitModel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Color = dto.Color,
                Taste = (Taste)Enum.Parse(typeof(Taste), dto.Taste)
            };
        }

        public override FruitDTO ModelToDTO(FruitModel model)
        {
            return new FruitDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
                Taste = model.Taste.ToString()
            };
        }

        public override FruitORM ModelToORM(FruitModel model)
        {
            return new FruitORM()
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
                Taste = (int)model.Taste
            };
        }

        public override FruitModel ORMToModel(FruitORM orm)
        {
            return new FruitModel()
            {
                Id = orm.Id,
                Name = orm.Name,
                Color = orm.Color,
                Taste = (Taste)orm.Taste
            };
        }

        public override void UpdateORM(FruitORM entity, FruitORM update)
        {
            // Note: Do not include Id
            entity.Name = update.Name;
            entity.Color = update.Color;
            entity.Taste = update.Taste;
        }
    }
}
