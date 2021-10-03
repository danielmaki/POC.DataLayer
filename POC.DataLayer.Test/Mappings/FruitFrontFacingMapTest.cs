using System;

using Xunit;

using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Mappings.FrontFacing;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Test.Unit.Mappings.Abstractions;

namespace POC.DataLayer.Data.Test.Unit.Mappings
{
    public class FruitFrontFacingMapTest : IFrontFacingMapTest
    {
        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, "Sweet")]
        [InlineData(2, "Lime", "Green", Taste.Sour, "Sour")]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, "Unknown")]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, "Sweet")]
        [InlineData(0, null, "Black", Taste.Unknown, "Unknown")]
        [InlineData(0, null, null, Taste.Unknown, "Unknown")]
        [InlineData(0, null, null, null, "Unknown")]
        public void ToModel(long id, string name, string color, Taste tasteIntl, string tasteExt)
        {
            // Setup
            var mapping = new FruitFrontFacingMap();
            var ext = new FruitDto()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteExt
            };

            // Execute
            var result = mapping.ToModel(ext);

            // Verify
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteIntl, result.Taste);
        }

        [Fact]
        public void ToModel_Null()
        {
            // Setup
            var mapping = new FruitFrontFacingMap();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ToModel(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, "Sweet")]
        [InlineData(2, "Lime", "Green", Taste.Sour, "Sour")]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, "Unknown")]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, "Sweet")]
        [InlineData(0, null, "Black", Taste.Unknown, "Unknown")]
        [InlineData(0, null, null, Taste.Unknown, "Unknown")]
        [InlineData(0, null, null, null, "Unknown")]
        public void ToExternal(long id, string name, string color, Taste tasteIntl, string tasteExt)
        {
            // Setup
            var mapping = new FruitFrontFacingMap();
            var intl = new Fruit()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteIntl
            };

            // Execute
            var result = mapping.ToExternal(intl);

            // Verify
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteExt, result.Taste);
        }

        [Fact]
        public void ToExternal_Null()
        {
            // Setup
            var mapping = new FruitFrontFacingMap();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ToExternal(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", "Sweet")]
        [InlineData(2, "Lime", "Green", "Sour")]
        [InlineData(3, "Unknown", "Unknown", "Unknown")]
        public void UpdateExternal(long id, string name, string color, string taste)
        {
            // Setup
            var mapping = new FruitFrontFacingMap();
            var ext = new FruitDto()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };
            var update = new FruitDto()
            {
                Id = 0,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            mapping.UpdateExternal(ext, update);

            // Verify
            Assert.Equal(id, ext.Id);
            Assert.Equal(name, update.Name);
            Assert.Equal(color, update.Color);
            Assert.Equal(taste, update.Taste);
        }

        [Fact]
        public void UpdateExternal_Null()
        {
            // Setup
            var mapping = new FruitFrontFacingMap();
            var ext = new FruitDto();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExternal(ext, null));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExternal(null, ext));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExternal(null, null));
        }
    }
}
