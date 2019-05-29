using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.MoneyAndCountClass;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UserModels;



namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class BuySellDocsController : EntityAbstractController<BuySellDoc>
    {

        BuySellDocBiz _buySellDocsBiz;
        AddressBiz _addressBiz;


        public BuySellDocsController(BuySellDocBiz buySellDocsBiz, AddressBiz addressBiz, AbstractControllerParameters param)
            : base(buySellDocsBiz, param)
        {
            _buySellDocsBiz = buySellDocsBiz;
            _addressBiz = addressBiz;
        }


        CustomerBiz CustomerBiz
        {
            get
            {
                return _buySellDocsBiz.CustomerBiz;
            }
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                return _buySellDocsBiz.OwnerBiz;
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
                return _buySellDocsBiz.PersonBiz;
            }
        }

        BuySellDocBiz BuySellDocBiz
        {
            get
            {

                return _buySellDocsBiz;
            }
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            BuySellDoc buySellDoc = (BuySellDoc)parm.Entity;

            buySellDoc.IsNullThrowException("Unable to unbox buySellDoc");
            buySellDoc.HeadingForCreateForm = parm.BuySellDocumentTypeEnum.ToString().ToTitleSentance();


            //All document types are relative to the UserId
            //There are only 2 document types - Sale, Purchase
            //For some reason I am unable to get the dropdown box 
            //for to bind with the value during create. It works during Edit.
            //Anyway, we want to discourage creatingBuySellDocs directly. They should be created
            //through shopping cart only.
            if (parm.Entity.MenuManager.IsCreate)
            {
                buySellDoc.BuySellDocStateEnum = parm.BuySellDocStateEnum;
                buySellDoc.BuySellDocumentTypeEnum = parm.BuySellDocumentTypeEnum;

                switch (buySellDoc.BuySellDocumentTypeEnum)
                {


                    case BuySellDocumentTypeENUM.Purchase: //This is a sale
                        //Customer customer = CustomerBiz.Factory() as Customer;
                        break;


                    case BuySellDocumentTypeENUM.Sale:     //this is a delivery from the vendor's point of view 

                        //the customer is not the user.
                        buySellDoc.SelectListCustomer = CustomerBiz.SelectListWithout(UserId);

                        //the Owner is the User
                        buySellDoc.SelectListOwner = OwnerBiz.SelectListOnlyWith(UserId);

                        Owner ownerSale = OwnerBiz.GetPlayerFor(UserId);
                        ownerSale.IsNullThrowException();
                        buySellDoc.OwnerId = ownerSale.Id;
                        buySellDoc.BuySellDocStateEnum = BuySellDocStateENUM.Quotation;
                        if (buySellDoc.BuySellItems.IsNull())
                            buySellDoc.BuySellItems = new List<BuySellItem>();

                        break;

                    case BuySellDocumentTypeENUM.Unknown:
                    default:
                        buySellDoc.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Unknown;
                        return View("SelectDocumentTypeView", buySellDoc);
                }


                buySellDoc.SelectListAddressInformTo = AddressBiz.SelectListInformAddressFor(UserId);
                buySellDoc.SelectListAddressShipTo = AddressBiz.SelectListShipAddressFor(UserId);

            }
            else
            {
                codeForEdit(buySellDoc);

            }
            //BuySellDocBiz.Detach(buySellDoc);


            return base.Event_CreateViewAndSetupSelectList(parm);

        }

        private void codeForCreate(BuySellDoc buySellDoc)
        {
        }

        private void codeForEdit(BuySellDoc buySellDoc)
        {
            //this is the code if we are in Edit.
            //BASICALLY we need to load the select lists

            //we need to know on which side the user is of this transaction
            //then we will know if this is a purchase order or a sale type.

            //if the user is the customer, then this is a sale type
            //if the user is the owner, then this is a purchase order
            //if user is neither, and user is Admin, then allow.

            buySellDoc.BuySellDocumentTypeEnum = BuySellDocBiz.IsSaleOrPurchase(buySellDoc);

            switch (buySellDoc.BuySellDocumentTypeEnum)
            {
                case BuySellDocumentTypeENUM.Sale:
                    //here the UserId is the Owner
                    buySellDoc.SelectListOwner = OwnerBiz.SelectListOnlyWith(UserId);
                    buySellDoc.SelectListCustomer = CustomerBiz.SelectListWithout(UserId);

                    buySellDoc.SelectListAddressInformTo = AddressBiz.SelectList();
                    buySellDoc.SelectListAddressShipTo = AddressBiz.SelectList();
                    buySellDoc.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Sale;

                    break;

                case BuySellDocumentTypeENUM.Purchase:
                    //here the UserId is the customer
                    buySellDoc.SelectListOwner = OwnerBiz.SelectListWithout(UserId);
                    buySellDoc.SelectListCustomer = CustomerBiz.SelectListOnlyWith(UserId);

                    buySellDoc.SelectListAddressInformTo = AddressBiz.SelectListInformAddressFor(UserId);
                    buySellDoc.SelectListAddressShipTo = AddressBiz.SelectListShipAddressFor(UserId);
                    buySellDoc.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Purchase;
                    break;

                case BuySellDocumentTypeENUM.Unknown:
                    break;

                default:
                    break;
            }


        }


        public override void Event_BeforeSaveInCreateAndEdit(ControllerCreateEditParameter parm)
        {
            base.Event_BeforeSaveInCreateAndEdit(parm);

        }
        //private BuySellDocumentTypeENUM IsSaleOrPurchase(BuySellDoc buySellDoc)
        //{
        //    if (buySellDoc.CustomerId.IsNullOrWhiteSpace() && buySellDoc.OwnerId.IsNullOrWhiteSpace())
        //    {
        //        ErrorsGlobal.Add("Both Customer and Owner are empty.", "Event_CreateViewAndSetupSelectList");
        //        throw new Exception();

        //    }

        //    if (buySellDoc.CustomerId.IsNullOrWhiteSpace())
        //    {
        //        //this is a purchase order
        //        return BuySellDocumentTypeENUM.Purchase;

        //    }
        //    ApplicationUser ownerUser = OwnerBiz.GetUserForEntityrWhoIsNotAdminFor(buySellDoc.OwnerId);
        //    ownerUser.IsNullThrowException();
        //    if (UserId == ownerUser.Id)
        //    {
        //        //this is a purchase
        //        return BuySellDocumentTypeENUM.Purchase;
        //    }


        //    if (buySellDoc.OwnerId.IsNullOrWhiteSpace())
        //    {
        //        //this is a sale.
        //        return BuySellDocumentTypeENUM.Sale;


        //    }

        //    //get the CustomerUser
        //    ApplicationUser customerUser = CustomerBiz.GetUserForEntityrWhoIsNotAdminFor(buySellDoc.CustomerId);
        //    customerUser.IsNullThrowException();
        //    if (UserId == customerUser.Id)
        //    {
        //        //this is a sale
        //        return BuySellDocumentTypeENUM.Sale;
        //    }

        //    throw new Exception("Unknown type");
        //}

        private void setup_Purchase(BuySellDoc buySellDoc)
        {
            //the customer is the user.
            ApplicationUser customerUser = CustomerBiz.GetUserForEntityrWhoIsNotAdminFor(UserId);
            customerUser.IsNullThrowException("customerUser");

            //the Owner is the responding party

            buySellDoc.SelectListOwner = OwnerBiz.SelectListOnlyWith(UserId);
            buySellDoc.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Sale;
            buySellDoc.SelectListCustomer = CustomerBiz.SelectListOnlyWith(customerUser.Id);
            buySellDoc.SelectListAddressInformTo = AddressBiz.SelectListInformAddressFor(customerUser.Id);
            buySellDoc.SelectListAddressShipTo = AddressBiz.SelectListShipAddressFor(customerUser.Id);
            buySellDoc.SelectListOwner = OwnerBiz.SelectListOnlyWith(UserId);
        }

        /// <summary>
        /// Note. This can be approached from sale or purchase point of view. 
        /// </summary>
        /// <param name="buySellDoc"></param>
        /// <param name="userId"></param>
        private void setup_SalesOrder_And_Purchase(BuySellDoc buySellDoc, string userId)
        {
            //get the customer for the user.
            //
            ApplicationUser customerUser = CustomerBiz.GetUserForEntityrWhoIsNotAdminFor(buySellDoc.CustomerId);
            customerUser.IsNullThrowException("customerUser");
            buySellDoc.SelectListCustomer = CustomerBiz.SelectListOnlyWith(customerUser.Id);
            buySellDoc.SelectListAddressInformTo = AddressBiz.SelectListInformAddressFor(customerUser.Id);
            buySellDoc.SelectListAddressShipTo = AddressBiz.SelectListShipAddressFor(customerUser.Id);

            //get userId for Owner
            ApplicationUser ownerUser = OwnerBiz.GetUserForEntityrWhoIsNotAdminFor(buySellDoc.OwnerId);
            ownerUser.IsNullThrowException("ownerUser");
            buySellDoc.SelectListOwner = OwnerBiz.SelectListOnlyWith(ownerUser.Id);

            buySellDoc.BuySellDocumentTypeEnum = BuySellDocumentTypeENUM.Sale;
        }
        /// <summary>
        /// this is where the shopping cart enters.
        /// </summary>
        /// <param name="productChildId"></param>
        /// <returns></returns>

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
                message = string.Format("Not saved. Error: {0}", ErrorsGlobal.ToString());
                return Json(new
                {
                    success = false,
                    message = message,
                },
                JsonRequestBehavior.DenyGet);
            }
        }


        #region Sales Orders
        public ActionResult ListAllSalesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.All, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Open_SalesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.New, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_BackOrdered_SalesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.BackOrdered, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Canceled_SalesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Canceled, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Closed_SalesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Closed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Credit_SalesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Credit, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_InProccess_SalesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.InProccess, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Quotation_SalesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Quotation, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }




        public ActionResult ListAllSalesOrders_AdminScreen()
        {
            try
            {
                if (!Is_Admin)
                {
                    throw new Exception("Unable to continue. You do not have admin rights.");

                }
                //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
                DateTime fromDate = DateTime.Now.AddMonths(-3);
                DateTime toDate = DateTime.Now;
                BuySellStatementModel salesOrderList = BuySellDocBiz.Get_X_Orders_List(UserId, fromDate, toDate, true, BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.All);
                return View("ListAllSalesOrders", salesOrderList);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }

        }
        public ActionResult List_Open_SalesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.New, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_BackOrdered_SalesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.BackOrdered, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Canceled_SalesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Canceled, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Closed_SalesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Closed, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Credit_SalesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Credit, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_InProccess_SalesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.InProccess, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Quotation_SalesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Quotation, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Live_SalesOrders(string toDateString, bool isAdmin = false)
        {
            try
            {
                if (isAdmin)
                {
                    //check if person is actually admin
                    if (!Is_Admin)
                    {
                        throw new Exception("Unable to continue. You do not have admin rights.");

                    }
                }
                DateTime toDateNotNull;
                bool success = DateTime.TryParse(toDateString, out toDateNotNull);

                if (!success)
                {
                    throw new Exception("Unable to parse date. String recieved: '" + toDateString + "'");
                }

                toDateNotNull = toDateNotNull.AddDays(-1);
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
                DateTime fromDate = toDateNotNull.AddMonths(-3);
                BuySellStatementModel salesOrderList = BuySellDocBiz.Get_X_Orders_List(UserId, fromDate, toDateNotNull, isAdmin, BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.All);
                return View("ListAllSalesOrders", salesOrderList);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }

        }

        public ActionResult GetSaleOrderTotals()
        {
            //UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new UserMoneyAccount();
            MoneyItemParent moneyItemParent = ViewBag.MoneyItemParent as MoneyItemParent ?? new MoneyItemParent();

            return View(moneyItemParent);

        }
        public ActionResult GetSaleOrderTotals_Admin()
        {
            try
            {
                UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.UserMoneyAccount();

                if (!moneyAccount.IsAdmin)
                    throw new Exception("You are not an administrator");
                return View(moneyAccount);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }
        }

        #endregion

        #region Purchase Orders
        public ActionResult List_Purchase_All()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.All, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Open_PurchasesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.New, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_BackOrdered_PurchasesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.BackOrdered, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Canceled_PurchasesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Canceled, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Closed_PurchasesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Closed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Credit_PurchasesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Credit, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_InProccess_PurchasesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.InProccess, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Quotation_PurchasesOrders()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Quotation, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }




        public ActionResult ListAllPurchasesOrders_AdminScreen()
        {
            try
            {
                if (!Is_Admin)
                {
                    throw new Exception("Unable to continue. You do not have admin rights.");

                }
                //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
                DateTime fromDate = DateTime.Now.AddMonths(-3);
                DateTime toDate = DateTime.Now;
                BuySellStatementModel salesOrderList = BuySellDocBiz.Get_X_Orders_List(UserId, fromDate, toDate, true, BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.All);
                return View("ListAllPurchasesOrders", salesOrderList);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }

        }
        public ActionResult List_Open_PurchasesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.New, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_BackOrdered_PurchasesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.BackOrdered, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Canceled_PurchasesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Canceled, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Closed_PurchasesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Closed, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Credit_PurchasesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Credit, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_InProccess_PurchasesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.InProccess, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Quotation_PurchasesOrders_AdminScreen()
        {
            try
            {
                return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Quotation, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Live_PurchasesOrders(string toDateString, bool isAdmin = false)
        {
            try
            {
                if (isAdmin)
                {
                    //check if person is actually admin
                    if (!Is_Admin)
                    {
                        throw new Exception("Unable to continue. You do not have admin rights.");

                    }
                }
                DateTime toDateNotNull;
                bool success = DateTime.TryParse(toDateString, out toDateNotNull);

                if (!success)
                {
                    throw new Exception("Unable to parse date. String recieved: '" + toDateString + "'");
                }

                toDateNotNull = toDateNotNull.AddDays(-1);
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
                DateTime fromDate = toDateNotNull.AddMonths(-3);
                BuySellStatementModel salesOrderList = BuySellDocBiz.Get_X_Orders_List(UserId, fromDate, toDateNotNull, isAdmin, BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.All);
                return View("ListAllPurchasesOrders", salesOrderList);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }

        }

        public ActionResult GetPurchaseOrderTotals()
        {
            try
            {
                //UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new UserMoneyAccount();
                MoneyItemParent moneyItemParent = ViewBag.MoneyItemParent as MoneyItemParent ?? new MoneyItemParent();
                moneyItemParent.IsNullThrowException();
                return View(moneyItemParent);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");
            }
        }
        public ActionResult GetPurchaseOrderTotals_Admin()
        {
            try
            {

                UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.UserMoneyAccount();
                moneyAccount.IsNullThrowException("money account not received.");
                if (!moneyAccount.IsAdmin)
                    throw new Exception("You are not an administrator");
                return View(moneyAccount);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }
        }


        //public ActionResult GetPurchaseTotals()
        //{
        //    UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new UserMoneyAccount();
        //    return View(moneyAccount);

        //}
        //public ActionResult GetPurchaseTotals_Admin()
        //{
        //    try
        //    {
        //        UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.UserMoneyAccount();
        //        if (!moneyAccount.IsAdmin)
        //            throw new Exception("You are not an administrator");
        //        return View(moneyAccount);

        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Index");

        //    }
        //}
        //public ActionResult GetPurchaseTotals()
        //{
        //    UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new UserMoneyAccount();
        //    return View(moneyAccount);

        //}
        //public ActionResult GetPurchaseTotals_Admin()
        //{
        //    try
        //    {
        //        UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.UserMoneyAccount();
        //        if (!moneyAccount.IsAdmin)
        //            throw new Exception("You are not an administrator");
        //        return View(moneyAccount);

        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Index");

        //    }
        //}

        #endregion

        public override void Event_AfterDeleting(string id)
        {
            base.Event_AfterDeleting(id);

            //we need to mark all the items deleted as well.
            //get a list of all the items
            //  List<BuySellItem> listOfItems = 

        }



        private ActionResult throwError(Exception e)
        {
            ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
            ErrorsGlobal.MemorySave();
            return RedirectToAction("Index", "Menus");
        }

        private ActionResult ListOrdersFor(BuySellDocumentTypeENUM buySellDocumentTypeEnum, BuySellDocStateENUM buySellDocStateEnum, bool isAdmin)
        {

            try
            {
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
                DateTime fromDate = DateTime.Now.AddMonths(-3);
                DateTime toDate = DateTime.Now;
                BuySellStatementModel orderList = BuySellDocBiz.Get_X_Orders_List(UserId, fromDate, toDate, isAdmin, buySellDocumentTypeEnum, buySellDocStateEnum);
                switch (buySellDocumentTypeEnum)
                {
                    case BuySellDocumentTypeENUM.Sale:

                        return View("ListAllSalesOrders", orderList);

                    case BuySellDocumentTypeENUM.Purchase:
                        return View("ListOrders", orderList);

                    case BuySellDocumentTypeENUM.Unknown:
                    default:
                        ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod());
                        throw new Exception(ErrorsGlobal.ToString());
                }

            }
            catch (Exception e)
            {

                return RedirectToAction("Index", "Menus");
            }
        }

        //public  ActionResult SelectDocumentTypeView(string isandForSearch, MenuENUM menuEnum = MenuENUM.CreateDefault, string productChildId = "", string menuPathMainId = "", string productId = "", string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, bool isMenu = false, string parentId = "", DocumentTypeENUM documentType = DocumentTypeENUM.Unknown)
        //{

        //    return View("SelectDocumentTypeView");
        //}

    }
}