using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class SaleOrdersController : EntityAbstractController<BuySellDoc>
    {

        SaleOrderBiz _saleOrderBiz;
        AddressBiz _addressBiz;

        public SaleOrdersController(SaleOrderBiz saleOrderBiz, AddressBiz addressBiz, AbstractControllerParameters param)
            : base(saleOrderBiz, param)
        {
            _saleOrderBiz = saleOrderBiz;
            _addressBiz = addressBiz;
        }

        SaleOrderBiz SaleOrderBiz
        {
            get
            {
                _saleOrderBiz.UserId = UserId;
                _saleOrderBiz.UserName = UserName;
                return _saleOrderBiz;
            }
        }
        CustomerBiz CustomerBiz
        {
            get
            {
                return SaleOrderBiz.CustomerBiz;
            }
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                return SaleOrderBiz.OwnerBiz;
            }
        }
        AddressBiz AddressBiz
        {
            get
            {
                _addressBiz.IsNullThrowException("_addressBiz");
                _addressBiz.UserId = UserId;
                _addressBiz.UserName = UserName;
                return _addressBiz;
            }
        }
        PersonBiz PersonBiz
        {
            get
            {
                return _saleOrderBiz.PersonBiz;
            }
        }

        BuySellDocBiz BuySellDocBiz
        {
            get
            {

                return _saleOrderBiz;
            }
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            BuySellDoc buySellDoc = parm.Entity as BuySellDoc;
            buySellDoc.IsNullThrowException("Unable to unbox buySellDoc");

            buySellDoc.SelectListOwner = OwnerBiz.SelectListOnlyWith(UserId);
            buySellDoc.SelectListCustomer = CustomerBiz.SelectListWithout(UserId);
            buySellDoc.SelectListAddressInformTo = AddressBiz.SelectList();
            buySellDoc.SelectListAddressShipTo = AddressBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }

        public ActionResult Buy(string productChildId)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        public ActionResult BuyAjax(string productChildId)
        {
            string message = "Success!";
            try
            {
                //save the item , 
                string poNumber = "";
                DateTime poDate = DateTime.MinValue;
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
                productChildId.IsNullOrWhiteSpaceThrowArgumentException("Product not recieved.");
                message = BuySellDocBiz.AddToSale(UserId, productChildId, poNumber, poDate);
                return Json(new
                {
                    success = true,
                    message = message,
                },
                JsonRequestBehavior.DenyGet);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
                message = string.Format( "Not saved. Error: {0}", ErrorsGlobal.ToString());
                return Json(new
                {
                    success = false,
                    message = message,
                },
                JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult ShowSalesOrdersForCurrentUser()
        {
            SaleOrderBiz.
            return View();
        }

    }
}