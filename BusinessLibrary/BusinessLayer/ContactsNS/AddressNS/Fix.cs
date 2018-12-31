using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            AddressMain addy = parm.Entity as AddressMain;
            AttachCountry(addy);
            addy.Name = parm.Entity.MakeUniqueName();
            addy.VerificationStatusEnum = addy.Verification.VerificaionStatusEnum;
            

        }

        private void AttachCountry(AddressMain addy)
        {
            addy.CountryId.IsNullOrWhiteSpaceThrowException("Country ID is null. Programming Error");

            if (addy.Country.IsNull())
            {
                Country country = CountryBiz.Find(addy.CountryId);
                country.IsNullThrowException("Country not found.");
                addy.Country = country;
            }
        }

        //private void attachPerson (AddressMain address)
        //{

        //    UserId.IsNullOrWhiteSpaceThrowException();
        //    if (address.PersonId.IsNullOrWhiteSpace())
        //    {
        //        Person person = UserBiz.GetPersonFor(UserId);
        //        person.IsNullThrowException("person");
        //        address.PersonId = person.Id;
        //        address.Person = person;
        //    }
        //    //if we dont do this, the userId is overwritten during Resetting Addresses for testing
        //    //if (entity.UserId.IsNullOrEmpty())
        //    //    entity.UserId = UserId;



        //}



    }
}
