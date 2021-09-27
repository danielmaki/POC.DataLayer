using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.DTO;
using POC.DataLayer.Data.Mappings;
using POC.DataLayer.Data.Models;
using POC.DataLayer.Data.ORM;

namespace POC.DataLayer.Data.Store
{
    public class FruitDataStore
    {
        private readonly ILogger<FruitDataStore> logger;
        private readonly ApplicationDbContext context;
        private readonly IDataMapping<FruitModel, FruitORM, FruitDTO> dataMapper;

        public FruitDataStore(ILogger<FruitDataStore> logger, ApplicationDbContext context, IDataMapping<FruitModel, FruitORM, FruitDTO> dataMapper)
        {
            this.logger = logger;
            this.context = context;
            this.dataMapper = dataMapper;
        }

        /// <summary>
        /// Get the list of entities
        /// </summary>
        /// <returns>The list with found entities or empty list if no entities were found</returns>
        public async IAsyncEnumerable<FruitModel> GetEntityListAsync()
        {
            var entities = context.Fruits.AsAsyncEnumerable();

            await foreach (var entity in entities)
            {
                yield return dataMapper.ORMToModel(entity);
            }
        }

        /// <summary>
        /// Get the entity by id
        /// </summary>
        /// <param name="id">Must be higher than zero</param>
        /// <returns>The entity if found, else null</returns>
        public async Task<FruitModel> GetEntityAsync(long id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = await context.Fruits.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            return dataMapper.ORMToModel(entity);
        }

        /// <summary>
        /// Creates new entity from the corresponding model
        /// </summary>
        /// <param name="model">model.Id must be equal to zero</param>
        /// <returns>The new entity if it was created, else null</returns>
        public async Task<FruitModel> CreateEntityAsync(FruitModel model)
        {
            if (model == null || model.Id != 0)
            {
                return null;
            }

            var entity = dataMapper.ModelToORM(model);

            context.Fruits.Add(entity);

            try
            {
                if (await context.SaveChangesAsync() != 1)
                {
                    throw new DbUpdateException("The entity was not added to the database.");
                }
            }
            catch (DbUpdateException error)
            {
                logger.LogWarning("Failed to create entity, reverting context: " + error.Message);

                context.Fruits.Remove(entity);

                return null;
            }

            return dataMapper.ORMToModel(entity);
        }

        /// <summary>
        /// Updates the entity with the provided model
        /// </summary>
        /// <param name="model">model.Id must be higher than zero</param>
        /// <returns>The updated entity model if it was updated, else null</returns>
        public async Task<FruitModel> UpdateEntityAsync(FruitModel model)
        {
            if (model == null || model.Id <= 0)
            {
                return null;
            }

            var update = dataMapper.ModelToORM(model);

            var entity = await context.Fruits.FindAsync(update.Id);

            if (entity == null)
            {
                return null;
            }

            var prevEntity = dataMapper.CopyORM(entity);

            dataMapper.UpdateORM(entity, update);

            try
            {
                if (await context.SaveChangesAsync() != 1)
                {
                    throw new DbUpdateException("The entity was not updated in the database.");
                }
            }
            catch (DbUpdateException error)
            {
                logger.LogWarning("Failed to update entity, reverting context: " + error.Message);

                // Revert update
                context.ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Modified).ToList().ForEach(entry => {
                        entry.State = EntityState.Unchanged;
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                    });

                return null;
            }

            return dataMapper.ORMToModel(entity);
        }

        /// <summary>
        /// Deletes the entity matching the provided id
        /// </summary>
        /// <param name="id">Must be higher than zero</param>
        /// <returns>The deleted entity model if it was deleted, else null</returns>
        public async Task<FruitModel> DeleteEntityAsync(long id)
        {
            if (id <= 0)
            {
                return null;
            }

            var entity = await context.Fruits.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            context.Fruits.Remove(entity);

            try
            {
                if (await context.SaveChangesAsync() != 1)
                {
                    throw new DbUpdateException("The entity was not removed from the database.");
                }
            }
            catch (DbUpdateException error)
            {
                logger.LogWarning("Failed to remove entity, reverting context: " + error.Message);

                context.Fruits.Add(entity);

                return null;
            }

            return dataMapper.ORMToModel(entity);
        }
    }
}
