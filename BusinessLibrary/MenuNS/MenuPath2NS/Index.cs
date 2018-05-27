using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;
namespace UowLibrary.ProductNS
{
    public partial class MenuPath2Biz 
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Menu Paths 2";
            //indexListVM.MainHeading = "Product Category 2";
            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = true;

        }




    }
}
