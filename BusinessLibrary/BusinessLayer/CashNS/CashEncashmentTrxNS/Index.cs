using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.CashEncashmentTrxNS
{
    public partial class CashEncashmentTrxBiz
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Cash Payment Methods";
            indexListVM.Show.EditDeleteAndCreate = true;

        }


    }
}
