using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using POC.DataLayer.Data.Context;
using POC.DataLayer.Data.Mappings;

namespace POC.DataLayer.Data.Store
{
    /// <summary>
    /// A generic class for CRUD operations to/from Entity Framework data store
    /// </summary>
    /// <typeparam name="MODEL"></typeparam>
    /// <typeparam name="ORM"></typeparam>
    public class DataStore<MODEL, ORM, DTO> : IDataStore<MODEL> where MODEL : IRequiredProperties where ORM : class, IRequiredProperties
    {
        private readonly ILogger<DataStore<MODEL, ORM, DTO>> logger;
        private readonly ApplicationDbContext context;
        private readonly IDataMapping<MODEL, ORM, DTO> dataMapper;

        public DataStore(ILogger<DataStore<MODEL, ORM, DTO>> logger, ApplicationDbContext context, IDataMapping<MODEL, ORM, DTO> dataMapper)
        {
            this.logger = logger;
            this.context = context;
            this.dataMapper = dataMapper;
        }

        /// <summary>
        /// Get a list of all entities
        /// </summary>
        /// <returns>The list with found entities or empty list if no entities were found</returns>
        public async IAsyncEnumerable<MODEL> GetEntityListAsync()
        {
            var entities = context.Set<ORM>().AsAsyncEnumerable();

            await foreach (var entity in entities)
            {
                yield return dataMapper.ORMToModel(entity);
            }
        }

        /// <summary>
        /// Get an entity by id
        /// </summary>
        /// <param name="id">Must be higher than zero</param>
        /// <returns>The entity if found, else null</returns>
        public async Task<MODEL> GetEntityAsync(long id)
        {
            if (id <= 0)
            {
                return default;
            }

            var entity = await context.Set<ORM>().FindAsync(id);

            if (entity == null)
            {
                return default;
            }

            return dataMapper.ORMToModel(entity);
        }

        /// <summary>
        /// Creates new data store entry for mapped ORM from provided MODEL
        /// </summary>
        /// <param name="model">model must contain Id and be equal to zero</param>
        /// <returns>The new data store entry mapped to MODEL if it was successfully created, else null</returns>
        public async Task<MODEL> CreateEntityAsync(MODEL model)
        {
            if (model == null || model.Id != 0)
            {
                return default;
            }

            var entity = dataMapper.ModelToORM(model);

            context.Set<ORM>().Add(entity);

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

                context.Set<ORM>().Remove(entity);

                return default;
            }

            return dataMapper.ORMToModel(entity);
        }

        /// <summary>
        /// Updates existing data store entry with mapped ORM from provided MODEL
        /// </summary>
        /// <param name="model">model must contain ID and must be higher than zero</param>
        /// <returns>The updated data store entry mapped to MODEL if it was successfully updated, else null</returns>
        public async Task<MODEL> UpdateEntityAsync(MODEL model)
        {
            if (model == null || model.Id <= 0)
            {
                return default;
            }

            var update = dataMapper.ModelToORM(model);

            var entity = await context.Set<ORM>().FindAsync(update.Id);

            if (entity == null)
            {
                return default;
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

                return default;
            }

            return dataMapper.ORMToModel(entity);
        }

        /// <summary>
        /// Deletes the data store entry matching the provided id
        /// </summary>
        /// <param name="id">Must be higher than zero</param>
        /// <returns>The deleted data store entry mapped to MODEL if it was successfully deleted, else null</returns>
        public async Task<MODEL> DeleteEntityAsync(long id)
        {
            if (id <= 0)
            {
                return default;
            }

            var entity = await context.Set<ORM>().FindAsync(id);

            if (entity == null)
            {
                return default;
            }

            context.Set<ORM>().Remove(entity);

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

                context.Set<ORM>().Add(entity);

                return default;
            }

            return dataMapper.ORMToModel(entity);
        }
    }
}
