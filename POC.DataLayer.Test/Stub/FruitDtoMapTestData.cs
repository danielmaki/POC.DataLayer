using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Test.Unit.Stub.Abstractions;

namespace POC.DataLayer.Data.Test.Unit.Stub
{
    #region MapModelToExternal
    public class MapFruitToDtoTestData : MapToExternalData<Fruit, FruitDto>
    {
        public MapFruitToDtoTestData()
        {
            Add(
                new Fruit
                {
                    Id = 1,
                    Name = "Apple",
                    Color = "Red",
                    Taste = Taste.Sweet
                },
                new FruitDto
                {
                    Id = 1,
                    Name = "Apple",
                    Color = "Red",
                    Taste = "Sweet"
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
                new FruitDto
                {
                    Id = 2,
                    Name = "Lime",
                    Color = "Green",
                    Taste = "Sour"
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
                new FruitDto
                {
                    Id = 3,
                    Name = "Grapefruit",
                    Color = "Pink",
                    Taste = "Bitter"
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
                new FruitDto
                {
                    Id = 4,
                    Name = "Unknown",
                    Color = "Unknown",
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = 0,
                    Name = null,
                    Color = "Black",
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = long.MinValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = long.MaxValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = "Unknown"
                }
            );
        }
    }
    #endregion

    #region MapExternalToModel
    public class MapDtoToFruitTestData : MapToModelData<FruitDto, Fruit>
    {
        public MapDtoToFruitTestData()
        {
            Add(
                new FruitDto
                {
                    Id = 1,
                    Name = "Apple",
                    Color = "Red",
                    Taste = "Sweet"
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
                new FruitDto
                {
                    Id = 2,
                    Name = "Lime",
                    Color = "Green",
                    Taste = "Sour"
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
                new FruitDto
                {
                    Id = 3,
                    Name = "Grapefruit",
                    Color = "Pink",
                    Taste = "Bitter"
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
                new FruitDto
                {
                    Id = 4,
                    Name = "Unknown",
                    Color = "Unknown",
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = 0,
                    Name = null,
                    Color = "Black",
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = 0,
                    Name = null,
                    Color = null,
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = long.MinValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = long.MaxValue,
                    Name = string.Empty,
                    Color = string.Empty,
                    Taste = "Unknown"
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
                new FruitDto
                {
                    Id = default,
                    Name = default,
                    Color = default,
                    Taste = default
                },
                new Fruit
                {
                    Id = default,
                    Name = default,
                    Color = default,
                    Taste = Taste.Unknown
                }
            );
            Add(
                new FruitDto
                {
                    Id = default,
                    Name = default,
                    Color = default,
                    Taste = string.Empty
                },
                new Fruit
                {
                    Id = default,
                    Name = default,
                    Color = default,
                    Taste = Taste.Unknown
                }
            );
            Add(
                new FruitDto
                {
                    Id = default,
                    Name = default,
                    Color = default,
                    Taste = "Enum doesn't exist"
                },
                new Fruit
                {
                    Id = default,
                    Name = default,
                    Color = default,
                    Taste = Taste.Unknown
                }
            );
        }
    }
    #endregion
}
