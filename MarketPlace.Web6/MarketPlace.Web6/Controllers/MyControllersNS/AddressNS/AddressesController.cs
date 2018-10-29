using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.AddressNS;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class AddressesController : EntityAbstractController<AddressWithId>
    {

        AddressBiz _addressBiz;

        public AddressesController(AddressBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _addressBiz = biz;
        }

        public AddressBiz AddressBiz
        {
            get
            {
                return _addressBiz;
            }
        }


        public ActionResult GetAddressForVerification(string id)
        {
            try
            {
                AddressVerificationRequest addy = AddressBiz.GetAddressVerificationRequest(id);
                return View(addy);

            }
            catch (System.Exception e)
            {

                ErrorsGlobal.AddMessage("Something went wrong. Action not completed", MethodBase.GetCurrentMethod(), e);
                return RedirectToAction("Index");
            }
        }


        /// <summary>
        /// This asks the user to confirm his request
        /// </summary>
        /// <param name="avr"></param>
        /// <returns></returns>
        public ActionResult DisplayAddressVerifcationForConfirmation(AddressVerificationRequest avr)
        {
            try
            {
                avr = AddressBiz.GetAddressVerificationRequestConfirmation(avr);
                return View(avr);

            }
            catch (System.Exception e)
            {

                ErrorsGlobal.AddMessage("Something went wrong. Action not completed", MethodBase.GetCurrentMethod(), e);
                return RedirectToAction("Index");
            }

        }


        /// <summary>
        /// This issues the address verification request
        /// </summary>
        /// <param name="avr"></param>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DisplayAddressVerifcationForConfirmation(AddressVerificationRequest avr, FormCollection fc)
        {
            try
            {
                AddressBiz.IssueAddressVerificationRequest(avr);
                return RedirectToAction("Index");


            }
            catch (System.Exception e)
            {

                ErrorsGlobal.AddMessage("Something went wrong. Action not completed", MethodBase.GetCurrentMethod(), e);
                return RedirectToAction("Index");
            }

        }
        //public ActionResult DisplayAddressVerifcationForConfirmation(string theAddressId, MailServiceENUM mailServiceEnum, FormCollection fc)
        //{

        //    try
        //    {
        //        //avr = AddressBiz.GetAddressVerificationRequestConfirmation(avr);

        //    }
        //    catch (System.Exception e)
        //    {

        //        ErrorsGlobal.AddMessage("Something went wrong. Action not completed", MethodBase.GetCurrentMethod(), e);

        //    }
        //    return View();
        //}


        /// <summary>
        /// This displays the canceled messae
        /// </summary>
        /// <param name="avr"></param>
        /// <returns></returns>
        public ActionResult DisplayAddressVerifcationForConfirmationCancel(AddressVerificationRequest avr)
        {
            //add the costs here.
            ErrorsGlobal.AddMessage("Your request have been canceled");
            return RedirectToAction("Index", "Menus");
        }


        public ActionResult SetAllAddressesToNotVerified()
        {
            try
            {
                AddressBiz.SetAllAddressesToNotVerified();

            }
            catch (System.Exception e)
            {

                ErrorsGlobal.AddMessage("Something went wrong. Action not completed", MethodBase.GetCurrentMethod(), e);
            }

            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public JsonResult IssueAddressVerificationRequest(AddressVerificationRequest avr)
        //{
        //    string message = "Successfully issued Address Verificaion.";
        //    bool success = true;
        //    try
        //    {
        //        //xxx
        //        AddressBiz.IssueAddressVerificationRequest(avr);

        //    }
        //    catch (System.Exception e)
        //    {
        //        message = "Unsuccessful. " + e.Message;
        //        success = false;

        //    }

        //    return Json(
        //        new
        //        {
        //            message = message,
        //            success = success
        //        });
        //}

    }
}