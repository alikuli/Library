using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;
using System;
using System.Linq;
using System.Reflection;
using UserModels;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {

        /// <summary>
        /// This creates and saves a mailer. It makes sure that there is only one created.
        /// Also, it ensures that deposit/account are taken care of
        /// </summary>
        /// <param name="vm"></param>
        public void CreateAndSaveMailer(CreateMailerVM vm)
        {
            //ControllerCreateEditParameter para
            vm.UserId.IsNullOrWhiteSpaceThrowArgumentException("User is required!");
            ApplicationUser user = UserBiz.Find(vm.UserId);
            user.IsNullThrowException("User not found");
            user.PersonId.IsNullOrWhiteSpaceThrowException("No person attached to user");
            string personId = user.PersonId;

            //first check if a mailer exists for the mailer
            bool mailerExists = FindAll().Any(x => x.PersonId == personId);
            
            if (mailerExists)
            {
                ErrorsGlobal.Add("Mailer already exists for user!", MethodBase.GetCurrentMethod(), null);
                string error = ErrorsGlobal.ToString();
                throw new Exception(error);
            }


            //Make sure user has enough deposit etc to become a mailer
            //todo 
            ErrorsGlobal.AddMessage("Financial checks need to be done!");

            Mailer mailer = new Mailer();

            mailer.Name = user.UserName;
            mailer.PersonId= user.PersonId;
            mailer.TrustLevelEnum = vm.TrustLevelEnum;
            //user.Mailers.Add(mailer);
            //user.MailerId = mailer.Id;

            CreateAndSave(mailer);
            ErrorsGlobal.AddMessage(string.Format("Mailer created for {0}", user.UserName));

        }

    }
}
