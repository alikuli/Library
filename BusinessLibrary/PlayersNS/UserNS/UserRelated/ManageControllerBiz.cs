﻿using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UowLibrary.Abstract;
using UserModels;
using UserModelsLibrary.ModelsNS;
using WebLibrary.Programs;

namespace UowLibrary
{
    public class ManageControllerBiz : AbstractBiz
    {
        public ManageControllerBiz(UserBiz userBiz, IMemoryMain memoryMain, IErrorSet errorSet)
            : base(memoryMain, errorSet)
        {
            UserBiz = userBiz;
        }

        public UserBiz UserBiz { get; private set; }



        #region Code to Run the Index Action

        //*** original code ***
        //public async Task<ActionResult> Index(ManageMessageId? message)
        //{

        //ViewBag.StatusMessage =
        //    message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
        //    : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
        //    : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
        //    : message == ManageMessageId.Error ? "An error has occurred."
        //    : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
        //    : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
        //    : "";

        //var userId = User.Identity.GetUserId();
        //var model = new IndexViewModel
        //{
        //    HasPassword = HasPassword(),
        //    PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
        //    TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
        //    Logins = await UserManager.GetLoginsAsync(userId),
        //    BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
        //};
        //}


        /// <summary>
        /// This holds the Index Status Message.
        /// </summary>
        public string IndexStatusMessage { get; private set; }


        public string MakeStatusMessage(ManageMessageId? message)
        {
            string statusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            return statusMessage;

        }



        public async Task<IndexViewModel> Index(ManageMessageId? message)
        {
            IndexStatusMessage = MakeStatusMessage(message);

            //var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserBiz.UserManager.GetPhoneNumberAsync(UserIdBiz),
                TwoFactor = await UserBiz.UserManager.GetTwoFactorEnabledAsync(UserIdBiz),
                Logins = await UserBiz.UserManager.GetLoginsAsync(UserIdBiz),
                BrowserRemembered = await UserBiz.GetTwoFactorBrowserRememberedAsync(UserIdBiz)
            };
            return model;
        }

