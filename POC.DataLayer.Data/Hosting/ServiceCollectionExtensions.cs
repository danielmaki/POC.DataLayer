using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Mappings;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Models.Abstractions;
using POC.DataLayer.Data.Store;
using POC.DataLayer.Data.Store.Abstractions;

namespace POC.DataLayer.Data.Hosting
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection self, IConfiguration configuration)
        {
            // Fruit data
            self.AddStore<FruitDataStore, FruitEntityMap, Fruit, FruitEntity>();
            self.AddMapping<FruitDtoMap, Fruit, FruitDto>();

            self.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            return self;
        }

        public static IServiceCollection AddMapping<MAP, MODEL, EXTERNAL>(this IServiceCollection self)
            where MAP : class, IDataMap<MODEL, EXTERNAL>
            where MODEL : IModel
            where EXTERNAL : IExternal
        {
            self.AddTransient<IDataMap<MODEL, EXTERNAL>, MAP>();

            return self;
        }

        public static IServiceCollection AddStore<STORE, MAP, MODEL, ENTITY>(this IServiceCollection self)
            where STORE : class, IDataStore<MODEL>
            where MAP : class, IDataMap<MODEL, ENTITY>
            where MODEL : IModel
            where ENTITY : IEntity
        {
            self.AddMapping<MAP, MODEL, ENTITY>();
            self.AddScoped<IDataStore<MODEL>, STORE>();

            return self;
        }
    }
}
