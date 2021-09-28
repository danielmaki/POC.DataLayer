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
    /// <typeparam name="INTERNAL"></typeparam>
    /// <typeparam name="EXTERNAL"></typeparam>
    public class DataStore<INTERNAL, EXTERNAL> : IDataStore<INTERNAL> where INTERNAL : IRequiredProperties where EXTERNAL : class, IRequiredProperties
    {
        private readonly ILogger<DataStore<INTERNAL, EXTERNAL>> logger;
        private readonly ApplicationDbContext context;
        private readonly IDataMap<INTERNAL, EXTERNAL> dataMapper;

        public DataStore(ILogger<DataStore<INTERNAL, EXTERNAL>> logger, ApplicationDbContext context, IDataMap<INTERNAL, EXTERNAL> dataMapper)
        {
            this.logger = logger;
            this.context = context;
            this.dataMapper = dataMapper;
        }

        /// <summary>
        /// Get a list of all entities
        /// </summary>
        /// <returns>The list with found entities or empty list if no entities were found</returns>
        public async IAsyncEnumerable<INTERNAL> GetEntityListAsync()
        {
            var entities = context.Set<EXTERNAL>().AsAsyncEnumerable();

            await foreach (var entity in entities)
            {
                yield return dataMapper.ExtToIntl(entity);
            }
        }

        /// <summary>
        /// Get an entity by id
        /// </summary>
        /// <param name="id">Must be higher than zero</param>
        /// <returns>The entity if found, else null</returns>
        public async Task<INTERNAL> GetEntityAsync(long id)
        {
            if (id <= 0)
            {
                return default;
            }

            var entity = await context.Set<EXTERNAL>().FindAsync(id);

            if (entity == null)
            {
                return default;
            }

            return dataMapper.ExtToIntl(entity);
        }

        /// <summary>
        /// Creates new data store entry for mapped EXTERNAL model from provided INTERNAL model
        /// </summary>
        /// <param name="intl">INTERNAL model must contain Id and be equal to zero</param>
        /// <returns>The new data store entry mapped to INTERNAL if it was successfully created, else null</returns>
        public async Task<INTERNAL> CreateEntityAsync(INTERNAL intl)
        {
            if (intl == null || intl.Id != 0)
            {
                return default;
            }

            var entity = dataMapper.IntlToExt(intl);

            context.Set<EXTERNAL>().Add(entity);

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

                context.Set<EXTERNAL>().Remove(entity);

                return default;
            }

            return dataMapper.ExtToIntl(entity);
        }

        /// <summary>
        /// Updates existing data store entry with mapped EXTERNAL model from provided INTERNAL model
        /// </summary>
        /// <param name="intl">INTERNAL model must contain Id property and must be higher than zero</param>
        /// <returns>The updated data store entry mapped to INTERNAL if it was successfully updated, else null</returns>
        public async Task<INTERNAL> UpdateEntityAsync(INTERNAL intl)
        {
            if (intl == null || intl.Id <= 0)
            {
                return default;
            }

            var update = dataMapper.IntlToExt(intl);

            var entity = await context.Set<EXTERNAL>().FindAsync(update.Id);

            if (entity == null)
            {
                return default;
            }

            var prevEntity = dataMapper.CopyExt(entity);

            dataMapper.UpdateExt(entity, update);

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

            return dataMapper.ExtToIntl(entity);
        }

        /// <summary>
        /// Deletes the data store entry matching the provided id
        /// </summary>
        /// <param name="id">Must be higher than zero</param>
        /// <returns>The deleted data store entry mapped to INTERNAL if it was successfully deleted, else null</returns>
        public async Task<INTERNAL> DeleteEntityAsync(long id)
        {
            if (id <= 0)
            {
                return default;
            }

            var entity = await context.Set<EXTERNAL>().FindAsync(id);

            if (entity == null)
            {
                return default;
            }

            context.Set<EXTERNAL>().Remove(entity);

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

                context.Set<EXTERNAL>().Add(entity);

                return default;
            }

            return dataMapper.ExtToIntl(entity);
        }
    }
}
