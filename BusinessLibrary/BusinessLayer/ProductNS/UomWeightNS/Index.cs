using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary
{
    public partial class UomWeightBiz : BusinessLayer<UomWeight>
    {

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "UOM Weight";
//            indexListVM.Record = "Country";
            indexListVM.Show.EditDeleteAndCreate = true;

        }


    }
}
