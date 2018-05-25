using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.CounterNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.CounterNS

{
    public partial class CounterBiz : BusinessLayer<Counter>
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Show.EditDeleteAndCreate = false;
            indexListVM.Show.Create = true;

            //indexListVM.Records = "Languages";
        }


    }
}
