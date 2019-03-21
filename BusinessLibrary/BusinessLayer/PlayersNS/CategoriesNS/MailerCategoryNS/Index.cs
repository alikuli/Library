using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.PlayersNS.MailerCategoryNS
{
    public partial class MailerCategoryBiz
    {


        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Mailer Category";
            indexListVM.Show.EditDeleteAndCreate = true;

        }





    }
}
