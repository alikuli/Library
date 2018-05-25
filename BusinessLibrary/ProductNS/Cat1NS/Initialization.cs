using DatastoreNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace UowLibrary.ProductNS

{
    public partial class ProductCat1Biz : BusinessLayer<ProductCategory1>
    {


        public override bool Event_LockEditDuringInitialization()
        {
            return false;
        }

        public override string[] GetDataForStringArrayFormat
        {
            get
            {
                return ProductCat1Array.DataArray();
            }
        }






    }
}
