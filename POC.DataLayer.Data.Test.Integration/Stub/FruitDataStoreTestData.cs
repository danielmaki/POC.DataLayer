using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Test.Integration.Stub.Abstractions;

namespace POC.DataLayer.Data.Test.Integration.Stub
{
    #region CreateValidModel
    public class CreateValidFruitTestData : CreateValidModelTestData<Fruit>
    {
        public CreateValidFruitTestData()
        {
            Add(
                new Fruit
                {
                    Id = 0,
                    Name = "Apple",
                    Color = "Red",
                    Taste = Taste.Sweet
                },
                new Fruit
                {
                    Id = 1,
                    Name = "Apple",
                    Color = "Red",
                    Taste = Taste.Sweet
                }
            );
            Add(
                new Fruit
                {
                    Id = 0,
                    Name = "Lime",
                    Color = "Green",
                    Taste = Taste.Sour
                },
                new Fruit
                {
                    Id = 2,
                    Name = "Lime",
                    Color = "Green",
                    Taste = Taste.Sour
                }
            );
            Add(
                new Fruit
                {
                    Id = 0,
                    Name = "Grapefruit",
                    Color = "Pink",
                    Taste = Taste.Bitter
                },
                new Fruit
                {
                    Id = 3,
                    Name = "Grapefruit",
                    Color = "Pink",
                    Taste = Taste.Bitter
                }
            );
            Add(
                new Fruit
                {
                    Id = 0,
                    Name = "Unknown",
                    Color = "Unknown",
                    Taste = Taste.Unknown
                },
                new Fruit
                {
                    Id = 4,
                    Name = "Unknown",
                    Color = "Unknown",
                    Taste = Taste.Unknown
                }
            );
        }
    }
    #endregion

    #region CreateDefaultModel
    public class CreateDefaultFruitTestData : CreateDefaultModelTestData<Fruit>
    {
        public CreateDefaultFruitTestData()
        {
            Add(
                new Fruit()
                {
                    Name = string.Empty,
                },
                new Fruit
                {
                    Id = 5,
                    Name = string.Empty,
                    Color = "Unknown",
                    Taste = Taste.Unknown
                }
            );
        }
    }
    #endregion
}
