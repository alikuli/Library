using InterfacesLibrary.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;

namespace ModelsClassLibrary.ModelsNS.ProductNS
{
    public partial class ProductCategoryMain : ProductCategoryAbstract, IProductCategoryMain, IHasUploads
    {

        public override bool HideNameInView()
        {
            return true; ;
        }



        public string MiscFilesLocation
        {
            get { return AliKuli.ConstantsNS.MyConstants.SAVE_LOCATION_PRODUCT_MAIN_CATEGORY; }
        }
    }
}