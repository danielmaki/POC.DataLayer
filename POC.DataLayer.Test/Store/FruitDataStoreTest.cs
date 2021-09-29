using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xunit;

using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Test.Store.Abstractions;

namespace POC.DataLayer.Data.Test.Store
{
    [TestCaseOrderer("POC.DataLayer.Data.Test.AlphabeticalOrderer", "POC.DataLayer.Data.Test")]
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
        public async Task Test1_CreateAsync_Case1_ValidModel(long id, string name, string color, Taste taste)
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
            var result = await fixture.dataStore.CreateAsync(model);

            // Verify
            Assert.NotEqual(model.Id, result.Id);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.Color, result.Color);
            Assert.Equal(model.Taste, result.Taste);
        }

        [Theory]
        [InlineData(0, "", null, Taste.Unknown)]
        public async Task Test1_CreateAsync_Case2_DefaultValues(long id, string name, string color, Taste taste)
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
            var result = await fixture.dataStore.CreateAsync(model);

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
        public async Task Test1_CreateAsync_Case3_InvalidModel(long id, string name, string color, Taste taste)
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
            var result = await fixture.dataStore.CreateAsync(model);

            // Verify
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet)]
        [InlineData(-1, "Lime", "Green", Taste.Sour)]
        public async Task Test1_CreateAsync_Case4_InvalidId(long id, string name, string color, Taste taste)
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
            var result = await fixture.dataStore.CreateAsync(model);

            // Verify
            Assert.Null(result);
        }

        [Fact]
        public async Task Test1_CreateAsync_Case5_NullEntity()
        {
            // Execute
            var result = await fixture.dataStore.CreateAsync(null);

            // Verify
            Assert.Null(result);
        }

        [Theory]
        [InlineData(0, "Papaya", "Orange", Taste.Sweet)]
        public async Task Test1_CreateAsync_Case6_ValidModel(long id, string name, string color, Taste taste)
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
            var result = await fixture.dataStore.CreateAsync(model);

            // Verify
            Assert.NotEqual(model.Id, result.Id);
            Assert.Equal(model.Name, result.Name);
            Assert.Equal(model.Color, result.Color);
            Assert.Equal(model.Taste, result.Taste);
        }

        [Theory]
        [InlineData("Test", "Black", Taste.Unknown)]
        public async Task Test2_UpdateAsync_Case1_ValidModel(string name, string color, Taste taste)
        {
            // Setup
            var result = fixture.dataStore.GetAllAsync();
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
                var updatedResult = await fixture.dataStore.UpdateAsync(model);

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
        public async Task Test2_UpdateAsync_Case2_InvalidModel(string name, string color, Taste taste)
        {
            // Setup
            var result = fixture.dataStore.GetAllAsync();
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
                var updatedResult = await fixture.dataStore.UpdateAsync(model.Item2);

                // Verify
                Assert.Null(updatedResult);

                updatedResult = await fixture.dataStore.GetAsync(model.Item2.Id);

                Assert.Equal(model.Item1.Id, updatedResult.Id);
                Assert.Equal(model.Item1.Name, updatedResult.Name);
                Assert.Equal(model.Item1.Color, updatedResult.Color);
                Assert.Equal(model.Item1.Taste, updatedResult.Taste);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99999)]
        public async Task Test2_UpdateAsync_Case3_InvalidId(long id)
        {
            // Setup
            var result = fixture.dataStore.GetAllAsync();
            var models = new List<Tuple<Fruit, Fruit>>();
            await foreach (var model in result)
            {
                var prevModel = model;
                model.Id = id;

                models.Add(new Tuple<Fruit, Fruit>(prevModel, model));
            }

            Assert.NotEmpty(models);

            foreach (var model in models)
            {
                // Execute
                var updatedResult = await fixture.dataStore.UpdateAsync(model.Item2);

                // Verify
                Assert.Null(updatedResult);
            }
        }

        [Fact]
        public async Task Test2_UpdateAsync_Case4_NullEntity()
        {
            // Execute
            var result = await fixture.dataStore.UpdateAsync(null);

            // Verify
            Assert.Null(result);
        }

        [Theory]
        [InlineData("Lime", "Green", Taste.Sour)]
        public async Task Test2_UpdateAsync_Case5_ValidModel(string name, string color, Taste taste)
        {
            // Setup
            var result = fixture.dataStore.GetAllAsync();
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
                var updatedResult = await fixture.dataStore.UpdateAsync(model);

                // Verify
                Assert.Equal(model.Id, updatedResult.Id);
                Assert.Equal(name, updatedResult.Name);
                Assert.Equal(color, updatedResult.Color);
                Assert.Equal(taste, updatedResult.Taste);
            }
        }

        [Fact]
        public async Task Test3_DeleteAsync_Case1_ValidId()
        {
            // Execute
            var result = fixture.dataStore.GetAllAsync();
            var models = new List<Fruit>();
            await foreach (var model in result)
            {
                models.Add(model);
            }

            Assert.NotEmpty(models);

            foreach (var model in models)
            {
                var deletedResult = await fixture.dataStore.DeleteAsync(model.Id);

                // Verify
                Assert.Equal(model.Id, deletedResult.Id);
                Assert.Equal(model.Name, deletedResult.Name);
                Assert.Equal(model.Color, deletedResult.Color);
                Assert.Equal(model.Taste, deletedResult.Taste);
            }

            result = fixture.dataStore.GetAllAsync();

            await foreach (var model in result)
            {
                Assert.Null(model);
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99999)]
        public async Task Test3_DeleteAsync_Case2_InvalidId(long id)
        {
            // Execute
            var result = await fixture.dataStore.DeleteAsync(id);

            // Verify
            Assert.Null(result);
        }
    }
}
