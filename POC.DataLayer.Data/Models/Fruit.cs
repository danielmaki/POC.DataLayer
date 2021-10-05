using POC.DataLayer.Data.Enums;
using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Data.Models
{
    public class Fruit : IModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public Taste Taste { get; set; }
    }

    public class FruitDto : IDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public string Taste { get; set; }
    }

    public class FruitEntity : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public int Taste { get; set; }
    }
}
