using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web4.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UserModelsLibrary.ModelsNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class ManageController : AbstractController
    {
        readonly ManageControllerBiz _manageControllerUOW;
        public ManageController(ManageControllerBiz biz, AbstractControllerParameters param)
            : base(param)
        {
            _manageControllerUOW = biz;
        }

        public ManageControllerBiz ManageControllerBiz
        {
            get { return _manageControllerUOW; }
        }

        public ManageControllerBiz ManageControllerUow { get { return _manageControllerUOW; } }

        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        //public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}



        #region Add Phone Number

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            try
            {
                var model = await ManageControllerUow.Index(message);
                ViewBag.StatusMessage = ManageControllerUow.IndexStatusMessage;
                return View(model);
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
            }
            return View();
        }

        #endregion

        #region Remove Login

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            try
            {
                var message = await ManageControllerUow.RemoveLogin(loginProvider, providerKey);
                return RedirectToAction("ManageLogins", new { Message = message });

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong while removing login.", MethodBase.GetCurrentMethod(), e);
            }
            return View();
        }


        #endregion
        #region Add Phone number

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Generate the token and send it
            try
            {
                
                //await ManageControllerUow.AddPhoneNumberAsync(model);
                //https://stackoverflow.com/questions/28659322/asynchronous-task-in-asp-net-mvc-5
                return await Task.Run( () => RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.FixedPhoneNumber }));

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong while adding phone number.", MethodBase.GetCurrentMethod(), e);
            }
            return View();

        }


        #endregion        //


        #region Enable/Diable 2 Factor Login

        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {

            await ManageControllerUow.EnableTwoFactorAuthenticationAsync();
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {

            await ManageControllerUow.DisableTwoFactorAuthenticationAsync();
            return RedirectToAction("Index", "Manage");
        }


        #endregion        //


        #region Verify Phone Number

        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await ManageControllerUow.VerifyPhoneNumberAsync(phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber, Code = code });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (await ManageControllerUow.VerifyPhoneNumberAsync(model))
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            else
                return View(model);
        }

        #endregion

        #region Remove Phone Number

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            return RedirectToAction("Index", new { Message = await ManageControllerUow.RemovePhoneNumber() });
        }


        #endregion


        #region Change Password
        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var success = await ManageControllerUow.ChangePassword(model);
            if (success)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            return View(model);
        }

        #endregion


        #region Set Password
        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var success = await ManageControllerUow.SetPasswordAsync(model);
                if (success)
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
            }
            return View(model);
        }


        #endregion        //


        #region Manage Logins
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ManageLoginsViewModel manageLoginsViewModel = await ManageControllerUow.ManageLoginsAsync(message, UserId);
            ViewBag.StatusMessage = ManageControllerUow.ManageLoginStatusMessage;

            if (manageLoginsViewModel == null)
            {
                return View("Error");
            }
            ViewBag.ShowRemoveButton = ManageControllerUow.ShowRemoveButton;
            return View(manageLoginsViewModel);
        }

        #endregion

        #region Link Login

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            IdentityResult result = await ManageControllerUow.LinkLoginCallbackAsync();
            if (result.IsNull())
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });

            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }


        #endregion
        protected override void Dispose(bool disposing)
        {
            //if (disposing && _userManager != null)
            //{
            //    _userManager.Dispose();
            //    _userManager = null;
            //}

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}


        //private bool HasPassword()
        //{
        //    var user = UserManager.FindById(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        return user.PasswordHash != null;
        //    }
        //    return false;
        //}

        //private bool HasPhoneNumber()
        //{
        //    var user = UserManager.FindById(User.Identity.GetUserId());
        //    if (user != null)
        //    {
        //        return user.PhoneNumber != null;
        //    }
        //    return false;
        //}

        //public enum ManageMessageId
        //{
        //    AddPhoneSuccess,
        //    ChangePasswordSuccess,
        //    SetTwoFactorSuccess,
        //    SetPasswordSuccess,
        //    RemoveLoginSuccess,
        //    RemovePhoneSuccess,
        //    Error
        //}

        #endregion
    }
}