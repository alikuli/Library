using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary
{
    public partial class CountryBiz : BusinessLayer<Country>
    {

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "[Abbreviation] - Country Name";
//            indexListVM.Record = "Country";
            indexListVM.Heading.RecordNamePlural = "Countries";
            indexListVM.Show.EditDeleteAndCreate = true;

        }


    }
}
