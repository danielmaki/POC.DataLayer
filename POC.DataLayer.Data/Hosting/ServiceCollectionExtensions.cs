using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Mappings.BackFacing;
using POC.DataLayer.Data.Mappings.FrontFacing;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Store;
using POC.DataLayer.Data.Store.Abstractions;

namespace POC.DataLayer.Data.Hosting
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection self, IConfiguration configuration)
        {
            // Fruit data
            self.AddSingleton<IDataMap<Fruit, FruitEntity>, FruitEntityMap>();
            self.AddScoped<IDataStore<Fruit>, FruitDataStore>();

            self.AddSingleton<IDataMap<Fruit, FruitDto>, FruitDtoMap>();

            self.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            return self;
        }
    }
}
