using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Data.Test.Unit.Mappings.Abstractions
{
    public interface IMappingTest<MODEL, EXTERNAL> where MODEL : IModel where EXTERNAL : IExternal
    {
        public abstract void ToExternal(MODEL model, EXTERNAL ext);

        public void ToExternal_Null();

        public abstract void ToModel(EXTERNAL ext, MODEL model);

        public void ToModel_Null();
        public void UpdateExternal(EXTERNAL init, EXTERNAL update, EXTERNAL expected);

        public void UpdateExternal_Null();
    }
}
