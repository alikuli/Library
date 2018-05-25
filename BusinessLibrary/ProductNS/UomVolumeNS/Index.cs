using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary
{
    public partial class UomVolumeBiz : BusinessLayer<UomVolume>
    {

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "UOM Volume";
//            indexListVM.Record = "Country";
            indexListVM.Show.EditDeleteAndCreate = true;

        }


    }
}
