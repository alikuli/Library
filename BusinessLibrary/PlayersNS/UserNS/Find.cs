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


        public ApplicationUser FindById_UserManager(string userId)
        {
            ApplicationUser user = UserManager.FindById(userId);
            return user;
        }

        public ApplicationUser FindByUserName_UserManager( string userName)
        {
            ApplicationUser user = UserManager.FindByName(userName);
            return user;
        }




    }
}
