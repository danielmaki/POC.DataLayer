﻿using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Moq;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.Store;

namespace POC.DataLayer.Data.Test.Store
{
    public class DbContextFixture : IDisposable
    {
        public ApplicationDbContext context { get; private set; }
        public IDataStore<FruitModel> dataStore { get; private set; }

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

            var logger = new Mock<ILogger<DataStore<FruitModel, FruitORM>>>(MockBehavior.Loose);
            var dataMapper = new FruitBackFacingMap();

            dataStore = new DataStore<FruitModel, FruitORM>(logger.Object, context, dataMapper);
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
