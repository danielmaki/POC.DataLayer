using System;

using Xunit;

using POC.DataLayer.Data.Enums;

namespace POC.DataLayer.Data.Test.Mappings
{
    public class FruitBackFacingMapTest : IBackFacingMapTest
    {
        [Theory]
        [InlineData(1, "Apple", "Red", 1)]
        [InlineData(2, "Lime", "Green", 2)]
        [InlineData(3, "Unknown", "Unknown", 0)]
        public void CopyExt(long id, string name, string color, int tasteExt)
        {
            // Setup
            var mapping = new FruitBackFacingMap();
            var ext = new FruitORM()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteExt
            };

            // Execute
            var result = mapping.CopyExt(ext);

            // Verify
            Assert.Equal(0, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteExt, result.Taste);
        }

        [Fact]
        public void CopyExt_Null()
        {
            // Setup
            var mapping = new FruitBackFacingMap();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.CopyExt(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, 1)]
        [InlineData(2, "Lime", "Green", Taste.Sour, 2)]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, 0)]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, 1)]
        [InlineData(0, null, "Black", Taste.Unknown, 0)]
        [InlineData(0, null, null, Taste.Unknown, 0)]
        [InlineData(0, null, null, null, null)]
        public void IntlToExt(long id, string name, string color, Taste tasteIntl, int tasteExt)
        {
            // Setup
            var mapping = new FruitBackFacingMap();
            var intl = new FruitModel()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteIntl
            };

            // Execute
            var result = mapping.IntlToExt(intl);

            // Verify
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteExt, result.Taste);
        }

        [Fact]
        public void IntlToExt_Null()
        {
            // Setup
            var mapping = new FruitBackFacingMap();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.IntlToExt(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, 1)]
        [InlineData(2, "Lime", "Green", Taste.Sour, 2)]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, 0)]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, 1)]
        [InlineData(0, null, "Black", Taste.Unknown, 0)]
        [InlineData(0, null, null, Taste.Unknown, 0)]
        [InlineData(0, null, null, null, null)]
        public void ExtToIntl(long id, string name, string color, Taste tasteIntl, int tasteExt)
        {
            // Setup
            var mapping = new FruitBackFacingMap();
            var ext = new FruitORM()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteExt
            };

            // Execute
            var result = mapping.ExtToIntl(ext);

            // Verify
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteIntl, result.Taste);
        }

        [Fact]
        public void ExtToIntl_Null()
        {
            // Setup
            var mapping = new FruitBackFacingMap();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ExtToIntl(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", 1)]
        [InlineData(2, "Lime", "Green", 2)]
        [InlineData(3, "Unknown", "Unknown", 0)]
        public void UpdateExt(long id, string name, string color, int taste)
        {
            // Setup
            var mapping = new FruitBackFacingMap();
            var ext = new FruitORM()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = taste
            };
            var update = new FruitORM()
            {
                Id = 0,
                Name = name,
                Color = color,
                Taste = taste
            };

            // Execute
            mapping.UpdateExt(ext, update);

            // Verify
            Assert.Equal(id, ext.Id);
            Assert.Equal(name, update.Name);
            Assert.Equal(color, update.Color);
            Assert.Equal(taste, update.Taste);
        }

        [Fact]
        public void UpdateExt_Null()
        {
            // Setup
            var mapping = new FruitBackFacingMap();
            var ext = new FruitORM();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExt(ext, null));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExt(null, ext));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExt(null, null));
        }
    }
}
