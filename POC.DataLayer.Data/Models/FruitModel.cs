using POC.DataLayer.Data.Enums;

namespace POC.DataLayer.Data.Models
{
    public class FruitModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public Taste Taste { get; set; }
    }
}
