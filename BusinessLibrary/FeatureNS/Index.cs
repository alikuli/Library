using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.FeaturesNS

{
    public partial class FeatureBiz
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            //indexListVM.Heading.Column = "All Languages";
            indexListVM.Show.EditDeleteAndCreate = true;

            //indexListVM.Records = "Languages";
        }


    }
}
