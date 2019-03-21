using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlacesNS.EmailAddressNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.EmailAddressNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class EmailAddressesController : EntityAbstractController<EmailAddress>
    {
        EmailAddressBiz _emailAddressBiz;
        public EmailAddressesController(EmailAddressBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _emailAddressBiz = biz;
        }

        //public override System.Web.Mvc.ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        //{
        //    EmailAddress email = parm.Entity as EmailAddress;
        //    email.IsNullThrowException();
        //    //email.EmailAddy = email.Name;

        //    return base.Event_CreateViewAndSetupSelectList(parm);
        //}


        EmailAddressBiz EmailAddressBiz
        {
            get
            {
                return _emailAddressBiz;
            }
        }
        public ActionResult GetAddressForVerification(string addressId)
        {
            try
            {
                EmailAddressBiz.SendEmailConfirmation(addressId);
            }
            catch (System.Exception e)
            {
                ErrorsGlobal.Add("Something went wrong. Verification not sent.", MethodBase.GetCurrentMethod(), e);
            }
            ErrorsGlobal.MemorySave();
            return RedirectToAction("Index");
        }

        public ActionResult EnterVerificationNumber(string addressId)
        {
            try
            {
                addressId.IsNullOrWhiteSpaceThrowArgumentException("addressId");
                EmailAddress emailAddy = EmailAddressBiz.Find(addressId);
                emailAddy.IsNullThrowException("Email addresss not found");

                IdNameModel model = new IdNameModel();
                model.Id = emailAddy.Id;
                model.Name = emailAddy.Name;

                return View(model);
            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong. Verification not sent.", MethodBase.GetCurrentMethod(), e);

            }
            ErrorsGlobal.MemorySave();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EnterVerificationNumber(string id, string verificationCode)
        {
            try
            {
                verificationCode.IsNullOrWhiteSpaceThrowArgumentException("Verification code not received");
                id.IsNullOrWhiteSpaceThrowArgumentException("EmailAddressBiz not received");

                EmailAddressBiz.CheckEmailVerificationCode(id, verificationCode);
            }
            catch (System.Exception e)
            {
                ErrorsGlobal.Add("Something went wrong. Your account is not verified. Try Again", MethodBase.GetCurrentMethod(), e);
            }
            ErrorsGlobal.MemorySave();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult UpdateAndSaveDefaultAddress(
            string addressId,
            string searchFor,
            string isandForSearch,
            string selectedId,
            string returnUrl,
            MenuENUM menuEnum = MenuENUM.IndexDefault,
            SortOrderENUM sortBy = SortOrderENUM.Item1_Asc,
            bool print = false,
            bool isMenu = false,
            string menuPathMainId = "")
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            addressId.IsNullOrWhiteSpaceThrowArgumentException();
            try
            {
                EmailAddressBiz.UpdateAndSaveDefaultAddress(addressId);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "EmailAddresses", new
                {
                    id = addressId,
                    searchFor = searchFor,
                    isandForSearch = isandForSearch,
                    selectedId = selectedId,
                    returnUrl = returnUrl,
                    menuEnum = menuEnum,
                    sortBy = sortBy
                });

            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}