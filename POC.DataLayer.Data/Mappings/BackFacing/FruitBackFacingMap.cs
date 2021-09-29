using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Models;

namespace POC.DataLayer.Data.Mappings.BackFacing
{
    public class FruitBackFacingMap : DataMap<Fruit, FruitEntity>
    {
        public override Fruit ToModel(FruitEntity ext)
        {
            return new Fruit()
            {
                Id = ext.Id,
                Name = ext.Name,
                Color = ext.Color,
                Taste = (Taste)ext.Taste
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

        public override void UpdateExternal(FruitEntity ext, FruitEntity update)
        {
            ext.Name = update.Name;
            ext.Color = update.Color;
            ext.Taste = update.Taste;
        }
    }
}
