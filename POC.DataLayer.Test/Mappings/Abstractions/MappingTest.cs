using System;

using Xunit;

using POC.DataLayer.Data.Mappings.Abstractions;
using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Data.Test.Unit.Mappings.Abstractions
{
    public abstract class MappingTest<MAP, MODEL, EXTERNAL> : IMappingTest<MODEL, EXTERNAL> where MAP : IDataMap<MODEL, EXTERNAL>, new() where MODEL : IModel where EXTERNAL : IExternal, new()
    {
        public virtual void ToExternal(MODEL model, EXTERNAL ext)
        {
            // Setup
            var mapping = new MAP();

            // Execute
            var result = mapping.ToExternal(model);

            // Verify
            Assert.Equal(ext.GetType().GetProperties(), result.GetType().GetProperties());
            foreach (var property in result.GetType().GetProperties())
            {
                Assert.Equal(property.GetValue(ext), property.GetValue(result));
            }
        }

        [Fact]
        public virtual void ToExternal_Null()
        {
            // Setup
            var mapping = new MAP();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ToExternal(default));
        }

        public virtual void ToModel(EXTERNAL ext, MODEL model)
        {
            // Setup
            var mapping = new MAP();

            // Execute
            var result = mapping.ToModel(ext);

            // Verify
            Assert.Equal(model.GetType().GetProperties(), result.GetType().GetProperties());
            foreach (var property in result.GetType().GetProperties())
            {
                Assert.Equal(property.GetValue(model), property.GetValue(result));
            }
        }

        [Fact]
        public virtual void ToModel_Null()
        {
            // Setup
            var mapping = new MAP();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.ToModel(default));
        }

        [Fact]
        public virtual void UpdateExternal()
        {
            // Setup
            var mapping = new MAP();
            var init = new EXTERNAL()
            {
                Id = 0
            };
            var update = new EXTERNAL()
            {
                Id = 1
            };

            // Execute
            mapping.UpdateExternal(init, update);

            // Verify
            Assert.NotEqual(init.Id, update.Id);
        }

        [Fact]
        public virtual void UpdateExternal_Null()
        {
            // Setup
            var mapping = new MAP();

            // Verify
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExternal(new EXTERNAL(), default));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExternal(default, new EXTERNAL()));
            Assert.Throws<NullReferenceException>(() => mapping.UpdateExternal(default, default));
        }
    }
}
