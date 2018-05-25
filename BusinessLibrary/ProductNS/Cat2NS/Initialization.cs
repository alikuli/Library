using DatastoreNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace UowLibrary.ProductNS
{
    public partial class ProductCat2Biz : BusinessLayer<ProductCategory2>
    {


        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return ProductCat2Array.DataArray();
            }
        }

        

    }
}
