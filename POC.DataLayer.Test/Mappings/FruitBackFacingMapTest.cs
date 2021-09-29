using System;

using Xunit;

using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Mappings.BackFacing;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Test.Mappings.Abstractions;

namespace POC.DataLayer.Data.Test.Mappings
{
    public class FruitBackFacingMapTest : IBackFacingMapTest
    {
        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, 1)]
        [InlineData(2, "Lime", "Green", Taste.Sour, 2)]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, 0)]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, 1)]
        [InlineData(0, null, "Black", Taste.Unknown, 0)]
        [InlineData(0, null, null, Taste.Unknown, 0)]
        [InlineData(0, null, null, null, null)]
        public void ToExternal(long id, string name, string color, Taste tasteIntl, int tasteExt)
        {
            // Setup
            var mapping = new FruitBackFacingMap();
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
            var mapping = new FruitBackFacingMap();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ToExternal(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, 1)]
        [InlineData(2, "Lime", "Green", Taste.Sour, 2)]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, 0)]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, 1)]
        [InlineData(0, null, "Black", Taste.Unknown, 0)]
        [InlineData(0, null, null, Taste.Unknown, 0)]
        [InlineData(0, null, null, null, null)]
        public void ToModel(long id, string name, string color, Taste tasteIntl, int tasteExt)
        {
            // Setup
            var mapping = new FruitBackFacingMap();
            var ext = new FruitEntity()
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
            var mapping = new FruitBackFacingMap();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ToModel(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", 1)]
        [InlineData(2, "Lime", "Green", 2)]
        [InlineData(3, "Unknown", "Unknown", 0)]
        public void UpdateExternal(long id, string name, string color, int taste)
        {
            // Setup
            var mapping = new FruitBackFacingMap();
            var ext = new FruitEntity()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };
            var update = new FruitEntity()
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
            var mapping = new FruitBackFacingMap();
            var ext = new FruitEntity();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExternal(ext, null));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExternal(null, ext));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExternal(null, null));
        }
    }
}
