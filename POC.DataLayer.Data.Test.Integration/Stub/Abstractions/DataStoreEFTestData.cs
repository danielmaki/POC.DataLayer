using Xunit;

using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Data.Test.Integration.Stub.Abstractions
{
    public class CreateValidModelTestData<MODEL> : TheoryData where MODEL : IModel
    {
        /// <summary>
        /// Adds data to the theory data set for creating a valid entry in the data store
        /// </summary>
        /// <param name="inputModel">The data entry to be added to the data store</param>
        /// <param name="outputModel">The data entry returned from the data store</param>
        public void Add(MODEL inputModel, MODEL outputModel)
        {
            AddRow(inputModel, outputModel);
        }
    }

    public class CreateDefaultModelTestData<MODEL> : TheoryData where MODEL : IModel
    {
        /// <summary>
        /// Adds data to the theory data set for creating a valid entry with default data in the data store
        /// </summary>
        /// <param name="inputModel">The data entry to be added to the data store</param>
        /// <param name="outputModel">The default data entry returned from the data store</param>
        public void Add(MODEL inputModel, MODEL outputModel)
        {
            AddRow(inputModel, outputModel);
        }
    }
}
