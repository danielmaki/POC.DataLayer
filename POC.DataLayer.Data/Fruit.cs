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

        //IEnumerable<object> GetProperties();
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

    public class FruitBackFacingMap : DataMap<FruitModel, FruitORM>
    {
        public override FruitORM CopyExt(FruitORM ext)
        {
            return new FruitORM()
            {
                Id = 0,
                Name = ext.Name,
                Color = ext.Color,
                Taste = ext.Taste
            };
        }

        public override FruitModel ExtToIntl(FruitORM ext)
        {
            return new FruitModel()
            {
                Id = ext.Id,
                Name = ext.Name,
                Color = ext.Color,
                Taste = (Taste)ext.Taste
            };
        }

        public override FruitORM IntlToExt(FruitModel intl)
        {
            return new FruitORM()
            {
                Id = intl.Id,
                Name = intl.Name,
                Color = intl.Color,
                Taste = (int)intl.Taste
            };
        }

        public override void UpdateExt(FruitORM ext, FruitORM update)
        {
            ext.Name = update.Name;
            ext.Color = update.Color;
            ext.Taste = update.Taste;
        }
    }

    public class FruitFrontFacingMap : DataMap<FruitModel, FruitDTO>
    {
        public override FruitDTO CopyExt(FruitDTO ext)
        {
            return new FruitDTO()
            {
                Id = 0,
                Name = ext.Name,
                Color = ext.Color,
                Taste = ext.Taste
            };
        }

        public override FruitModel ExtToIntl(FruitDTO ext)
        {
            return new FruitModel()
            {
                Id = ext.Id,
                Name = ext.Name,
                Color = ext.Color,
                Taste = (Taste)Enum.Parse(typeof(Taste), ext.Taste)
            };
        }

        public override FruitDTO IntlToExt(FruitModel intl)
        {
            return new FruitDTO()
            {
                Id = intl.Id,
                Name = intl.Name,
                Color = intl.Color,
                Taste = intl.Taste.ToString()
            };
        }

        public override void UpdateExt(FruitDTO ext, FruitDTO update)
        {
            ext.Name = update.Name;
            ext.Color = update.Color;
            ext.Taste = update.Taste;
        }
    }
}
