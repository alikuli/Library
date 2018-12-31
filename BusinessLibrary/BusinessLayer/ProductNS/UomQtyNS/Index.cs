using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary
{
    public partial class UomQuantityBiz : BusinessLayer<UomQty>
    {

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "UOM Quantity";
//            indexListVM.Record = "Country";
            indexListVM.Show.EditDeleteAndCreate = true;

        }


    }
}
