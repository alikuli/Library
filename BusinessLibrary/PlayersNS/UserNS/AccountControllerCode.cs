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
        

        #region AccountControllerBiz

        //-------------------------------------- AccountControllerBiz Begin

        #region Register

        public RegisterViewModel CreateRegisterViewModel()
        {
            RegisterViewModel r = new RegisterViewModel();
            r.CountrySelectList = CountryDal.SelectList();
            return r;
        }


        public RegisterViewModel LoadCountrySelectListIn(RegisterViewModel rvm)
        {
            rvm.CountrySelectList = CountryDal.SelectList();
            return rvm;
        }

        /// <summary>
        /// This registers the user.
        /// The UserName is the fixed User PhoneNumber.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task RegisterAsync(RegisterViewModel model)
        {

            Country country = CountryDal.FindFor(model.CountryID);

            if (country.IsNull())
            {
                ErrorsGlobal.Add("No Country found!", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }



            string fixedPhoneNumber = fixPhoneNumber(model, country);

            //use the supplied userName or the fixed phone number if nothing supplied.
            //at the moment we are using the complete phone number as the user name.
            //if we use the phone number as the user name and the user changes his phone
            //number then we will have a problem. It is better not to link the user name
            //with the phone number.

            string userName = model.UserName;
            if (model.UserName.IsNullOrWhiteSpace())
            {
                userName = fixedPhoneNumber;
            }


            var user = new ApplicationUser
            {
                UserName = userName,
                PhoneNumber = fixedPhoneNumber,
                PhoneNumberAsEntered = model.Phone
            };


            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                string code = await GenerateChangePhoneNumberTokenAsync(user.Id, fixedPhoneNumber);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            }

        }

        private string fixPhoneNumber(RegisterViewModel model, Country country)
        {
            //fix phone number
            string fixedPhoneNumber = "";
            try
            {
                fixedPhoneNumber = PhoneNumberFixer(model.Phone, country.Abbreviation);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Unable to fix phone number!", MethodBase.GetCurrentMethod(), e);
                throw new Exception(ErrorsGlobal.ToString());
            }

            if (fixedPhoneNumber.IsNull())
            {
                ErrorsGlobal.Add("Phone number is null!", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }
            return fixedPhoneNumber;
        }


        #endregion


        #region Login
        /// <summary>
        /// I want to use username as login because the phone number can have many different inputs
        /// and at the time of login, I do not know the country.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<SignInStatus> LoginAsync(LoginViewModel model)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true

            //Fix the phone number...
            //string fixedPhoneNumber
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            return result;

        }


        public async Task<SendCodeViewModel> SendCodeAsync(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                ErrorsGlobal.Add("No user found.", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe };

        }

        #endregion



        #region Verify Code

        public async Task<VerifyCodeViewModel> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                ErrorsGlobal.AddMessage("Code Not Verified.");
                return null;
            }
            return new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe };

        }

        public async Task<SignInStatus> VerifyCodeAsync(VerifyCodeViewModel model)
        {
            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            return result;
        }


        #endregion


        #region Confirm Email

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return result;
        }

        public string Code { get; set; }
        public string CallBackUrl { get; set; }
        public ApplicationUser AppUser { get; set; }

        /// <summary>
        /// This method finds the user and sets AppUser to that User.
        /// Then it makes sure the email is confirmed.
        /// Then it generates a new code to reset the password.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> IsUserConfirmedThenGenerateCode(ForgotPasswordViewModel model)
        {
            var AppUser = await UserManager.FindByNameAsync(model.Phone);
            if (AppUser == null || !(await UserManager.IsEmailConfirmedAsync(AppUser.Id)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return false;
            }
            Code = await UserManager.GeneratePasswordResetTokenAsync(AppUser.Id);
            return true;

        }


        public async Task<bool> SendendEmailToConfirmedUserAndUrlAsync()
        {
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link

            await UserManager.SendEmailAsync(AppUser.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + CallBackUrl + "\">here</a>");
            return true;

        }


        #endregion

        #region Reset Password
        public async Task<bool> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return true;
            }

            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return true;
            }

            AddErrorsFrom(result);
            return false;
        }


        #endregion

        /// <summary>
        /// Sends a two factor code to the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> SendTwoFactorCodeAsync(SendCodeViewModel model)
        {
            return await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider);
        }

        public void LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

        }

        //-------------------------------------- AccountControllerBiz End
        #endregion

        


    }
}
