namespace POC.DataLayer.Data.Mappings
{
    public interface IDataMapping<MODEL, ORM, DTO>
    {
        public ORM CopyORM(ORM orm);

        public MODEL DTOToModel(DTO dto);

        public DTO ModelToDTO(MODEL model);

        public ORM ModelToORM(MODEL model);

        public MODEL ORMToModel(ORM orm);

        public void UpdateORM(ORM orm, ORM update);
    }
}
