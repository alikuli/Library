using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary
{
    public partial class UserBiz : BusinessLayer<ApplicationUser>
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;
        private IErrorSet _ierrorSet;
        private IRepositry<Country> _icountryDAL;
        //private IRepositry<ApplicationUser> _iuserDAL;

        private IAuthenticationManager _iAuthenticationManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleStore<IdentityRole, string> _store;
        //private RightBiz _rightBiz;
        //public AccountController()
        //{
        //}

        public UserBiz(
            ApplicationDbContext db,
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IAuthenticationManager iAuthenticationManager,
            IErrorSet ierrSet,
            IRepositry<Country> icountryDAL,
            IRepositry<ApplicationUser> userDal,
            IMemoryMain memoryMain,
            IErrorSet errorSet,
            ConfigManagerHelper configManager, 
            UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, userDal, db, configManager, uploadedFileBiz)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _ierrorSet = ierrSet;
            _icountryDAL = icountryDAL;
            //_iuserDAL = iuserDAL;
            _iAuthenticationManager = iAuthenticationManager;

            _store = new RoleStore<IdentityRole>();
            _roleManager = new RoleManager<IdentityRole>(_store);
            //_rightBiz = rightBiz;

        }

        public ApplicationSignInManager SignInManager { get; set; }
        public ApplicationUserManager UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get { return _roleManager; } }
        public IAuthenticationManager AuthenticationManager { get { return _iAuthenticationManager; } }

        public IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes
        {
            get
            {
                return AuthenticationManager.GetExternalAuthenticationTypes();
            }
        }
        //public string UserName { get; set; }
        //public string UserId { get; set; }

        //public ErrorSet ErrorsGlobal
        //{
        //    get
        //    {
        //        if (_ierrorSet.IsNull())
        //            throw new Exception("ErrorSet is null. UserDAL. " + MethodBase.GetCurrentMethod().Name);

        //        return (ErrorSet)_ierrorSet;

        //    }
        //}
        private Repositry<Country> CountryDal
        {
            get
            {
                if (_icountryDAL.IsNull())
                {
                    ErrorsGlobal.Add("Country DAL is null.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());

                }

                return (Repositry<Country>)_icountryDAL;
            }
        }

        //private Repositry<ApplicationUser> UserDal
        //{
        //    get
        //    {
        //        if (_iuserDAL.IsNull())
        //        {
        //            ErrorsGlobal.Add("User DAL is null.", MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());

        //        }

        //        return (Repositry<ApplicationUser>)_icountryDAL;
        //    }
        //}
        public async Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        {
            string code = await UserManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
            return code;
        }



        public override SelectList SelectList()
        {
            if (this.IsNull())
                return null;

            var allUsers = UserManager.Users.ToList();

            if (allUsers.IsNull() || allUsers.Count() == 0)
                return null;

            var sortedList = allUsers.OrderBy(x => x.UserName)
                .Select(x =>
                new
                {
                    Text = x.UserName,
                    Value = x.Id
                })
                .ToList();
            var selectList = new SelectList(sortedList, "Value", "Text");
            return selectList;
        }





        public async Task<bool> GetTwoFactorBrowserRememberedAsync(string userId)
        {
            return await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId);
        }


        private void throwErrorUserIsNullOrWhiteSpace(string userName)
        {
            if (userName.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.Add("User name is empty", "Creating User.");
                throw new Exception(ErrorsGlobal.ToString());
            }
        }



        public override string SelectListCacheKey
        {
            get { return "UserlistCacheKey"; }
        }





    }
}
