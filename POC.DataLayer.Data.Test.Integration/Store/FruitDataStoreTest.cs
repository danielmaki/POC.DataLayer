using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Models;

namespace POC.DataLayer.Data.Test.Integration.Store
{
    [TestCaseOrderer("POC.DataLayer.Data.Test.Integration.AlphabeticalOrderer", "POC.DataLayer.Data.Test.Integration")]
    public class FruitDataStoreTest : DataStoreEFTest<ApplicationDbContextFixture, ApplicationDbContext>
    {
        public FruitDataStoreTest(ApplicationDbContextFixture fixture) : base(fixture)
        {
        }

        [Theory]
        [InlineData(0, "Apple", "Red", Taste.Sweet)]
        [InlineData(0, "Lime", "Green", Taste.Sour)]
        [InlineData(0, "Unknown", "Unknown", Taste.Unknown)]
        public override async Task Test1_CreateAsync_Case1_ValidModel(long id, string name, string color, Taste taste)
        {
            // Setup
            var model = new Fruit()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            var result = await fixture.fruitDataStore.CreateAsync(model);

            // Verify
            Assert.NotEqual(model.Id, result.Id);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.Color, result.Color);
            Assert.Equal(model.Taste, result.Taste);
        }

        [Theory]
        [InlineData(0, "", null, Taste.Unknown)]
        public override async Task Test1_CreateAsync_Case2_DefaultValues(long id, string name, string color, Taste taste)
        {
            // Setup
            var model = new Fruit()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            var result = await fixture.fruitDataStore.CreateAsync(model);

            // Verify
            Assert.NotEqual(model.Id, result.Id);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal("Unknown", result.Color);
            Assert.Equal(model.Taste, result.Taste);
        }

        [Theory]
        [InlineData(0, "Papaya", "Orange", Taste.Sweet)]
        public override async Task Test1_CreateAsync_Case6_RetryValidModel(long id, string name, string color, Taste taste)
        {
            // Setup
            var model = new Fruit()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            var result = await fixture.fruitDataStore.CreateAsync(model);

            // Verify
            Assert.NotEqual(model.Id, result.Id);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.Color, result.Color);
            Assert.Equal(model.Taste, result.Taste);
        }

        [Theory]
        [InlineData("Test", "Black", Taste.Unknown)]
        public override async Task Test2_UpdateAsync_Case1_ValidModel(string name, string color, Taste taste)
        {
            // Setup
            var result = fixture.fruitDataStore.GetAllAsync();
            var models = new List<Fruit>();
            await foreach (var model in result)
            {
                model.Name = name;
                model.Color = color;
                model.Taste = taste;

                models.Add(model);
            }

            Assert.NotEmpty(models);

            foreach (var model in models)
            {
                // Execute
                var updatedResult = await fixture.fruitDataStore.UpdateAsync(model);

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
        public override async Task Test2_UpdateAsync_Case2_InvalidModel(string name, string color, Taste taste)
        {
            // Setup
            var result = fixture.fruitDataStore.GetAllAsync();
            var models = new List<Tuple<Fruit, Fruit>>();
            await foreach (var model in result)
            {
                var prevModel = new Fruit()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Color = model.Color,
                    Taste = model.Taste
                };
                model.Name = name;
                model.Color = color;
                model.Taste = taste;

                models.Add(new Tuple<Fruit, Fruit>(prevModel, model));
            }

            Assert.NotEmpty(models);

            foreach (var model in models)
            {
                // Execute
                var updatedResult = await fixture.fruitDataStore.UpdateAsync(model.Item2);

                // Verify
                Assert.Null(updatedResult);

                updatedResult = await fixture.fruitDataStore.GetAsync(model.Item2.Id);

                Assert.Equal(model.Item1.Id, updatedResult.Id);
                Assert.Equal(model.Item1.Name, updatedResult.Name);
                Assert.Equal(model.Item1.Color, updatedResult.Color);
                Assert.Equal(model.Item1.Taste, updatedResult.Taste);
            }
        }

        [Theory]
        [InlineData("Lime", "Green", Taste.Sour)]
        public override async Task Test2_UpdateAsync_Case5_RetryValidModel(string name, string color, Taste taste)
        {
            // Setup
            var result = fixture.fruitDataStore.GetAllAsync();
            var models = new List<Fruit>();
            await foreach (var model in result)
            {
                model.Name = name;
                model.Color = color;
                model.Taste = taste;

                models.Add(model);
            }

            Assert.NotEmpty(models);

            foreach (var model in models)
            {
                // Execute
                var updatedResult = await fixture.fruitDataStore.UpdateAsync(model);

                // Verify
                Assert.Equal(model.Id, updatedResult.Id);
                Assert.Equal(name, updatedResult.Name);
                Assert.Equal(color, updatedResult.Color);
                Assert.Equal(taste, updatedResult.Taste);
            }
        }

        [Fact]
        public override async Task Test3_DeleteAsync_Case1_ValidId()
        {
            // Execute
            var result = fixture.fruitDataStore.GetAllAsync();
            var models = new List<Fruit>();
            await foreach (var model in result)
            {
                models.Add(model);
            }

            Assert.NotEmpty(models);

            foreach (var model in models)
            {
                var deletedResult = await fixture.fruitDataStore.DeleteAsync(model.Id);

                // Verify
                Assert.Equal(model.Id, deletedResult.Id);
                Assert.Equal(model.Name, deletedResult.Name);
                Assert.Equal(model.Color, deletedResult.Color);
                Assert.Equal(model.Taste, deletedResult.Taste);
            }

            result = fixture.fruitDataStore.GetAllAsync();

            await foreach (var model in result)
            {
                Assert.Null(model);
            }
        }
    }
}
