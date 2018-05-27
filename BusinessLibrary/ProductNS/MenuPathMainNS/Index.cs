using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Reflection;
namespace UowLibrary.ProductNS
{
    public partial class MenuPathMainBiz 
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Menu Mains";
            //indexListVM.MainHeading = "Product Category 3";
            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = true;

        }

        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithid)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithid);
            MenuPathMain menupathMain = icommonWithid as MenuPathMain;

            if (menupathMain.IsNull())
            {
                ErrorsGlobal.Add("Unable to convert to Menu Main", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            indexItem.ImageAddressStr = selectAddressOfImageToDisplay(menupathMain);
        }

        private string selectAddressOfImageToDisplay(MenuPathMain menupathmain)
        {
            //Get a list of images for this category item.
            UploadedFile image = menupathmain.MiscFiles.FirstOrDefault(x => x.MetaData.IsDeleted == false);

            if (image.IsNull())
                image = new UploadedFile();


            return image.RelativePathWithFileName();
        }


    }
}
