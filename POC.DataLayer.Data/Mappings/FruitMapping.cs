using System;

using POC.DataLayer.Data.DTO;
using POC.DataLayer.Data.ORM;
using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Models;

namespace POC.DataLayer.Data.Mappings
{
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
