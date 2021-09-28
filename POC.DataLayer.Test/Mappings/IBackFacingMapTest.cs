using POC.DataLayer.Data.Enums;

namespace POC.DataLayer.Data.Test.Mappings
{
    public interface IBackFacingMapTest
    {
        public void CopyExt(long id, string name, string color, int tasteExt);

        public void CopyExt_Null();

        public void IntlToExt(long id, string name, string color, Taste tasteIntl, int tasteExt);

        public void IntlToExt_Null();

        public void ExtToIntl(long id, string name, string color, Taste tasteIntl, int tasteExt);

        public void ExtToIntl_Null();

        public void UpdateExt(long id, string name, string color, int taste);

        public void UpdateExt_Null();
    }
}
