using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.DataLayer.Data.Store
{
    public interface IDataStore<INTERNAL> where INTERNAL : IRequiredProperties
    {
        public IAsyncEnumerable<INTERNAL> GetEntityListAsync();

        public Task<INTERNAL> GetEntityAsync(long id);

        public Task<INTERNAL> CreateEntityAsync(INTERNAL intl);

        public Task<INTERNAL> UpdateEntityAsync(INTERNAL intl);

        public Task<INTERNAL> DeleteEntityAsync(long id);
    }
}
