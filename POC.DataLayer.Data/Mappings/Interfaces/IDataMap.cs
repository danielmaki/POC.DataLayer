namespace POC.DataLayer.Data.Mappings
{
    /// <summary>
    /// Interface for mapping between INTERNAL and EXTERNAL facing models
    /// </summary>
    /// <typeparam name="INTERNAL">The INTERNAL model that is used within the solution</typeparam>
    /// <typeparam name="EXTERNAL">The EXTERNAL facing model e.g. DTO or EF model</typeparam>
    public interface IDataMap<INTERNAL, EXTERNAL> where INTERNAL : IRequiredProperties where EXTERNAL : IRequiredProperties
    {
        /// <summary>
        /// Copies all properties from the given EXTERNAL model instance into a new EXTERNAL model instance except for the Id property
        /// </summary>
        /// <param name="ext"></param>
        /// <returns>A new EXTERNAL model instance with zero assigned to its Id property</returns>
        public EXTERNAL CopyExt(EXTERNAL ext);

        /// <summary>
        /// Maps an EXTERNAL model instance to a new INTERNAL model instance
        /// </summary>
        /// <param name="ext"></param>
        /// <returns>A new INTERNAL model instance</returns>
        public INTERNAL ExtToIntl(EXTERNAL ext);

        /// <summary>
        /// Maps an INTERNAL model instance to a new EXTERNAL model instance
        /// </summary>
        /// <param name="int"></param>
        /// <returns>A new external model instance</returns>
        public EXTERNAL IntlToExt(INTERNAL intl);

        /// <summary>
        /// Updates an EXTERNAL model instance with all properties from given EXTERNAL model instance except for its ID propery
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="update"></param>
        public void UpdateExt(EXTERNAL ext, EXTERNAL update);
    }
}
