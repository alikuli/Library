using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UserModels;

namespace UowLibrary
{
    public partial class UserBiz : BusinessLayer<ApplicationUser>
    {

        public override void Fix(ControllerCreateEditParameter parm)
        {
            ApplicationUser appUser = parm.Entity as ApplicationUser;
            appUser.IsNullThrowException("Unable to unbox user");

            appUser.Name = appUser.UserName;
            base.Fix(parm);

            //if (appUser.CountryId.IsNullOrWhiteSpace())
            //    appUser.CountryId = null;

            if (appUser.PersonId.IsNullOrWhiteSpace())
                appUser.PersonId = null;

            bool newBlackListed = !appUser.BlackListOldValue && appUser.BlackListed.Value;
            bool newUnBlackListed = appUser.BlackListOldValue && !appUser.BlackListed.Value;

            bool newSuspended = !appUser.SuspendedOldValue && appUser.Suspended.Value;
            bool newUnSuspended = appUser.SuspendedOldValue && !appUser.Suspended.Value;

            if (newBlackListed)
                appUser.BlackListed.Activate(UserName);

            if (newUnBlackListed)
                appUser.BlackListed.Deactivate(UserName);

            if (newSuspended)
                appUser.Suspended.Activate(UserName);

            if (newUnSuspended)
                appUser.Suspended.Deactivate(UserName);
        }



    }
}
