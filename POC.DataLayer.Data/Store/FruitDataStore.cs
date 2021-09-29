using Microsoft.Extensions.Logging;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Store.Abstractions;

namespace POC.DataLayer.Data.Store
{
    public class FruitDataStore : DataStoreEF<Fruit, FruitEntity>
    {
        private readonly ILogger<FruitDataStore> logger;

        public FruitDataStore(ILogger<FruitDataStore> logger, ApplicationDbContext context, IDataMap<Fruit, FruitEntity> dataMapper)
            : base(logger, context, dataMapper)
        {
            this.logger = logger;
        }
    }
}
