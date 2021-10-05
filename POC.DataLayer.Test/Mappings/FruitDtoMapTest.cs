using Xunit;

using POC.DataLayer.Data.Mappings;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Test.Unit.Mappings.Abstractions;
using POC.DataLayer.Data.Test.Unit.Stub;

namespace POC.DataLayer.Data.Test.Unit.Mappings
{
    public class FruitDtoMapTest : MappingTest<FruitDtoMap, Fruit, FruitDto>
    {
        [Theory]
        [ClassData(typeof(MapFruitToDtoTestData))]
        public override void ToExternal(Fruit model, FruitDto dto)
        {
            base.ToExternal(model, dto);
        }

        [Theory]
        [ClassData(typeof(MapDtoToFruitTestData))]
        public override void ToModel(FruitDto dto, Fruit model)
        {
            base.ToModel(dto, model);
        }

        [Theory]
        [ClassData(typeof(UpdateFruitDtoTestData))]
        public override void UpdateExternal(FruitDto init, FruitDto update, FruitDto expected)
        {
            base.UpdateExternal(init, update, expected);
        }
    }
}
