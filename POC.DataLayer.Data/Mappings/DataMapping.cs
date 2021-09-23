using System;

namespace POC.DataLayer.Data.Mappings
{
    public abstract class DataMapping<MODEL, ORM, DTO> : IDataMapping<MODEL, ORM, DTO>
    {
        public abstract ORM CopyORM(ORM orm);

        public abstract MODEL DTOToModel(DTO dto);

        public abstract DTO ModelToDTO(MODEL model);

        public abstract ORM ModelToORM(MODEL model);

        public abstract MODEL ORMToModel(ORM orm);

        public abstract void UpdateORM(ORM orm, ORM update);
    }
}