        private bool HasPassword()
        {

            var user = UserBiz.FindById_UserManager(UserIdBiz);

            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        #endregion


        #region  Code for Remove Login


        //*** ORIGINAL CODE ***
        //public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        //{
        //    ManageMessageId? message;
        //    var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        message = ManageMessageId.RemoveLoginSuccess;
        //    }
        //    else
        //    {
        //        message = ManageMessageId.Error;
        //    }
        //    return RedirectToAction("ManageLogins", new { Message = message });
        //}



        public async Task<ManageMessageId?> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserBiz.UserManager.RemoveLoginAsync(UserIdBiz, new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserBiz.UserManager.FindByIdAsync(UserIdBiz);
                if (user != null)
                {
                    await UserBiz.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return message;
        }

        #endregion




        #region Add Phone Number
        public async Task<bool> AddPhoneNumberAsync(AddPhoneNumberViewModel model)
        {
            try
            {
                ApplicationUser user = await UserBiz.UserManager.FindByIdAsync(UserIdBiz);
                if (user.IsNull())
                {
                    ErrorsGlobal.Add("No User found.", MethodBase.GetCurrentMethod());
                    return await Task.Run(() => false);

                }
                string fixedPhoneNumber = UserBiz.PhoneNumberFixer(model.Number, user.CountryAbbreviation);
                model.FixedPhoneNumber = fixedPhoneNumber;
                // Generate the token and send it
                var code = await UserBiz.UserManager.GenerateChangePhoneNumberTokenAsync(UserIdBiz, model.FixedPhoneNumber);
                if (UserBiz.UserManager.SmsService != null)
                {
                    var message = new IdentityMessage
                    {
                        Destination = model.Number,
                        Body = "Your security code is: " + code
                    };
                    await UserBiz.UserManager.SmsService.SendAsync(message);
                }
                return await Task.Run(() => true);
            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong while adding phone number async.", MethodBase.GetCurrentMethod(), e);

            }

            return await Task.Run(() => false);
        }

        #endregion


        #region Enable/Diable 2 Factor Login


        // *** ORIGINAL CODE ****
        //// POST: /Manage/EnableTwoFactorAuthentication
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> EnableTwoFactorAuthentication()
        //{
        //    await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), true);
        //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //    }
        //    return RedirectToAction("Index", "Manage");
        //}

        ////
        //// POST: /Manage/DisableTwoFactorAuthentication
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DisableTwoFactorAuthentication()
        //{
        //    await UserManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId(), false);
        //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //    }
        //    return RedirectToAction("Index", "Manage");
        //}


        public async Task EnableTwoFactorAuthenticationAsync()
        {
            await UserBiz.UserManager.SetTwoFactorEnabledAsync(UserIdBiz, true);
            var user = await UserBiz.UserManager.FindByIdAsync(UserIdBiz);
            if (user != null)
            {
                await UserBiz.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
        }



        public async Task DisableTwoFactorAuthenticationAsync()
        {
            await UserBiz.UserManager.SetTwoFactorEnabledAsync(UserIdBiz, false);
            var user = await UserBiz.UserManager.FindByIdAsync(UserIdBiz);
            if (user != null)
            {
                await UserBiz.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
        }


        #endregion        //

        #region Verify Phone Number

        //*** ORIGINAL CODE ***
        //// GET: /Manage/VerifyPhoneNumber
        //public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        //{
        //    var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);
        //    // Send an SMS through the SMS provider to verify the phone number
        //    return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        //}

        ////
        //// POST: /Manage/VerifyPhoneNumber
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, model.Code);
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
        //    }
        //    // If we got this far, something failed, redisplay form
        //    ModelState.AddModelError("", "Failed to verify phone");
        //    return View(model);
        //}



        // GET: /Manage/VerifyPhoneNumber


        public async Task<string> VerifyPhoneNumberAsync(string phoneNumber)
        {
            var code = await UserBiz.UserManager.GenerateChangePhoneNumberTokenAsync(UserIdBiz, phoneNumber);
            return code;
        }


        public async Task<bool> VerifyPhoneNumberAsync(VerifyPhoneNumberViewModel model)
        {
            var result = await UserBiz.UserManager.ChangePhoneNumberAsync(UserIdBiz, model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserBiz.UserManager.FindByIdAsync(UserIdBiz);
                if (user != null)
                {
                    await UserBiz.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return true;
            }
            // If we got this far, something failed, redisplay form
            ErrorsGlobal.Add("Failed to verify phone", MethodBase.GetCurrentMethod());
            return false;
        }



        #endregion


        #region Remove Phone Number

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ManageMessageId> RemovePhoneNumber()
        {
            var result = await UserBiz.UserManager.SetPhoneNumberAsync(UserIdBiz, null);
            if (!result.Succeeded)
            {
                return ManageMessageId.Error;
            }
            var user = await UserBiz.UserManager.FindByIdAsync(UserIdBiz);
            if (user != null)
            {
                await UserBiz.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return ManageMessageId.RemovePhoneSuccess;
        }


        #endregion


        #region Change Password

        //
        // POST: /Manage/ChangePassword
        //public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //        if (user != null)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //        }
        //        return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
        //    }
        //    AddErrors(result);
        //    return View(model);
        //}


        /// <summary>
        /// Returns true if success, and signs in if user found, otherwise loads errors from identityResult into GlobalErrors and returns false.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(ChangePasswordViewModel model)
        {
            var result = await UserBiz.UserManager.ChangePasswordAsync(UserIdBiz, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserBiz.UserManager.FindByIdAsync(UserIdBiz);
                if (user != null)
                {
                    await UserBiz.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return true;
                //return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrorsFrom(result);
            //return View(model);
            return false;
        }


        #endregion




        #region Set Password


        ////
        //// POST: /Manage/SetPassword
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
        //        if (result.Succeeded)
        //        {
        //            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //            if (user != null)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //            }
        //            return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}


        //
        // POST: /Manage/SetPassword
        public async Task<bool> SetPasswordAsync(SetPasswordViewModel model)
        {
            var result = await UserBiz.UserManager.AddPasswordAsync(UserIdBiz, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserBiz.UserManager.FindByIdAsync(UserIdBiz);
                if (user != null)
                {
                    await UserBiz.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return true;
            }
            AddErrorsFrom(result);

            // If we got this far, something failed, redisplay form
            return false;
        }

        #endregion        //


        #region Manage Logins
        //// GET: /Manage/ManageLogins
        //public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        //{
        //    ViewBag.StatusMessage =
        //        message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
        //        : message == ManageMessageId.Error ? "An error has occurred."
        //        : "";
        //    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
        //    if (user == null)
        //    {
        //        return View("Error");
        //    }
        //    var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
        //    var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
        //    ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
        //    return View(new ManageLoginsViewModel
        //    {
        //        CurrentLogins = userLogins,
        //        OtherLogins = otherLogins
        //    });
        //}

        /// <summary>
        /// This gets its message in ManageLoginsAsync
        /// </summary>
        public string ManageLoginStatusMessage { get; set; }

        /// <summary>
        /// This gets its message in ManageLoginsAsync
        /// </summary>
        public bool ShowRemoveButton { get; set; }

        // GET: /Manage/ManageLogins


        public async Task<ManageLoginsViewModel> ManageLoginsAsync(ManageMessageId? message)
        {
            ManageLoginStatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = await UserBiz.UserManager.FindByIdAsync(UserIdBiz);

            if (user == null)
            {
                return null;
            }

            var userLogins = await UserBiz.UserManager.GetLoginsAsync(UserIdBiz);
            var otherLogins = UserBiz.GetExternalAuthenticationTypes
                .Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider))
                .ToList();

            ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return new ManageLoginsViewModel
                            {
                                CurrentLogins = userLogins,
                                OtherLogins = otherLogins
                            };
        }


        #endregion



        #region Link Login
        //public async Task<ActionResult> LinkLoginCallback()
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        //    }
        //    var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
        //    return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        //}

        private const string XsrfKey = "XsrfId";

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<IdentityResult> LinkLoginCallbackAsync()
        {
            var loginInfo = await UserBiz.AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, UserIdBiz);

            if (loginInfo == null)
            {
                return null;
            }
            var result = await UserBiz.UserManager.AddLoginAsync(UserIdBiz, loginInfo.Login);
            return result;
        }


        #endregion

    }
}



