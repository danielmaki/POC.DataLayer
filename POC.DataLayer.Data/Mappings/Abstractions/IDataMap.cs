using POC.DataLayer.Data.Models.Abstractions;

namespace POC.DataLayer.Data.Mappings.Abstractions
{
    /// <summary>
    /// A generic interface for mappings between internal MODEL instances and EXTERNAL facing model instances
    /// </summary>
    /// <typeparam name="MODEL">The MODEL that is used within the solution</typeparam>
    /// <typeparam name="EXTERNAL">The EXTERNAL facing model e.g. DTO or EF model</typeparam>
    public interface IDataMap<MODEL, EXTERNAL> where MODEL : IModel where EXTERNAL : IExternal
    {
        /// <summary>
        /// Maps an EXTERNAL model instance to a new MODEL instance
        /// </summary>
        /// <param name="ext"></param>
        /// <returns>A new MODEL model instance</returns>
        public MODEL ToModel(EXTERNAL ext);

        /// <summary>
        /// Maps a MODEL instance to a new EXTERNAL model instance
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A new EXTERNAL model instance</returns>
        public EXTERNAL ToExternal(MODEL model);

        /// <summary>
        /// Updates an EXTERNAL model instance with all properties from given EXTERNAL model instance except for its Id propery
        /// </summary>
        /// <param name="ext">all properties but ext.Id will be updated</param>
        /// <param name="update"></param>
        public void UpdateExternal(EXTERNAL ext, EXTERNAL update);
    }
}
