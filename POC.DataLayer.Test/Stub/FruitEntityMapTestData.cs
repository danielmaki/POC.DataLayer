using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Test.Unit.Stub.Abstractions;

namespace POC.DataLayer.Data.Test.Unit.Stub
{
    #region MapModelToExternal
    public class MapFruitToEntityTestData : MapToExternalData<Fruit, FruitEntity>
    {
        public MapFruitToEntityTestData()
        {
            Add(
                new Fruit
                {
                    Id = 1,
                    Name = "Apple",
                    Color = "Red",
                    Taste = Taste.Sweet
                },
                new FruitEntity
                {
                    Id = 1,
                    Name = "Apple",
                    Color = "Red",
                    Taste = 1
                }
            );
            Add(
                new Fruit
                {
                    Id = 2,
                    Name = "Lime",
                    Color = "Green",
                    Taste = Taste.Sour
                },
                new FruitEntity
                {
                    Id = 2,
                    Name = "Lime",
                    Color = "Green",
                    Taste = 2
                }
            );
            Add(
                new Fruit
                {
                    Id = 3,
                    Name = "Grapefruit",
                    Color = "Pink",
                    Taste = Taste.Bitter
                },
                new FruitEntity
                {
                    Id = 3,
                    Name = "Grapefruit",
                    Color = "Pink",
                    Taste = 3
                }
            );
            Add(
                new Fruit
                {
                    Id = 4,
                    Name = "Unknown",
                    Color = "Unknown",
                    Taste = Taste.Unknown
                },
                new FruitEntity
                {
                    Id = 4,
                    Name = "Unknown",
                    Color = "Unknown",
                    Taste = 0
                }
            );
            Add(
                new Fruit
                {
                    Id = 0,
                    Name = null,
                    Color = "Black",
                    Taste = Taste.Unknown
                },
                new FruitEntity
                {
                    Id = 0,
                    Name = null,
                    Color = "Black",
                    Taste = 0
                }
            );
            Add(
                new Fruit
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = Taste.Unknown
                },
                new FruitEntity
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = 0
                }
            );
            Add(
                new Fruit
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = default
                },
                new FruitEntity
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = default
                }
            );
            Add(
                new Fruit
                {
                    Id = long.MinValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = default
                },
                new FruitEntity
                {
                    Id = long.MinValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = default
                }
            );
            Add(
                new Fruit
                {
                    Id = long.MaxValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = default
                },
                new FruitEntity
                {
                    Id = long.MaxValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = default
                }
            );
        }
    }
    #endregion

    #region MapExternalToModel
    public class MapEntityToFruitTestData : MapToModelData<FruitEntity, Fruit>
    {
        public MapEntityToFruitTestData()
        {
            Add(
                new FruitEntity
                {
                    Id = 1,
                    Name = "Apple",
                    Color = "Red",
                    Taste = 1
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
                new FruitEntity
                {
                    Id = 2,
                    Name = "Lime",
                    Color = "Green",
                    Taste = 2
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
                new FruitEntity
                {
                    Id = 3,
                    Name = "Grapefruit",
                    Color = "Pink",
                    Taste = 3
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
                new FruitEntity
                {
                    Id = 4,
                    Name = "Unknown",
                    Color = "Unknown",
                    Taste = 0
                },
                new Fruit
                {
                    Id = 4,
                    Name = "Unknown",
                    Color = "Unknown",
                    Taste = Taste.Unknown
                }
            );
            Add(
                new FruitEntity
                {
                    Id = 0,
                    Name = null,
                    Color = "Black",
                    Taste = 0
                },
                new Fruit
                {
                    Id = 0,
                    Name = null,
                    Color = "Black",
                    Taste = Taste.Unknown
                }
            );
            Add(
                new FruitEntity
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = 0
                },
                new Fruit
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = Taste.Unknown
                }
            );
            Add(
                new FruitEntity
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = default
                },
                new Fruit
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = default
                }
            );
            Add(
                new FruitEntity
                {
                    Id = long.MinValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = default
                },
                new Fruit
                {
                    Id = long.MinValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = default
                }
            );
            Add(
                new FruitEntity
                {
                    Id = long.MaxValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = default
                },
                new Fruit
                {
                    Id = long.MaxValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = default
                }
            );
            Add(
                new FruitEntity
                {
                    Id = default,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = int.MinValue
                },
                new Fruit
                {
                    Id = default,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = Taste.Unknown
                }
            );
            Add(
                new FruitEntity
                {
                    Id = default,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = int.MaxValue
                },
                new Fruit
                {
                    Id = default,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = Taste.Unknown
                }
            );
        }
    }
    #endregion
}
