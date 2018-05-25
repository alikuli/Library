using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;

namespace UowLibrary.UploadFileNS
{
    public partial class UploadedFileBiz : BusinessLayer<UploadedFile>
    {

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Uploads";
            //indexListVM.MainHeading = "Various Uploads";

            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = true;



        }

        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithid)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithid);

            UploadedFile uploadedFile = icommonWithid as UploadedFile;

            if (uploadedFile.IsNull())
            {
                ErrorsGlobal.Add("Unable to convert to product Category Main", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }


            indexItem.ImageAddressStr = uploadedFile.RelativePathWithFileName();
        }



    }
}
