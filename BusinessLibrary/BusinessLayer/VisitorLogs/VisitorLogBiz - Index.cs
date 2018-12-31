using ModelsClassLibrary.ModelsNS.Logs.VisitorsLogNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.VisitorLogNS
{
    public partial class VisitorLogBiz : BusinessLayer<VisitorLog>
    {



        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.NameInput1 = "Name";
            indexListVM.NameInput2 = "Controller";
            indexListVM.Heading.Column = "Visitor Logging";
            
            indexListVM.Show.EditDeleteAndCreate = true;


        }


    }
}
