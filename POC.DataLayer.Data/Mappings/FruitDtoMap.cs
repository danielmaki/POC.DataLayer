using System;

using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Models;

namespace POC.DataLayer.Data.Mappings
{
    public class FruitDtoMap : DataMap<Fruit, FruitDto>
    {
        public override Fruit ToModel(FruitDto ext)
        {
            return new Fruit()
            {
                Id = ext.Id,
                Name = ext.Name,
                Color = ext.Color,
                Taste = Enum.TryParse<Taste>(ext.Taste, out var taste) ? taste : Taste.Unknown
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
    }
}
