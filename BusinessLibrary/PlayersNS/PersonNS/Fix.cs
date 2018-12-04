using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using UserModels;

namespace UowLibrary.PlayersNS.PersonNS
{
    public partial class PersonBiz
    {


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            Person person = parm.Entity as Person;
            person.IsNullThrowException("Unable to unbox person");

            //we need to add this because the DefaultBillAddressId is returning as a blank
            //and then thesystme thinks that there is a record with a blank Id
            //and the system does not save.
            if (person.DefaultBillAddressId.IsNullOrWhiteSpace())
                person.DefaultBillAddressId = null;

            if (person.PersonCategoryId.IsNullOrWhiteSpace())
                person.PersonCategoryId = null;

            if (person.CountryId.IsNullOrWhiteSpace())
                person.CountryId = null;

            if (person.DefaultEmailAddressId.IsNullOrWhiteSpace())
                person.DefaultEmailAddressId = null;

            if (person.DefaultPhoneId.IsNullOrWhiteSpace())
                person.DefaultPhoneId = null;

            if (person.Users.IsNullOrEmpty())
            {
                person.Name = UserName;

                person.Users = new List<ApplicationUser>();
                ApplicationUser appUser = UserBiz.Find(UserId);
                appUser.IsNullThrowException("User");

                //if (!appUser.PersonId.IsNullOrWhiteSpace())
                //    throw new Exception("User has a person, but person is not recognizing the user! Programming Error.");

                appUser.PersonId = person.Id;
                person.Users.Add(appUser);
                UserBiz.Update(appUser);
            }

        }


        //public void UpdateAndSaveDefaultAddress(string userId, string addressId)
        //{
        //    userId.IsNullOrWhiteSpaceThrowArgumentException();
        //    addressId.IsNullOrWhiteSpaceThrowArgumentException();

        //    ApplicationUser appUser = Find(userId);
        //    appUser.IsNullThrowException();
        //    appUser.PersonId.IsNullOrWhiteSpaceThrowArgumentException("No person for user");
        //    string personId = appUser.PersonId;

        //    PersonBiz.UpdateDefaultAddress(personId, addressId);
        //}


    }
}
