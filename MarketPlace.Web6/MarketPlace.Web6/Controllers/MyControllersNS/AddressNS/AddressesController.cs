using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddessWithIdNS;
using ModelsClassLibrary.ModelsNS.ContactNS.AddressNS.AddressNS;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public partial class AddressesController : EntityAbstractController<AddressMain>
    {

        AddressBiz _addressBiz;

        public AddressesController(AddressBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _addressBiz = biz;
        }

        public AddressBiz AddressBiz { get { return _addressBiz; } }


        public ActionResult GetAddressForVerification(string addressId)
        {
            try
            {
                AddressVerificationRequest addy = AddressBiz.GetAddressVerificationRequest(addressId);
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
        public ActionResult DisplayAddressVerifcationForConfirmation(string addressId, MailServiceENUM mailServiceEnum, MailLocalOrForiegnENUM mailLocalOrForiegnENUM)
        {
            try
            {
                AddressVerificationRequest avr = AddressBiz.GetAddressVerificationRequestConfirmationHttp(addressId, mailServiceEnum, mailLocalOrForiegnENUM);
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
                AddressBiz.IssueAddressVerificationRequest(avr, GlobalObject);
                return RedirectToAction("Index");


            }
            catch (System.Exception e)
            {

                ErrorsGlobal.AddMessage("Something went wrong. Action not completed", MethodBase.GetCurrentMethod(), e);
                return RedirectToAction("Index");
            }

        }

        public ActionResult EnterVerificationNumber(string addressId)
        {
            try
            {
                AddressVerificationNumberVm vm = AddressBiz.CreateEnterVerificationNumberVm(addressId); ;
                return View(vm);
            }
            catch (System.Exception e)
            {
                ErrorsGlobal.Add("Something Went wrong", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Menus");
            }
        }

        [HttpPost]
        public ActionResult EnterVerificationNumber(AddressVerificationNumberVm avnVM)
        {
            try
            {
                AddressBiz.VerifiyAddressCode(avnVM, GlobalObject);
            }
            catch (System.Exception e)
            {
                ErrorsGlobal.Add("Something Went wrong", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
            }
            return RedirectToAction("Index", "Menus");
        }

        /// <summary>
        /// This displays the canceled messae
        /// </summary>
        /// <param name="avr"></param>
        /// <returns></returns>
        public ActionResult DisplayAddressVerifcationForConfirmationCancel(AddressVerificationRequest avr)
        {
            //add the costs here.
            ErrorsGlobal.AddMessage("Your request have been canceled");
            ErrorsGlobal.MemorySave();
            return RedirectToAction("Index", "Menus");
        }


        public ActionResult SetAllAddressesToNotVerified()
        {
            try
            {
                AddressBiz.SetAllAddressesToNotVerified(GlobalObject);

            }
            catch (System.Exception e)
            {

                ErrorsGlobal.AddMessage("Something went wrong. Action not completed", MethodBase.GetCurrentMethod(), e);
            }

            ErrorsGlobal.DoNotClearMessages = true;
            return RedirectToAction("Index", "Home");
        }




        public async Task<ActionResult> ResetAddressVerificationComplete()
        {
            try
            {
                await AddressBiz.ResetAddressVerificationComplete(GlobalObject);
            }
            catch (System.Exception e)
            {

                ErrorsGlobal.Add("Some problem occoured", MethodBase.GetCurrentMethod(), e);
            }
            ErrorsGlobal.DoNotClearMessages = true;
            return RedirectToAction("Index", "Home");

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
                AddressBiz.UpdateAndSaveDefaultAddress(UserId, addressId, GlobalObject);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Addresses", new
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



        [HttpPost]
        public ActionResult SaveAddressFromJavaScript(AddressJsVm address)
        {

            return Json(new
            {
                message = "Address Successfully posted!"
            });
        }

        [HttpPost]
        public ActionResult GetAddressInfo(string id)
        {
            id.IsNullOrWhiteSpaceThrowArgumentException();
            AddressMain addyMain = AddressBiz.Find(id);
            addyMain.IsNullThrowExceptionArgument();
            AddressStringWithNames addressString = addyMain.ToAddressComplex();
            return Json(addressString, JsonRequestBehavior.DenyGet);
        }

    }
}