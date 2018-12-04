using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using UserModels;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Mailer mailer = parm.Entity as Mailer;

            //update the User as well.
            //mailer.UserId.IsNullOrWhiteSpaceThrowArgumentException("No user ID");
            //locate User

            //we cannot send the logged in user's Id when creating a mailer, we need to send in the 
            //mailers UserId, especially during create

            //ApplicationUser mailerUser = UserBiz.Find(mailer.UserId);
            //mailerUser.IsNullThrowException("User for Mailer not found");

            //dont do this!! This causes terrible errors of data concurrency etc. Entity Framwork will do this
            //mailer.User = mailerUser;
            //mailer.Name = mailerUser.UserName;
        }

    }
}
