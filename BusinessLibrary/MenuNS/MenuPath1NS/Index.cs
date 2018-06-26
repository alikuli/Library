using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;

namespace UowLibrary.MenuNS
{
    public partial class MenuPath1Biz 
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Menu Path 1";
            //indexListVM.MainHeading = "Product Category 1";
            indexListVM.IsImageTiled = true;
            indexListVM.Show.EditDeleteAndCreate = false;
            //we need to make  indexListVM.Show.EditDeleteAndCreate false because we want to control
            //the view models that are created to make the product and that will be based on the
            //industry that the product belongs to, i.e. MenuPath1

        }



    }
}
