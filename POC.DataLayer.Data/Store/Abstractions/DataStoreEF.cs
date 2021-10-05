using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Data.Store.Abstractions
{
    /// <summary>
    /// A generic service for CRUD operations to/from Entity Framework data store
    /// </summary>
    /// <typeparam name="MODEL">The internal model used within the solution</typeparam>
    /// <typeparam name="ENTITY">The back facing model representing the EF model</typeparam>
    public abstract class DataStoreEF<MODEL, ENTITY, CONTEXT> : IDataStore<MODEL> where MODEL : IModel where ENTITY : class, IEntity where CONTEXT : DbContext
    {
        private readonly ILogger<DataStoreEF<MODEL, ENTITY, CONTEXT>> logger;

        private readonly CONTEXT context;
        private readonly IDataMap<MODEL, ENTITY> dataMapper;

        private readonly string typeName;

        public DataStoreEF(ILogger<DataStoreEF<MODEL, ENTITY, CONTEXT>> logger, CONTEXT context, IDataMap<MODEL, ENTITY> dataMapper)
        {
            this.logger = logger;

            this.context = context;
            this.dataMapper = dataMapper;

            typeName = typeof(MODEL).ToString();
        }

        /// <summary>
        /// Gets all ENTITY entries
        /// </summary>
        /// <returns>The IAsyncEnumerable containing all ENTITY entries, can be empty if none were found</returns>
        public virtual async IAsyncEnumerable<MODEL> GetAllAsync()
        {
            logger.LogTrace($"Get all {typeName}");

            var entities = context.Set<ENTITY>().AsAsyncEnumerable();

            await foreach (var entity in entities)
            {
                yield return dataMapper.ToModel(entity);
            }
        }

        /// <summary>
        /// Gets the ENTITY entry matching the id
        /// </summary>
        /// <param name="id">must be higher than zero</param>
        /// <returns>The ENTITY entry, if found, else null</returns>
        public virtual async Task<MODEL> GetAsync(long id)
        {
            logger.LogTrace($"Get {typeName}");

            if (id <= 0)
            {
                return default;
            }

            var entity = await context.Set<ENTITY>().FindAsync(id);

            if (entity == null)
            {
                return default;
            }

            return dataMapper.ToModel(entity);
        }

        public virtual Task<MODEL> GetBatchAsync(IEnumerable<long> id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creates a new ENTITY entry from provided MODEL
        /// </summary>
        /// <param name="model">model.Id must be equal to zero</param>
        /// <returns>The new ENTITY entry mapped to MODEL, if it was created successfully, else null</returns>
        public virtual async Task<MODEL> CreateAsync(MODEL model)
        {
            logger.LogTrace($"Create {typeName}");

            if (model == null || model.Id != 0)
            {
                return default;
            }

            var entity = dataMapper.ToExternal(model);

            context.Set<ENTITY>().Add(entity);

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

                // Revert changes
                context.Remove(entity);

                return default;
            }

            return dataMapper.ToModel(entity);
        }

        public virtual Task<MODEL> CreateBatchAsync(IEnumerable<MODEL> model)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Updates the existing ENTITY entry with provided MODEL
        /// </summary>
        /// <param name="model">model.Id must be higher than zero</param>
        /// <returns>The updated ENTITY entry mapped to MODEL, if it was updated successfully, else null</returns>
        public virtual async Task<MODEL> UpdateAsync(MODEL model)
        {
            logger.LogTrace($"Update {typeName}");

            if (model == null || model.Id <= 0)
            {
                return default;
            }

            var update = dataMapper.ToExternal(model);

            var entity = await context.Set<ENTITY>().FindAsync(update.Id);

            if (entity == null)
            {
                return default;
            }

            dataMapper.UpdateExternal(entity, update);

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

                revertChanges(EntityState.Modified);

                return default;
            }

            return dataMapper.ToModel(entity);
        }

        public virtual Task<MODEL> UpdateBatchAsync(IEnumerable<MODEL> model)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Deletes the ENTITY entry matching the provided id
        /// </summary>
        /// <param name="id">must be higher than zero</param>
        /// <returns>The deleted ENTITY entry mapped to MODEL, if it was deleted successfully, else null</returns>
        public virtual async Task<MODEL> DeleteAsync(long id)
        {
            logger.LogTrace($"Delete {typeName}");

            if (id <= 0)
            {
                return default;
            }

            var entity = await context.Set<ENTITY>().FindAsync(id);

            if (entity == null)
            {
                return default;
            }

            context.Set<ENTITY>().Remove(entity);

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

                revertChanges(EntityState.Deleted);

                return default;
            }

            return dataMapper.ToModel(entity);
        }

        public virtual Task<MODEL> DeleteBatchAsync(IEnumerable<long> id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Reverts changes in context
        /// </summary>
        /// <param name="revertState">The state of each ENTITY in context to be reverted</param>
        protected void revertChanges(EntityState revertState)
        {
            context.ChangeTracker.Entries()
                .Where(x => x.State == revertState).ToList().ForEach(entry => {
                    entry.State = EntityState.Unchanged;
                    entry.CurrentValues.SetValues(entry.OriginalValues);
                });
        }
    }
}
