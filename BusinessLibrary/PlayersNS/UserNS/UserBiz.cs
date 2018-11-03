using AliKuli.Extentions;
using DalLibrary.Interfaces;
using MarketPlace.Web6.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;
using UserModels;

namespace UowLibrary
{
    public partial class UserBiz : BusinessLayer<ApplicationUser>
    {
        //private CountryBiz _countryBiz;
        AddressBiz _addressBiz;
        private IAuthenticationManager _iAuthenticationManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleStore<IdentityRole, string> _store;

        public UserBiz(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IAuthenticationManager iAuthenticationManager,
            AddressBiz addressBiz,
            IRepositry<ApplicationUser> entityDal,
            BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {
            //_countryBiz = countryBiz;
            _addressBiz = addressBiz;
            UserManager = userManager;
            SignInManager = signInManager;
            _iAuthenticationManager = iAuthenticationManager;

            _store = new RoleStore<IdentityRole>();
            _roleManager = new RoleManager<IdentityRole>(_store);

        }


        public AddressBiz AddressBiz { get { return _addressBiz; } }


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
        public CountryBiz CountryBiz
        {
            get
            {

                _addressBiz.CountryBiz.IsNullThrowException();
                return _addressBiz.CountryBiz;
            }
        }

        public async Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        {
            string code = await UserManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
            return code;
        }


        //public  SelectList SelectListExcept(string userId)
        //{
        //    if (this.IsNull())
        //        return null;

        //    var allUsers = UserManager.Users.Where(x => x.Id != userId).ToList();

        //    if (allUsers.IsNull() || allUsers.Count() == 0)
        //        return null;

        //    var sortedList = allUsers.OrderBy(x => x.UserName)
        //        .Select(x =>
        //        new
        //        {
        //            Text = x.UserName,
        //            Value = x.Id
        //        })
        //        .ToList();
        //    var selectList = new SelectList(sortedList, "Value", "Text");
        //    return selectList;
        //}

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
