using System.Threading.Tasks;

using Xunit;

using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Enums;

namespace POC.DataLayer.Data.Test.Store
{
    public class FruitDataStoreTest : IDataStoreFixture
    {
        public readonly DbContextFixture fixture;

        public FruitDataStoreTest(DbContextFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData(0, "Apple", "Red", Taste.Sweet)]
        [InlineData(0, "Lime", "Green", Taste.Sour)]
        [InlineData(0, "Unknown", "Unknown", Taste.Unknown)]
        public async Task Test1_CreateEntityAsync_Case1_ValidModel(long id, string name, string color, Taste taste)
        {
            // Setup
            var model = new FruitModel()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            var result = await fixture.service.CreateEntityAsync(model);

            // Verify
            Assert.NotEqual(model.Id, result.Id);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.Color, result.Color);
            Assert.Equal(model.Taste, result.Taste);
        }

        [Theory]
        [InlineData(0, "", null, Taste.Unknown)]
        public async Task Test1_CreateEntityAsync_Case2_DefaultValues(long id, string name, string color, Taste taste)
        {
            // Setup
            var model = new FruitModel()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            var result = await fixture.service.CreateEntityAsync(model);

            // Verify
            Assert.NotEqual(model.Id, result.Id);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal("Unknown", result.Color);
            Assert.Equal(model.Taste, result.Taste);
        }

        [Theory]
        [InlineData(0, "This name is too long to be a name for a fruit, the maximum string length is 64.", "Yellow", Taste.Sweet)]
        [InlineData(0, "Banana", "This color is too long to be a color for a fruit, the maximum string length is 32.", Taste.Sweet)]
        [InlineData(0, null, "Yellow", Taste.Sweet)]
        public async Task Test1_CreateEntityAsync_Case3_InvalidModel(long id, string name, string color, Taste taste)
        {
            // Setup
            var model = new FruitModel()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            var result = await fixture.service.CreateEntityAsync(model);

            // Verify
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet)]
        [InlineData(-1, "Lime", "Green", Taste.Sour)]
        public async Task Test1_CreateEntityAsync_Case4_InvalidId(long id, string name, string color, Taste taste)
        {
            // Setup
            var model = new FruitModel()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            var result = await fixture.service.CreateEntityAsync(model);

            // Verify
            Assert.Null(result);
        }

        [Fact]
        public async Task Test1_CreateEntityAsync_Case5_NullEntity()
        {
            // Execute
            var result = await fixture.service.CreateEntityAsync(null);

            // Verify
            Assert.Null(result);
        }

        [Theory]
        [InlineData(0, "Papaya", "Orange", Taste.Sweet)]
        public async Task Test1_CreateEntityAsync_Case6_ValidModel(long id, string name, string color, Taste taste)
        {
            // Setup
            var model = new FruitModel()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            var result = await fixture.service.CreateEntityAsync(model);

            // Verify
            Assert.NotEqual(model.Id, result.Id);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.Color, result.Color);
            Assert.Equal(model.Taste, result.Taste);
        }

        [Theory]
        [InlineData("Test", "Black", Taste.Unknown)]
        public async Task Test2_UpdateEntityAsync_Case1_ValidModel(string name, string color, Taste taste)
        {
            // Setup
            var result = await fixture.service.GetEntityListAsync();

            Assert.NotEmpty(result);

            foreach (var model in result)
            {
                model.Name = name;
                model.Color = color;
                model.Taste = taste;

                // Execute
                var updatedResult = await fixture.service.UpdateEntityAsync(model);

                // Verify
                Assert.Equal(model.Id, updatedResult.Id);
                Assert.Equal(name, updatedResult.Name);
                Assert.Equal(color, updatedResult.Color);
                Assert.Equal(taste, updatedResult.Taste);
            }
        }

        [Theory]
        [InlineData("This name is too long to be a name for a fruit, the maximum string length is 64.", "Yellow", Taste.Sweet)]
        [InlineData("Banana", "This color is too long to be a color for a fruit, the maximum string length is 32.", Taste.Sweet)]
        [InlineData(null, "Yellow", Taste.Sweet)]
        public async Task Test2_UpdateEntityAsync_Case2_InvalidModel(string name, string color, Taste taste)
        {
            // Setup
            var result = await fixture.service.GetEntityListAsync();

            Assert.NotEmpty(result);

            foreach (var model in result)
            {
                var prevModel = new FruitModel()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Color = model.Color,
                    Taste = model.Taste
                };
                model.Name = name;
                model.Color = color;
                model.Taste = taste;

                // Execute
                var updatedResult = await fixture.service.UpdateEntityAsync(model);

                // Verify
                Assert.Null(updatedResult);

                updatedResult = await fixture.service.GetEntityAsync(model.Id);

                Assert.Equal(prevModel.Id, updatedResult.Id);
                Assert.Equal(prevModel.Name, updatedResult.Name);
                Assert.Equal(prevModel.Color, updatedResult.Color);
                Assert.Equal(prevModel.Taste, updatedResult.Taste);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99999)]
        public async Task Test2_UpdateEntityAsync_Case3_InvalidId(long id)
        {
            // Execute
            var result = await fixture.service.GetEntityListAsync();

            Assert.NotEmpty(result);

            foreach (var model in result)
            {
                var prevModel = model;
                model.Id = id;

                var updatedResult = await fixture.service.UpdateEntityAsync(model);

                // Verify
                Assert.Null(updatedResult);

                updatedResult = await fixture.service.GetEntityAsync(model.Id);

                Assert.Equal(prevModel.Id, updatedResult.Id);
                Assert.Equal(prevModel.Name, updatedResult.Name);
                Assert.Equal(prevModel.Color, updatedResult.Color);
                Assert.Equal(prevModel.Taste, updatedResult.Taste);
            }
        }

        [Fact]
        public async Task Test2_UpdateEntityAsync_Case4_NullEntity()
        {
            // Execute
            var result = await fixture.service.UpdateEntityAsync(null);

            // Verify
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Lime", "Green", Taste.Sour)]
        public async Task Test2_UpdateEntityAsync_Case5_ValidModel(string name, string color, Taste taste)
        {
            // Setup
            var result = await fixture.service.GetEntityListAsync();

            Assert.NotEmpty(result);

            foreach (var model in result)
            {
                model.Name = name;
                model.Color = color;
                model.Taste = taste;

                // Execute
                var updatedResult = await fixture.service.UpdateEntityAsync(model);

                // Verify
                Assert.Equal(model.Id, updatedResult.Id);
                Assert.Equal(name, updatedResult.Name);
                Assert.Equal(color, updatedResult.Color);
                Assert.Equal(taste, updatedResult.Taste);
            }
        }

        [Fact]
        public async Task Test3_DeleteEntityAsync_Case1_ValidId()
        {
            // Execute
            var result = await fixture.service.GetEntityListAsync();

            Assert.NotEmpty(result);

            foreach (var fruit in result)
            {
                var deletedResult = await fixture.service.DeleteEntityAsync(fruit.Id);

                // Verify
                Assert.Equal(fruit.Id, deletedResult.Id);
                Assert.Equal(fruit.Name, deletedResult.Name);
                Assert.Equal(fruit.Color, deletedResult.Color);
                Assert.Equal(fruit.Taste, deletedResult.Taste);
            }

            result = await fixture.service.GetEntityListAsync();

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99999)]
        public async Task Test3_DeleteEntityAsync_Case2_InvalidId(long id)
        {
            // Execute
            var result = await fixture.service.DeleteEntityAsync(id);

            // Verify
            Assert.Null(result);
        }
    }
}
