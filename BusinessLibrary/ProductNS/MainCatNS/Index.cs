using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Reflection;
namespace UowLibrary.ProductNS
{
    public partial class ProductCatMainBiz : BusinessLayer<ProductCategoryMain>
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Product Category";
            //indexListVM.MainHeading = "Product Category 3";
            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = true;

        }

        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithid)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithid);
            ProductCategoryMain productCategoryMain = icommonWithid as ProductCategoryMain;

            if (productCategoryMain.IsNull())
            {
                ErrorsGlobal.Add("Unable to convert to product Category Main", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            indexItem.ImageAddressStr = selectAddressOfImageToDisplay(productCategoryMain);
        }

        private string selectAddressOfImageToDisplay(ProductCategoryMain ProductCategoryMain)
        {
            //Get a list of images for this category item.
            UploadedFile image = ProductCategoryMain.MiscFiles.FirstOrDefault(x => x.MetaData.IsDeleted == false);

            if (image.IsNull())
                image = new UploadedFile();


            return image.RelativePathWithFileName();
        }


    }
}
