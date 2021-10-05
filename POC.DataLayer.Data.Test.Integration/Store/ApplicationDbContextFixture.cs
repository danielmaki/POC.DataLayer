using Microsoft.Extensions.Logging;

using Moq;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.Mappings;
using POC.DataLayer.Data.Models.Abstractions;
using POC.DataLayer.Data.Store;
using POC.DataLayer.Data.Store.Abstractions;
using POC.DataLayer.Data.Test.Integration.Store.Abstractions;

namespace POC.DataLayer.Data.Test.Integration.Store
{
    public class ApplicationDbContextFixture : DbContextFixture<ApplicationDbContext>
    {
        public readonly FruitDataStore fruitDataStore;

        public ApplicationDbContextFixture() : base()
        {
            // Fruit data store
            var logger = new Mock<ILogger<FruitDataStore>>(MockBehavior.Loose);
            var dataMapper = new FruitEntityMap();

            fruitDataStore = new FruitDataStore(logger.Object, context, dataMapper);
        }

        public override IDataStore<IModel> dataStore => (IDataStore<IModel>)fruitDataStore;
    }
}
