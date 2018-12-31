using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.FeatureNS.MenuFeatureNS
{
    public partial class MenuFeatureBiz
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Menu Features";
            indexListVM.Show.EditDeleteAndCreate = true;

        }


    }
}
