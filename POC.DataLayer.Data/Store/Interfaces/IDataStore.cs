using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.DataLayer.Data.Store
{
    public interface IDataStore<MODEL>
    {
        public IAsyncEnumerable<MODEL> GetEntityListAsync();

        public Task<MODEL> GetEntityAsync(long id);

        public Task<MODEL> CreateEntityAsync(MODEL model);

        public Task<MODEL> UpdateEntityAsync(MODEL model);

        public Task<MODEL> DeleteEntityAsync(long id);
    }
}
