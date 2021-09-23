using System;

using Xunit;

using POC.DataLayer.Data.DTO;
using POC.DataLayer.Data.ORM;
using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Mappings;
using POC.DataLayer.Data.Models;

namespace POC.DataLayer.Data.Test.Mappings
{
    public class FruitMappingTest : IDataMappingTest
    {
        [Theory]
        [InlineData(1, "Apple", "Red", 1)]
        [InlineData(2, "Lime", "Green", 2)]
        [InlineData(3, "Unknown", "Unknown", 0)]
        public void CopyORM(long id, string name, string color, int tasteORM)
        {
            // Setup
            var mapping = new FruitMapping();
            var orm = new FruitORM()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteORM
            };

            // Execute
            var result = mapping.CopyORM(orm);

            // Verify
            Assert.Equal(0, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteORM, result.Taste);
        }

        [Fact]
        public void CopyORM_Null()
        {
            // Setup
            var mapping = new FruitMapping();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.CopyORM(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, "Sweet")]
        [InlineData(2, "Lime", "Green", Taste.Sour, "Sour")]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, "Unknown")]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, "Sweet")]
        [InlineData(0, null, "Black", Taste.Unknown, "Unknown")]
        [InlineData(0, null, null, Taste.Unknown, "Unknown")]
        [InlineData(0, null, null, null, "Unknown")]
        public void DTOToModel(long id, string name, string color, Taste tasteModel, string tasteDTO)
        {
            // Setup
            var mapping = new FruitMapping();
            var dto = new FruitDTO()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteDTO
            };

            // Execute
            var result = mapping.DTOToModel(dto);

            // Verify
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteModel, result.Taste);
        }

        [Fact]
        public void DTOToModel_Null()
        {
            // Setup
            var mapping = new FruitMapping();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.DTOToModel(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, "Sweet")]
        [InlineData(2, "Lime", "Green", Taste.Sour, "Sour")]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, "Unknown")]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, "Sweet")]
        [InlineData(0, null, "Black", Taste.Unknown, "Unknown")]
        [InlineData(0, null, null, Taste.Unknown, "Unknown")]
        [InlineData(0, null, null, null, "Unknown")]
        public void ModelToDTO(long id, string name, string color, Taste tasteModel, string tasteDTO)
        {
            // Setup
            var mapping = new FruitMapping();
            var model = new FruitModel()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteModel
            };

            // Execute
            var result = mapping.ModelToDTO(model);

            // Verify
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteDTO, result.Taste);
        }

        [Fact]
        public void ModelToDTO_Null()
        {
            // Setup
            var mapping = new FruitMapping();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ModelToDTO(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, 1)]
        [InlineData(2, "Lime", "Green", Taste.Sour, 2)]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, 0)]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, 1)]
        [InlineData(0, null, "Black", Taste.Unknown, 0)]
        [InlineData(0, null, null, Taste.Unknown, 0)]
        [InlineData(0, null, null, null, null)]
        public void ModelToORM(long id, string name, string color, Taste tasteModel, int tasteORM)
        {
            // Setup
            var mapping = new FruitMapping();
            var model = new FruitModel()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteModel
            };

            // Execute
            var result = mapping.ModelToORM(model);

            // Verify
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteORM, result.Taste);
        }

        [Fact]
        public void ModelToORM_Null()
        {
            // Setup
            var mapping = new FruitMapping();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ModelToORM(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", Taste.Sweet, 1)]
        [InlineData(2, "Lime", "Green", Taste.Sour, 2)]
        [InlineData(3, "Unknown", "Unknown", Taste.Unknown, 0)]
        [InlineData(0, "Banana", "Yellow", Taste.Sweet, 1)]
        [InlineData(0, null, "Black", Taste.Unknown, 0)]
        [InlineData(0, null, null, Taste.Unknown, 0)]
        [InlineData(0, null, null, null, null)]
        public void ORMToModel(long id, string name, string color, Taste tasteModel, int tasteORM)
        {
            // Setup
            var mapping = new FruitMapping();
            var orm = new FruitORM()
            {
                Id = id,
                Name = name,
                Color = color,
                Taste = tasteORM
            };

            // Execute
            var result = mapping.ORMToModel(orm);

            // Verify
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
            Assert.Equal(color, result.Color);
            Assert.Equal(tasteModel, result.Taste);
        }

        [Fact]
        public void ORMToModel_Null()
        {
            // Setup
            var mapping = new FruitMapping();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ORMToModel(null));
        }

        [Theory]
        [InlineData(1, "Apple", "Red", 1)]
        [InlineData(2, "Lime", "Green", 2)]
        [InlineData(3, "Unknown", "Unknown", 0)]
        public void UpdateORM(long id, string name, string color, int taste)
        {
            // Setup
            var mapping = new FruitMapping();
            var orm = new FruitORM()
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
            mapping.UpdateORM(orm, update);

            // Verify
            Assert.Equal(id, orm.Id);
            Assert.Equal(name, update.Name);
            Assert.Equal(color, update.Color);
            Assert.Equal(taste, update.Taste);
        }

        [Fact]
        public void UpdateORM_Null()
        {
            // Setup
            var mapping = new FruitMapping();
            var orm = new FruitORM();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.UpdateORM(orm, null));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateORM(null, orm));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateORM(null, null));
        }
    }
}
