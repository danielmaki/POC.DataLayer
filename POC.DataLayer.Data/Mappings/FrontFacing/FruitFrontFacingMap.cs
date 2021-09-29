using System;

using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Models;

namespace POC.DataLayer.Data.Mappings.FrontFacing
{
    public class FruitFrontFacingMap : DataMap<Fruit, FruitDto>
    {
        public override Fruit ToModel(FruitDto ext)
        {
            return new Fruit()
            {
                Id = ext.Id,
                Name = ext.Name,
                Color = ext.Color,
                Taste = (Taste)Enum.Parse(typeof(Taste), ext.Taste)
            };
        }

        public override FruitDto ToExternal(Fruit model)
        {
            return new FruitDto()
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
                Taste = model.Taste.ToString()
            };
        }

        public override void UpdateExternal(FruitDto ext, FruitDto update)
        {
            ext.Name = update.Name;
            ext.Color = update.Color;
            ext.Taste = update.Taste;
        }
    }
}
