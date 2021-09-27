namespace POC.DataLayer.Data.Mappings
{
    /// <summary>
    /// Abstract class for mappings between a data store model, internal model and dto model
    /// </summary>
    /// <typeparam name="MODEL">The MODEL that is used internally in the solution</typeparam>
    /// <typeparam name="ORM">The ORM that matched the data store model</typeparam>
    /// <typeparam name="DTO">The DTO that matched the externally visible properties</typeparam>
    public abstract class DataMapping<MODEL, ORM, DTO> : IDataMapping<MODEL, ORM, DTO>
    {
        /// <summary>
        /// Copies all ORM properties into a new ORM instance except for the ID property
        /// </summary>
        /// <param name="orm"></param>
        /// <returns>A new ORM instance with zero assigned to its ID property</returns>
        public abstract ORM CopyORM(ORM orm);

        /// <summary>
        /// Maps a DTO instance to a new MODEL instance
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>a new MODEL</returns>
        public abstract MODEL DTOToModel(DTO dto);

        /// <summary>
        /// Maps a MODEL instance to a new DTO instance
        /// </summary>
        /// <param name="model"></param>
        /// <returns>a new DTO instance</returns>
        public abstract DTO ModelToDTO(MODEL model);

        /// <summary>
        /// Maps a MODEL instance to a new ORM instance
        /// </summary>
        /// <param name="model"></param>
        /// <returns>a new ORM instance</returns>
        public abstract ORM ModelToORM(MODEL model);

        /// <summary>
        /// Maps an ORM instance to a new MODEL instance
        /// </summary>
        /// <param name="orm"></param>
        /// <returns>a new MODEL instance</returns>
        public abstract MODEL ORMToModel(ORM orm);

        /// <summary>
        /// Updates an ORM instance with all properties from another ORM instance except for its ID propery
        /// </summary>
        /// <param name="orm"></param>
        /// <param name="update"></param>
        public abstract void UpdateORM(ORM orm, ORM update);
    }
}
