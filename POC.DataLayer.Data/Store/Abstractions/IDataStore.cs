using System.Collections.Generic;
using System.Threading.Tasks;

using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Data.Store.Abstractions
{
    /// <summary>
    /// A generic interface for CRUD operations to/from a data store
    /// </summary>
    /// <typeparam name="MODEL">The internal model used within the solution</typeparam>
    public interface IDataStore<MODEL> where MODEL : IModel
    {
        /// <summary>
        /// Gets all entries
        /// </summary>
        /// <returns>The IAsyncEnumerable containing all entries, can be empty if none were found</returns>
        public IAsyncEnumerable<MODEL> GetAllAsync();

        /// <summary>
        /// Gets the entry matching the id
        /// </summary>
        /// <param name="id">must be higher than zero</param>
        /// <returns>The entry, if found, else null</returns>
        public Task<MODEL> GetAsync(long id);

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<MODEL> GetBatchAsync(IEnumerable<long> id);

        /// <summary>
        /// Creates a new entry from provided MODEL
        /// </summary>
        /// <param name="model">model.Id must be equal to zero</param>
        /// <returns>The new entry mapped to MODEL, if it was created successfully, else null</returns>
        public Task<MODEL> CreateAsync(MODEL model);

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<MODEL> CreateBatchAsync(IEnumerable<MODEL> model);

        /// <summary>
        /// Updates the existing entry with provided MODEL
        /// </summary>
        /// <param name="model">model.Id must be higher than zero</param>
        /// <returns>The updated entry mapped to MODEL, if it was updated successfully, else null</returns>
        public Task<MODEL> UpdateAsync(MODEL model);

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<MODEL> UpdateBatchAsync(IEnumerable<MODEL> model);

        /// <summary>
        /// Deletes the entry matching the provided id
        /// </summary>
        /// <param name="id">must be higher than zero</param>
        /// <returns>The deleted entry mapped to MODEL, if it was deleted successfully, else null</returns>
        public Task<MODEL> DeleteAsync(long id);

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<MODEL> DeleteBatchAsync(IEnumerable<long> id);
    }
}
