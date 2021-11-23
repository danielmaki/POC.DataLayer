using Xunit;

using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Data.Test.Unit.Stub.Abstractions
{
    public class MapToExternalData<MODEL, EXTERNAL> : TheoryData where MODEL : IModel where EXTERNAL : IExternal
    {
        /// <summary>
        /// Adds data to the theory data set for mapping an internal model to an external model
        /// </summary>
        /// <param name="model">The internal model</param>
        /// <param name="ext">The external model</param>
        public void Add(MODEL model, EXTERNAL ext)
        {
            AddRow(model, ext);
        }
    }

    public class MapToModelData<EXTERNAL, MODEL> : TheoryData where EXTERNAL : IExternal where MODEL : IModel
    {
        /// <summary>
        /// Adds data to the theory data set for mapping an an external model to internal model
        /// </summary>
        /// <param name="ext">The external model</param>
        /// <param name="model">The internal model</param>
        public void Add(EXTERNAL ext, MODEL model)
        {
            AddRow(ext, model);
        }
    }
}
