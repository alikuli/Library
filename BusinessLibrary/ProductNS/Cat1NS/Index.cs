using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;
namespace UowLibrary.ProductNS
{
    public partial class ProductCat1Biz : BusinessLayer<ProductCategory1>
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Product Category 1";
            //indexListVM.MainHeading = "Product Category 1";
            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = true;

        }



    }
}
