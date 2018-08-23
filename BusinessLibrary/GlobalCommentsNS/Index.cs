using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.GlobalCommentsNS
{
    public partial class GlobalCommentBiz : BusinessLayer<GlobalComment>

    {
        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "All Comments";
            indexListVM.Show.EditDeleteAndCreate = true;

        }


    }
}
