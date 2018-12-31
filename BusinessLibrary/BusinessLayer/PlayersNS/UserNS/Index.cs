using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ViewModels;
using System.Reflection;
using UserModels;
using AliKuli.Extentions;
using System;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace UowLibrary
{
    public partial class UserBiz : BusinessLayer<ApplicationUser>
    {
        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);

            indexListVM.Heading.Column = "Users";
            indexListVM.Show.EditDeleteAndCreate = true;
            indexListVM.Show.Create = false;

        }

        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithid)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithid);

            ApplicationUser user = icommonWithid as ApplicationUser;

            if (user.IsNull())
            {
                ErrorsGlobal.Add("Unable to convert to User", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            indexItem.Name = string.Format("{0} [{1}]", user.UserName, user.PhoneNumber);
        }
    }
}
