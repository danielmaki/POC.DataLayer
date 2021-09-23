using POC.DataLayer.Data.Enums;

namespace POC.DataLayer.Data.Test.Mappings
{
    public interface IDataMappingTest
    {
        public void CopyORM(long id, string name, string color, int tasteORM);

        public void CopyORM_Null();

        public void DTOToModel(long id, string name, string color, Taste tasteModel, string tasteDTO);

        public void DTOToModel_Null();

        public void ModelToDTO(long id, string name, string color, Taste tasteModel, string tasteDTO);

        public void ModelToDTO_Null();

        public void ModelToORM(long id, string name, string color, Taste tasteModel, int tasteORM);

        public void ModelToORM_Null();

        public void ORMToModel(long id, string name, string color, Taste tasteModel, int tasteORM);

        public void ORMToModel_Null();

        public void UpdateORM(long id, string name, string color, int taste);

        public void UpdateORM_Null();
    }
}
