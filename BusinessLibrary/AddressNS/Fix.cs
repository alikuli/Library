using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;


namespace UowLibrary.AddressNS
{
    public partial class AddressBiz
    {


        public override void Fix(ControllerCreateEditParameter parm)
        {
            base.Fix(parm);
            AddressWithId addy = parm.Entity as AddressWithId;
            //these are only for create
            AttachUser(addy);
            AttachCountry(addy);
            parm.Entity.Name = parm.Entity.MakeUniqueName();
        }

        private void AttachCountry(AddressWithId addy)
        {
            if (!addy.CountryId.IsNullOrEmpty())
            {
                if (addy.Country.IsNull())
                {
                    Country country = CountryBiz.Find(addy.CountryId);
                    country.IsNullThrowException("Country not found.");
                    addy.Country = country;
                }
            }
        }

        private void AttachUser(AddressWithId entity)
        {

            UserId.IsNullOrWhiteSpaceThrowException();
            entity.User = UserBiz.Find(UserId);

            if (entity.User.IsNull())
            {
                ErrorsGlobal.Add("User not found!", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }


            //UserId.IsNullOrWhiteSpaceThrowException();
            //ApplicationUser user = UserBiz.Find(UserId);
            //user.Addresses.Add(entity);

            //entity.User = user;
            //entity.UserId = UserId;
            //entity.User.Addresses.Add(entity);
            //;
            //if (entity.User.IsNull())
            //{
            //    ErrorsGlobal.Add("User not found!", MethodBase.GetCurrentMethod());
            //    throw new Exception(ErrorsGlobal.ToString());
            //}
        }

    }
}
