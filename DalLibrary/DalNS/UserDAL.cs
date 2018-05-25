using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserModels;

namespace DalNS
{
    public class UserDAL
    {
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;
        private IErrorSet _ierrorSet;
        private IRepositry<Country> _icountryDAL;
        private IAuthenticationManager _iAuthenticationManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleStore<IdentityRole, string> _store;
        //public AccountController()
        //{
        //}

        public UserDAL(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            IAuthenticationManager iAuthenticationManager,
            IErrorSet ierrSet,
            IRepositry<Country> icountryDAL)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _ierrorSet = ierrSet;
            _icountryDAL = icountryDAL;
            _iAuthenticationManager = iAuthenticationManager;

            _store = new RoleStore<IdentityRole>();
            _roleManager = new RoleManager<IdentityRole>(_store);

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
        public string UserName { get; set; }
        public ErrorSet Errors_UserDal
        {
            get
            {
                if (_ierrorSet.IsNull())
                    throw new Exception("ErrorSet is null. UserDAL. " + MethodBase.GetCurrentMethod().Name);

                return (ErrorSet)_ierrorSet;

            }
        }
        public Repositry<Country> CountryDal
        {
            get
            {
                if (_ierrorSet.IsNull())
                {
                    Errors_UserDal.Add("CountryDAL is null.", MethodBase.GetCurrentMethod());
                    throw new Exception(Errors_UserDal.ToString());

                }

                return (Repositry<Country>)_icountryDAL;
            }
        }

        public async Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        {
            string code = await UserManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
            return code;
        }



        public virtual SelectList SelectList()
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



