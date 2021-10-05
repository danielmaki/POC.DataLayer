using System.Threading.Tasks;

using Xunit;

using POC.DataLayer.Data.Enums;

namespace POC.DataLayer.Data.Test.Integration.Store.Abstractions
{
    public interface IDataStoreFixture<FIXTURE> : IClassFixture<FIXTURE> where FIXTURE : class
    {
        public Task Test1_CreateAsync_Case1_ValidModel(long id, string name, string color, Taste taste);
        public Task Test1_CreateAsync_Case2_DefaultValues(long id, string name, string color, Taste taste);
        public Task Test1_CreateAsync_Case3_InvalidModel(long id, string name, string color, Taste taste);
        public Task Test1_CreateAsync_Case4_InvalidId(long id, string name, string color, Taste taste);
        public Task Test1_CreateAsync_Case5_NullEntity();
        public Task Test1_CreateAsync_Case6_RetryValidModel(long id, string name, string color, Taste taste);
        public Task Test2_UpdateAsync_Case1_ValidModel(string name, string color, Taste taste);
        public Task Test2_UpdateAsync_Case2_InvalidModel(string name, string color, Taste taste);
        public Task Test2_UpdateAsync_Case3_InvalidId(long id);
        public Task Test2_UpdateAsync_Case4_NullEntity();
        public Task Test2_UpdateAsync_Case5_RetryValidModel(string name, string color, Taste taste);
        public Task Test2_UpdateAsync_Case6_NoChange();
        public Task Test3_DeleteAsync_Case1_ValidId();
        public Task Test3_DeleteAsync_Case2_InvalidId(long id);
    }
}
