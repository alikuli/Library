using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;

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
            uploadedFile.IsNullThrowException("Unable to convert to Upload");


            indexItem.Name = string.Format("{0}", uploadedFile.MetaData.Created.Date_NotNull_Min.ToLongDateString());

            //this is the tool tip.
            if (indexItem.Description.IsNullOrWhiteSpace())
                indexItem.Description = string.Format("This is a file uploaded by you on {0}.", uploadedFile.MetaData.Created.Date_NotNull_Min.ToLongDateString());

            indexItem.ImageAddressStr = uploadedFile.RelativePathWithFileName();
        }



    }
}