        #region Create User

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
                throw new NotImplementedException();
             //   PhoneNumbersUtility phoneUtility = new PhoneNumbersUtility(oldPhoneNumber, countryAbbreviation);
               // return phoneUtility.CompletePhoneNumber;

            }
            catch (Exception)
            {

                Errors_UserDal.Add(string.Format("Unable to fix the following number: '{0}' for country abbreviation :'{1}'", oldPhoneNumber, countryAbbreviation), MethodBase.GetCurrentMethod());
                throw new Exception(Errors_UserDal.ToString());
            }

        }
        public ApplicationUser Factory(RegisterViewModel r, string countryAbbrev = "")
        {
            //todo there is a duplicate of this code during registration
            #region Error CHecking

            if (r.UserName.IsNullOrWhiteSpace())
            {
                Errors_UserDal.Add("No User Name", MethodBase.GetCurrentMethod());
                throw new Exception(Errors_UserDal.ToString());
            }
            //Check the incoming phone number and fix it.
            //string phone

            //Load the country
            if (r.Phone.IsNullOrWhiteSpace())
            {
                Errors_UserDal.Add("No Phone", MethodBase.GetCurrentMethod());
                throw new Exception(Errors_UserDal.ToString());
            }


            string _countryAbbrev = "";

            if (countryAbbrev.IsNullOrWhiteSpace())
            {
                if (r.CountryID.IsNullOrEmpty())
                {
                    Errors_UserDal.Add("No Country Received", MethodBase.GetCurrentMethod());
                    throw new Exception(Errors_UserDal.ToString());
                }
                Country country = CountryDal.FindForLight(r.CountryID);
                if (country.IsNull())
                {
                    Errors_UserDal.Add("Country not found", MethodBase.GetCurrentMethod());
                    throw new Exception(Errors_UserDal.ToString());
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
            //    FName = r.Address.FName,
            //    MName = r.MName,
            //    LName = r.LName,
            //    IdentificationNo = r.CountryIdCardNumber,
            //    NameOfFatherOrHusband = r.NameOfFatherOrHusband,
            //    Sex = r.Sex,
            //    SonOfOrWifeOf = r.SonOfOrWifeOf
            //};

            //AddressComplex a = new AddressComplex
            //{
            //    Address2 = r.Address2,
            //    Attention = r.Attention,
            //    HouseNo = r.HouseNo,
            //    //Phone = r.Phone,
            //    Road = r.Road,
            //    WebAddress = r.WebAddress,
            //    Zip = r.Zip

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
                Errors_UserDal.Add("No Country Abbreviation Received", MethodBase.GetCurrentMethod());
                throw new Exception(Errors_UserDal.ToString());
            }

            return Factory(rvm, countryAbbrev);


        }


        public void Create(RegisterViewModel registerViewModel)
        {
            ApplicationUser u = Factory(registerViewModel);
            Create(u, registerViewModel.Password);
        }

        public bool Create(ApplicationUser newUser, string password)
        {

            try
            {
                IdentityResult result = UserManager.Create(newUser, password);
                return Result_Helper(newUser, result);
            }
            catch (Exception e)
            {

                Errors_UserDal.Add("User not created", MethodBase.GetCurrentMethod(), e);
                throw new Exception(Errors_UserDal.ToString());
            }


        }



        public async Task<bool> CreateAsync(ApplicationUser newUser, string password)
        {

            try
            {
                IdentityResult result = await UserManager.CreateAsync(newUser, password);
                return Result_Helper(newUser, result);
            }
            catch (Exception e)
            {

                Errors_UserDal.Add("User not created", MethodBase.GetCurrentMethod(), e);
                throw new Exception(Errors_UserDal.ToString());
            }

        }

        private bool Result_Helper(ApplicationUser newUser, IdentityResult result)
        {
            if (result == IdentityResult.Success)
            {
                Errors_UserDal.AddMessage(string.Format("Successfully created user '{0}'", newUser.UserName));
                return true;
            }
            Errors_UserDal.AddMessage(string.Format("user '{0}' not created", newUser.UserName));
            return false;
        }


        //public async Task CreateAsync(User newUser, string password)
        //{
        //    _isCreating = true;
        //    ErrorCheck(newUser);
        //    IdentityResult result = await UserManager.CreateAsync(newUser, password);
        //    CreateUserHelper(newUser, password);

        //}

        //public bool IsInRole(string id, string role)
        //{
        //    try
        //    {
        //        bool found = UserManager.IsInRoleAsync(id,role);
        //        //bool found = UserManager.IsInRole(id, role);
        //        return found;

        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }


        //    //User user = FindUserById(id);
        //    //if (user.IsNull())
        //    //{
        //    //    return false;
        //    //}

        //    //IdentityRoleGuid roleFound = _db.Roles.FirstOrDefault(x => x.Name.ToLower() == role.ToLower());

        //    //if (roleFound.IsNull())
        //    //{
        //    //    return false;
        //    //}

        //    //var roleInUser = user.Roles.FirstOrDefault(x => x.RoleId == roleFound.Id);

        //    //return !roleInUser.IsNull();
        //}

        //private static void EncryptEmailAndUserName(User newUser)
        //{
        //    newUser.UserNameUnEncrypted = newUser.UserName;
        //    newUser.EmailUnEncrypted = newUser.Email;

        //    //newUser.UserName = newUser.IsEncrypted ? newUser.UserName.Encrypt(MyConstants.USERNAME_ENCRPTION_SEED) : newUser.UserName;
        //    newUser.Email = newUser.IsEncrypted ? newUser.Email.Encrypt(newUser.MetaData.GetCreatedTicks()) : newUser.Email;
        //}

        //private static void DecryptEmailAndUserName(User newUser)
        //{
        //    newUser.UserNameUnEncrypted = newUser.IsEncrypted ? newUser.UserName.Decrypt(MyConstants.USERNAME_ENCRPTION_SEED) : newUser.UserName;
        //    newUser.EmailUnEncrypted = newUser.IsEncrypted ? newUser.Email.Decrypt(newUser.MetaData.GetCreatedTicks()) : newUser.Email; ;

        //    newUser.UserName = newUser.UserNameUnEncrypted;
        //    newUser.Email = newUser.EmailUnEncrypted;
        //}




        public bool Create(ApplicationUser entity)
        {
            string generatedPassword = System.Web.Security.Membership.GeneratePassword(8, 2);
            return Create(entity, generatedPassword);
        }

        //public async Task CreateAsync(User entity)
        //{
        //    string generatedPassword = System.Web.Security.Membership.GeneratePassword(8, 2);
        //    await CreateAsync(entity, generatedPassword);
        //}






        //private void SendSmsToUser(string userId, string phoneNumber, string password, bool isAdmin)
        //{
        //    try
        //    {
        //        if (!isAdmin)
        //        {
        //            string addedText = string.Format("Password {0}", password);
        //            SendSmsCode(userId, phoneNumber, addedText);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Errors_UserDal.Add("SMS not sent!", MethodBase.GetCurrentMethod(), e);
        //        throw new Exception(Errors_UserDal.ToString());
        //    }
        //}





        ///// <summary>
        ///// Checks if user exists.
        ///// userName expected is unencrypted.
        ///// identityCardNo is unencrypted.
        ///// If user exists, returns user.
        ///// </summary>
        ///// <param name="userName"></param>
        ///// <param name="identityCardNo"></param>
        ///// <param name="isAdmin"></param>
        //private User GetUser(string userName, string identityCardNo, bool isAdmin)
        //{

        //    //check if user exists by name...
        //    User user = FindByUserName(userName);


        //    if (user != null)
        //        return user;

        //    //check if user exists by National Id Card
        //    if (!isAdmin)
        //        user = FindForIdentityCard(identityCardNo);

        //    return user;

        //}

        //private bool IsUserExist(string userName, string identityCardNo, bool isAdmin)
        //{
        //    User user = GetUser(userName, identityCardNo, isAdmin);

        //    if (user == null)
        //        return false;
        //    else
        //        return true;
        //}

        #endregion


        public ApplicationUser FindById(string userId)
        {
            ApplicationUser user = UserManager.FindById(userId);
            return user;
        }

        public async Task<bool> GetTwoFactorBrowserRememberedAsync(string userId)
        {
            return await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId);
        }

        public ApplicationUser CreateUser(string userName, string password)
        {
            throwErrorUserIsNullOrWhiteSpace(userName);


            ApplicationUser appUser = UserManager.Find(userName, password);
            if (!appUser.IsNull())
                return appUser;

            appUser = createUserEngine(userName, password);
            return appUser;

        }

        //This is 
        private ApplicationUser createUserEngine(string userName, string password)
        {
            ApplicationUser appUser = new ApplicationUser();
            appUser.UserName = userName;
            try
            {
                IdentityResult result = UserManager.Create(appUser, password);

                if (result.Succeeded)
                {
                    return appUser;
                }
                else
                {
                    if (!result.Errors.IsNull())
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var item in result.Errors)
                            {
                                Errors_UserDal.Add(item, "Creating User");
                            }
                        }
                        else
                        {
                            Errors_UserDal.Add("Errors are 0 but no user created.", "Creating User");

                        }
                    }
                    else
                    {
                        Errors_UserDal.Add("Errors are null, but no user created", "Creating User");
                    }
                }

                throw new Exception();

            }
            catch (Exception e)
            {

                Errors_UserDal.Add("Unable to create user.", MethodBase.GetCurrentMethod(), e);
                throw new Exception(Errors_UserDal.ToString());

            }
        }

        private void throwErrorUserIsNullOrWhiteSpace(string userName)
        {
            if (userName.IsNullOrWhiteSpace())
            {
                Errors_UserDal.Add("User name is empty", "Creating User.");
                throw new Exception(Errors_UserDal.ToString());
            }
        }

        public IdentityRole CreateRole(string roleName)
        {
            try
            {

                //first check to see if role exists...
                if (RoleManager.RoleExists(roleName))
                    return RoleManager.FindByName(roleName);

                IdentityRole role = new IdentityRole(roleName);
                RoleManager.Create(role);
                return role;
            }
            catch (Exception e)
            {

                Errors_UserDal.Add("Unable to create role.", MethodBase.GetCurrentMethod(), e);
                throw new Exception(Errors_UserDal.ToString());
            }

        }

        public void AddUserToRole(string userId, string roleName)
        {
            try
            {
                UserManager.AddToRole(userId, roleName);
            }
            catch (Exception e)
            {

                Errors_UserDal.Add(string.Format("Unable to add user: '{0}' to role: '{1}'.", userId, roleName), MethodBase.GetCurrentMethod(), e);
                throw new Exception(Errors_UserDal.ToString());
            }
        }


    }
}
