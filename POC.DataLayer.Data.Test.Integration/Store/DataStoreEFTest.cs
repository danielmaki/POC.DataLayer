using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Xunit;

using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Models.Abstractions;
using POC.DataLayer.Data.Test.Integration.Store.Abstractions;

namespace POC.DataLayer.Data.Test.Integration.Store
{
    [TestCaseOrderer("POC.DataLayer.Data.Test.Integration.AlphabeticalOrderer", "POC.DataLayer.Data.Test.Integration")]
    public abstract class DataStoreEFTest<FIXTURE, CONTEXT> : IDataStoreFixture<FIXTURE> where FIXTURE : DbContextFixture<CONTEXT> where CONTEXT : DbContext, new()
    {
        public readonly FIXTURE fixture;

        public DataStoreEFTest(FIXTURE fixture)
        {
            this.fixture = fixture;
        }

        public abstract Task Test1_CreateAsync_Case1_ValidModel(long id, string name, string color, Taste taste);

        public abstract Task Test1_CreateAsync_Case2_DefaultValues(long id, string name, string color, Taste taste);

        [Theory]
        [InlineData(0, "This name is too long to be a name for a fruit, the maximum string length is 64.", "Yellow", Taste.Sweet)]
        [InlineData(0, "Banana", "This color is too long to be a color for a fruit, the maximum string length is 32.", Taste.Sweet)]
        [InlineData(0, null, "Yellow", Taste.Sweet)]
        public virtual async Task Test1_CreateAsync_Case3_InvalidModel(long id, string name, string color, Taste taste)
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
        public virtual async Task Test1_CreateAsync_Case4_InvalidId(long id, string name, string color, Taste taste)
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
        public virtual async Task Test1_CreateAsync_Case5_NullEntity()
        {
            // Execute
            var result = await fixture.dataStore.CreateAsync(null);

            // Verify
            Assert.Null(result);
        }

        public abstract Task Test1_CreateAsync_Case6_RetryValidModel(long id, string name, string color, Taste taste);

        public abstract Task Test2_UpdateAsync_Case1_ValidModel(string name, string color, Taste taste);

        public abstract Task Test2_UpdateAsync_Case2_InvalidModel(string name, string color, Taste taste);

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99999)]
        public virtual async Task Test2_UpdateAsync_Case3_InvalidId(long id)
        {
            // Setup
            var result = fixture.dataStore.GetAllAsync();
            var models = new List<Tuple<IModel, IModel>>();
            await foreach (var model in result)
            {
                var prevModel = model;
                model.Id = id;

                models.Add(new Tuple<IModel, IModel>(prevModel, model));
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
        public virtual async Task Test2_UpdateAsync_Case4_NullEntity()
        {
            // Execute
            var result = await fixture.dataStore.UpdateAsync(null);

            // Verify
            Assert.Null(result);
        }

        public abstract Task Test2_UpdateAsync_Case5_RetryValidModel(string name, string color, Taste taste);

        [Fact]
        public virtual async Task Test2_UpdateAsync_Case6_NoChange()
        {
            // Setup
            var result = fixture.dataStore.GetAllAsync();
            var models = new List<IModel>();
            await foreach (var model in result)
            {
                models.Add(model);
            }

            Assert.NotEmpty(models);

            foreach (var model in models)
            {
                // Execute
                var updatedResult = await fixture.dataStore.UpdateAsync(model);

                // Verify
                Assert.Null(updatedResult);
            }
        }

        public abstract Task Test3_DeleteAsync_Case1_ValidId();

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(99999)]
        public virtual async Task Test3_DeleteAsync_Case2_InvalidId(long id)
        {
            // Execute
            var result = await fixture.dataStore.DeleteAsync(id);

            // Verify
            Assert.Null(result);
        }
    }
}
