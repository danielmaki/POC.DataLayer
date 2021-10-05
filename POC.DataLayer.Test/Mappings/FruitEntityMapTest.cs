using Xunit;

using POC.DataLayer.Data.Mappings;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Test.Unit.Mappings.Abstractions;
using POC.DataLayer.Data.Test.Unit.Stub;

namespace POC.DataLayer.Data.Test.Unit.Mappings
{
    public class FruitEntityMapTest : MappingTest<FruitEntityMap, Fruit, FruitEntity>
    {
        [Theory]
        [ClassData(typeof(MapFruitToEntityTestData))]
        public override void ToExternal(Fruit model, FruitEntity entity)
        {
            base.ToExternal(model, entity);
        }

        [Theory]
        [ClassData(typeof(MapEntityToFruitTestData))]
        public override void ToModel(FruitEntity entity, Fruit model)
        {
            base.ToModel(entity, model);
        }

        [Theory]
        [ClassData(typeof(UpdateFruitEntityTestData))]
        public override void UpdateExternal(FruitEntity init, FruitEntity update, FruitEntity expected)
        {
            base.UpdateExternal(init, update, expected);
        }
    }
}
