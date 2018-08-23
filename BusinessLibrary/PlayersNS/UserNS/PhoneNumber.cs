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

        /// <summary>
        /// Returns complete phone number
        /// </summary>
        /// <param name="oldPhoneNumber"></param>
        /// <param name="countryAbbreviation"></param>
        /// <returns></returns>
        public string PhoneNumberFixer(string oldPhoneNumber, string countryAbbreviation)
        {

            try
            {
                PhoneNumbersUtility phoneUtility = new PhoneNumbersUtility(oldPhoneNumber, countryAbbreviation);
                return phoneUtility.CompletePhoneNumber;

            }
            catch (Exception)
            {

                ErrorsGlobal.Add(string.Format("Unable to fix the following number: '{0}' for country abbreviation :'{1}'", oldPhoneNumber, countryAbbreviation), MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

        }



    }
}
