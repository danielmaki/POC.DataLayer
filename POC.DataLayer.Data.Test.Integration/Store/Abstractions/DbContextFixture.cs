using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Moq;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.Mappings.BackFacing;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.Store;
using POC.DataLayer.Data.Store.Abstractions;

namespace POC.DataLayer.Data.Test.Integration.Store.Abstractions
{
    public class DbContextFixture : IDisposable
    {
        public ApplicationDbContext context { get; private set; }
        public IDataStore<Fruit> dataStore { get; private set; }

        public DbContextFixture()
        {
            var connectionString = $"Server=(localdb)\\mssqllocaldb;Database=POC.DataLayer.Test-{Guid.NewGuid()}; Trusted_Connection=True; MultipleActiveResultSets=true";

            var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkSqlServer()
            .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            context = new ApplicationDbContext(options);
            context.Database.Migrate();

            var logger = new Mock<ILogger<FruitDataStore>>(MockBehavior.Loose);
            var dataMapper = new FruitBackFacingMap();

            dataStore = new FruitDataStore(logger.Object, context, dataMapper);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Database.EnsureDeleted();
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}
