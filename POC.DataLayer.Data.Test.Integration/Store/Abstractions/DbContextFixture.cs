using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.Models.Abstractions;
using POC.DataLayer.Data.Store.Abstractions;

namespace POC.DataLayer.Data.Test.Integration.Store.Abstractions
{
    public abstract class DbContextFixture<CONTEXT> : IDisposable where CONTEXT : DbContext, new()
    {
        public ApplicationDbContext context { get; private set; }
        public abstract IDataStore<IModel> dataStore { get; }

        public DbContextFixture()
        {
            var connectionString = $"Server=(localdb)\\mssqllocaldb;Database=POC.DataLayer.Test.Integration.{Guid.NewGuid()}; Trusted_Connection=True; MultipleActiveResultSets=true";

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
