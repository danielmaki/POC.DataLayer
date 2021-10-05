using System;

using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Models;

namespace POC.DataLayer.Data.Mappings
{
    public class FruitEntityMap : DataMap<Fruit, FruitEntity>
    {
        public override Fruit ToModel(FruitEntity ext)
        {
            return new Fruit()
            {
                Id = ext.Id,
                Name = ext.Name,
                Color = ext.Color,
                Taste = Enum.IsDefined(typeof(Taste), ext.Taste) ? (Taste)ext.Taste : Taste.Unknown
            };
        }

        public override FruitEntity ToExternal(Fruit model)
        {
            return new FruitEntity()
            {
                Id = model.Id,
                Name = model.Name,
                Color = model.Color,
                Taste = (int)model.Taste
            };
        }
    }
}
