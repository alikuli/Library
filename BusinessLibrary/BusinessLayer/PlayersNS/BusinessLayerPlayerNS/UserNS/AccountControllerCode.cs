using AliKuli.Extentions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsNS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserModels;

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
            //r.CountrySelectList = CountryBiz.SelectList();
            return r;
        }


        //public RegisterViewModel LoadCountrySelectListIn(RegisterViewModel rvm)
        //{
        //    rvm.CountrySelectList = CountryBiz.SelectList();
        //    return rvm;
        //}

        /// <summary>
        /// This registers the user.
        /// this also creates a customer, owner person for the user. If there is a problem in creation of anyone of these...
        /// the user is also not created.
        /// All the work is done is SuperCashBiz
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> RegisterAsync(RegisterViewModel model, string personId)
        {


            string userName = model.UserName;

            //see if a user exists under the same name
            bool userFound = (await UserManager.FindByNameAsync(userName)) != null;

            if (userFound)
            {
                throw new Exception(string.Format("User with '{0}' name already exists! Try another name.", userName));
            }

            var user = new ApplicationUser
            {
                UserName = userName,
                //PersonId = personId,
            };



            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                await SignInUser(user,false);


                UserName = user.UserName;
                UserId = user.Id;

                return user;
                //string code = await GenerateChangePhoneNumberTokenAsync(user.Id, fixedPhoneNumber);

                // For more information on how to enable account confirmation and pas sword reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
            }
            else
            {
                //User did not get  created for some reason
                if (result.Errors.IsNull())
                {
                    throw new Exception("Unable to create a login id. Try again");
                }
                else
                {
                    foreach (string item in result.Errors)
                    {
                        ErrorsGlobal.Add(item, "Login");
                    }
                    throw new Exception();

                }

            }

        }

        public async Task SignInUser(ApplicationUser user, bool rememberMe)
        {
            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: rememberMe);
        }

        //private string fixPhoneNumber(RegisterViewModel model, Country country)
        //{
        //    //fix phone number
        //    string fixedPhoneNumber = "";
        //    try
        //    {
        //        fixedPhoneNumber = PhoneNumberFixer(model.Phone, country.Abbreviation);

        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add("Unable to fix phone number!", MethodBase.GetCurrentMethod(), e);
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }

        //    if (fixedPhoneNumber.IsNull())
        //    {
        //        ErrorsGlobal.Add("Phone number is null!", MethodBase.GetCurrentMethod());
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }
        //    return fixedPhoneNumber;
        //}


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
        public async Task<bool> IsEmailConfirmed(ForgotPasswordViewModel model)
        {
            //var AppUser = await UserManager.FindByNameAsync(model.Phone);
            if (model.Email.IsNullOrWhiteSpace())
                return false;


            ApplicationUser appUser = await UserManager.FindByEmailAsync(model.Email);
            if (appUser == null || !(await UserManager.IsEmailConfirmedAsync(appUser.Id)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return false;
            }
            AppUser = appUser;
            Code = await UserManager.GeneratePasswordResetTokenAsync(appUser.Id);
            return true;

        }

        //Just a temp message collector set up to get data for errors
        public List<string> MessageCollector { get; set; }

        /// <summary>
        /// This fixes the phone number and then checks against the phone number if he user is found.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> IsPhoneConfirmed(ForgotPasswordViewModel model)
        {
            //var AppUser = await UserManager.FindByNameAsync(model.Phone);
            if (model.Phone.IsNullOrWhiteSpace())
                return false;

            //if (MessageCollector.IsNull())
            //    MessageCollector = new List<string>();

            //MessageCollector.Add("Entered IsPhoneConfirmed");
            //first fix the phone number
            Country country = CountryBiz.FindAll().FirstOrDefault(x => x.Id == model.CountryId);
            if (country.IsNull())
            {
                return false;
            }

            string fixedPhoneNumber = PhoneNumberFixer(model.Phone, country.Abbreviation);
            //MessageCollector.Add("fixedPhoneNumber = " + fixedPhoneNumber);

            ApplicationUser appUser = FindAll().FirstOrDefault(x => x.PhoneNumber == fixedPhoneNumber && x.PhoneNumberConfirmed == true);
            //MessageCollector.Add("AppUser.ID = " + appUser.Id);

            if (appUser == null || !(await UserManager.IsEmailConfirmedAsync(appUser.Id)))
            {
                // Don't reveal that the user does not exist or is not confirmed
                return false;
            }

            AppUser = appUser;
            Code = await UserManager.GeneratePasswordResetTokenAsync(appUser.Id);
            //MessageCollector.Add("Code = " + Code);

            return true;

        }

        public async Task<bool> IsUserNameFound(ForgotPasswordViewModel model)
        {
            //var AppUser = await UserManager.FindByNameAsync(model.Phone);
            if (model.UserName.IsNullOrWhiteSpace())
                return false;

            //if (MessageCollector.IsNull())
            //    MessageCollector = new List<string>();
            //MessageCollector.Add("Entered IsUserNameFound");



            ApplicationUser appUser = UserManager.FindByName(model.UserName);
            //if (appUser.IsNull())
            //{
            //    MessageCollector.Add("AppUser is null");

            //}
            //else
            //{
            //    MessageCollector.Add("AppUser is found");

            //}
            bool bothEmailAndPhoneNotConfirmed = !(await UserManager.IsEmailConfirmedAsync(appUser.Id)) && !(await UserManager.IsPhoneNumberConfirmedAsync(appUser.Id));
            if (appUser == null || bothEmailAndPhoneNotConfirmed)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return false;
            }
            AppUser = appUser;
            Code = await UserManager.GeneratePasswordResetTokenAsync(appUser.Id);
            //MessageCollector.Add("Code is generated: " + Code);

            return true;

        }

        public async Task<bool> SendendEmailToConfirmedUserAndUrlAsync()
        {
            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
            // Send an email with this link
            string userId = AppUser.Id;
            string subject = "Reset Password";
            string body = "Please reset your password by clicking <a href=\"" + CallBackUrl + "\">here</a>";
            await UserManager.SendEmailAsync(userId, subject, body);
            return true;

        }

        #endregion

        #region Reset Password
        public async Task<bool> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
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
