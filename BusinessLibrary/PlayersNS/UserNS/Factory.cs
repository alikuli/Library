using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.PlayersNS;
using UserModels;
using UserModelsLibrary.ModelsNS;
using WebLibrary.Programs;

namespace UowLibrary
{
    public partial class UserBiz : BusinessLayer<ApplicationUser>
    {
        




        public ApplicationUser Factory(RegisterViewModel r, string countryAbbrev = "")
        {

            #region Error CHecking

            if (r.UserName.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("No User Name", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }
            //Check the incoming phone number and fix it.
            //string phone

            //Load the country
            if (r.Phone.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("No Phone", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }


            string _countryAbbrev = "";

            if (countryAbbrev.IsNullOrWhiteSpace())
            {
                if (r.CountryID.IsNullOrEmpty())
                {
                    ErrorsGlobal.Add("No Country Received", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }
                Country country = CountryDal.FindForLight(r.CountryID);
                if (country.IsNull())
                {
                    ErrorsGlobal.Add("Country not found", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
                }

                _countryAbbrev = country.Abbreviation;

            }
            else
            {
                _countryAbbrev = countryAbbrev;
            }
            #endregion

            //fix phone number
            string fixedPhoneNumber = PhoneNumberFixer(
                r.Phone,
                _countryAbbrev);

            //PersonComplex p = new PersonComplex
            //{
            //    FName = r.FName,
            //    MName = r.MName,
            //    LName = r.LName,
            //    IdentificationNo = r.CountryIdCardNumber,
            //    NameOfFatherOrHusband = r.NameOfFatherOrHusband,
            //    Sex = r.Sex,
            //    SonOfOrWifeOf = r.SonOfOrWifeOf
            //};

            //AddressComplex a = new AddressComplex
            //{
            //    Address2 = r.Address.Address2,
            //    Attention = r.Address.Attention,
            //    HouseNo = r.Address.HouseNo,
            //    //Phone = r.Phone,
            //    Road = r.Address.Road,
            //    WebAddress = r.Address.WebAddress,
            //    Zip = r.Address.Zip

            //    //CityName = r.CityName,
            //    //CountryName = r.CountryName,
            //    //StateName = r.StateName,
            //    //TownName = r.TownName,            
            //};

            ApplicationUser u = new ApplicationUser
            {
                UserName = r.UserName,  //This will be the Login
                PhoneNumber = fixedPhoneNumber,
                PhoneNumberAsEntered = r.Phone,
                Email = r.Email,
                PersonComplex = r.Person,
                AddressComplex = r.Address

            };

            //Misc
            //u.IsEncrypted = ConfigManagerHelper.IsEncrypted;


            return u;

        }



        private ApplicationUser Factory(string password, string userName, string phoneNumber, string countryAbbrev, string email = "")
        {

            RegisterViewModel rvm = new RegisterViewModel();
            rvm.Password = password;
            rvm.UserName = userName;
            rvm.Phone = phoneNumber;
            rvm.Email = email;

            if (countryAbbrev.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("No Country Abbreviation Received", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            return Factory(rvm, countryAbbrev);


        }



        


    }
}
