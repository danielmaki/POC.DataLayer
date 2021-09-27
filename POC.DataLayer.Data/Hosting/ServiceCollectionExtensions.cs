using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//using POC.DataLayer.Data.Configurations;
using POC.DataLayer.Data.Context;
//using POC.DataLayer.Data.DTO;
using POC.DataLayer.Data.Mappings;
//using POC.DataLayer.Data.Models;
//using POC.DataLayer.Data.ORM;
using POC.DataLayer.Data.Store;

namespace POC.DataLayer.Data.Hosting
{
    public static class DataServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection self, IConfiguration configuration)
        {
            // Fruit data
            self.AddTransient<FruitConfiguration>();
            self.AddSingleton<IDataMapping<FruitModel, FruitORM, FruitDTO>, FruitMapping>();
            self.AddScoped<IDataStore<FruitModel>, DataStore<FruitModel, FruitORM, FruitDTO>>();

            self.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            return self;
        }
    }
}
