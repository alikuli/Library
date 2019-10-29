using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FreightOffersTrxNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.FreightOffersTrxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.DeliverymanNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.PlayersNS.VehicalTypeNS;
using UowLibrary.SuperLayerNS;
namespace MarketPlace.Web6.Controllers
{
    /// <summary>
    /// Note, where programming for different kinds of documents, Sale, Purchase and Delivery, the following program levels
    /// have to be considered.
    /// 
    /// CONTROLLER LEVEL
    ///     BuySellDocsController
    ///
    ///     BuySellDoc.ListOrders
    ///     BuySellDoc._FieldsOnlyEditFormatfor 
    ///     BuySellDoc._ListItems
    ///     BuySellItem._FieldsOnlyEditFormat 
    ///     
    /// BUSINESS RULE LEVELS
    ///     BuySellDocBiz.BussinessRules
    ///     BuySellItem.BussinessRules
    ///     
    /// MODEL LEVEL
    ///     BuySellDoc.UpdatePropertiesDuringModify
    ///     BuySellItem.UpdatePropertiesDuringModify
    ///     
    /// In order for a seller to accept an order, he must have a minimum balance of Rs. x amount. This is so as to cover
    /// any commission expenses, otherwise the account may go negative.
    /// </summary>
    [Authorize]
    public class BuySellDocsController : EntityAbstractController<BuySellDoc>
    {

        //BuySellDocBiz _buySellDocsBiz;
        AddressBiz _addressBiz;
        //DeliverymanBiz _deliverymanBiz;
        SuperBiz _superBiz;

        public BuySellDocsController(/* BuySellDocBiz buySellDocsBiz DeliverymanBiz deliverymanBiz, */ AbstractControllerParameters param, SuperBiz superBiz, AddressBiz addressBiz)
            : base(superBiz.BuySellDocBiz, param)
        {
            //_buySellDocsBiz = buySellDocsBiz;
            _addressBiz = addressBiz;
            //_deliverymanBiz = deliverymanBiz;
            _superBiz = superBiz;
        }

        SuperBiz SuperBiz
        {
            get
            {
                _superBiz.UserId = UserId;
                _superBiz.UserName = UserName;
                return _superBiz;
            }
        }
        VehicalTypeBiz VehicalTypeBiz
        {
            get
            {
                return SuperBiz.VehicalTypeBiz;
            }
        }
        DeliverymanBiz DeliverymanBiz
        {
            get
            {
                return SuperBiz.DeliverymanBiz;
            }
        }
        CustomerBiz CustomerBiz
        {
            get
            {
                return SuperBiz.CustomerBiz;
            }
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                return SuperBiz.OwnerBiz;
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
                return SuperBiz.PersonBiz;
            }
        }

        BuySellDocBiz BuySellDocBiz
        {
            get
            {

                return SuperBiz.BuySellDocBiz;
            }
        }
        FreightOfferTrxBiz FreightOfferTrxBiz
        {
            get { return SuperBiz.BuySellDocBiz.FreightOfferTrxBiz; }
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            try
            {

                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");

                BuySellDoc buySellDoc = (BuySellDoc)parm.Entity;
                buySellDoc.IsNullThrowException("Unable to unbox buySellDoc");

                //do work
                BuySellDocBiz.CreateHeadingForCreateForm(parm);
                BuySellDocBiz.Load_DocStateAndType_Into_Items(buySellDoc);

                BuySellDocBiz.GetDefaultVehicalType(buySellDoc);
                BuySellDocBiz.LoadSelectListsFor_GET(buySellDoc);

                //buySellDoc.BuySellDocumentTypeEnum = parm.BuySellDocumentTypeEnum;

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
            }
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);

        }

        public ActionResult RejectOrder(string id, string returnUrl, BuySellDocumentTypeENUM buySellDocumentTypeEnum)
        {
            try
            {

                BuySellDocBiz.RejectOrder_Code(id, buySellDocumentTypeEnum, GlobalObject);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
            }
            return Redirect(returnUrl);
        }



        public ActionResult CancelRejectOrder(string buySellDocId, string returnUrl, string text, BuySellDocumentTypeENUM buySellDocumentTypeEnum = BuySellDocumentTypeENUM.Unknown, BuySellDocStateModifierENUM buySellDocStateModifierEnum = BuySellDocStateModifierENUM.Unknown)
        {

            try
            {
                buySellDocId.IsNullOrWhiteSpaceThrowArgumentException("Id");
                returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");
                //text.IsNullOrWhiteSpaceThrowArgumentException("text");


                if (buySellDocumentTypeEnum == BuySellDocumentTypeENUM.Unknown)
                    throw new Exception("buySellDocumentType unknown");


                BuySellDoc buySellDoc = BuySellDocBiz.Find(buySellDocId);
                buySellDoc.IsNullThrowException();
                buySellDoc.BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
                buySellDoc.BuySellDocStateModifierEnum = buySellDocStateModifierEnum;
                //get the penalty amounts
                PersonPayingPenalty personPayingPenalty;
                IPenaltyClass penaltyClass = PenaltyController.GetPenalty(buySellDoc, out personPayingPenalty);

                if (!penaltyClass.IsNull())
                {
                    text = penaltyClass.Text;
                }
                else
                {
                    text = "No Value Received";
                }

                RejectCancelDeleteInbetweenClass rejectCancelDeleteInbetweenClass = new RejectCancelDeleteInbetweenClass(
                    returnUrl,
                    text,
                    buySellDoc,
                    GlobalObject);

                return View(rejectCancelDeleteInbetweenClass);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                return Redirect(returnUrl);
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CancelRejectOrder(RejectCancelDeleteInbetweenClass rejectCancelDeleteInbetweenClass, string button)
        {


            try
            {

                rejectCancelDeleteInbetweenClass.IsNullThrowException();
                rejectCancelDeleteInbetweenClass.ReturnUrl.IsNullOrWhiteSpaceThrowException();

                if (button.IsNullOrWhiteSpace())
                    return Redirect(rejectCancelDeleteInbetweenClass.ReturnUrl);


                if (button.ToLower() == "accept")
                {

                    //rejectCancelDeleteInbetweenClass.BuySellDocumentTypeEnum + rejectCancelDeleteInbetweenClass.BuySellDoc.BuySellDocStateEnum.ToString().ToTitleSentance();
                    rejectCancelDeleteInbetweenClass.GlobalObject = SuperBiz.GetGlobalObject();
                    SuperBiz.CancelRejectOrder(rejectCancelDeleteInbetweenClass);
                }

            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
            }

            return Redirect(rejectCancelDeleteInbetweenClass.ReturnUrl);

        }


        public override void Event_Before_Edit_UpdateAndSaveAsync(ControllerCreateEditParameter parm)
        {
            parm.Entity.IsNullThrowException();
            BuySellDoc buySellDoc = BuySellDoc.UnBox(parm.Entity);

            //set the buySellDoc.BuySellDocStateModifierEnum value
            buySellDoc.BuySellDocStateModifierEnum = parm.BuySellDocStateModifierEnum;

            if (buySellDoc.BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Accept)
            {
                //getCustomerSalesmanAndOwnerSalesman(buySellDoc);
                //getDeliverymanSalesman(buySellDoc);
            }
        }

        //private void getDeliverymanSalesman(BuySellDoc bsd)
        //{
        //    if (bsd.BuySellDocumentTypeEnum == BuySellDocumentTypeENUM.Delivery)
        //    {
        //        //this is the current state BuySellDocStateENUM.RequestConfirmed moving on to BuySellDocStateENUM.CourierComingToPickUp
        //        if (bsd.BuySellDocStateEnum == BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller)
        //        {
        //            if (bsd.DeliverymanSalesmanId.IsNullOrWhiteSpace())
        //                SuperBiz.GetAllDeliverySalesmen(bsd);

        //        }
        //    }
        //}




        public ActionResult CancelDeliveryMan(string buySellDocId, string returnUrl, BuySellDocumentTypeENUM buySellDocumentTypeEnum = BuySellDocumentTypeENUM.Unknown)
        {

            try
            {
                BuySellDocBiz.CancelDeliveryManAndSave_GET(buySellDocId, buySellDocumentTypeEnum);

            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
            }
            return Redirect(returnUrl);
        }

        //private void CancelDeliveryMan_Code(string buySellDocId, BuySellDocumentTypeENUM buySellDocumentTypeEnum)
        //{
        //    try
        //    {
        //        BuySellDocBiz.CancelDeliveryManAndSave(buySellDocId, buySellDocumentTypeEnum);
        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
        //    }
        //}

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            try
            {
                SuperBiz.Event_Edit_ViewAndSetupSelectList_Get_Code(parm);
                BuySellDoc bsd = BuySellDoc.UnBox(parm.Entity);

                Hide_Save_Button(bsd);

            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);

            }
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);

        }




        public override void Event_Edit_InError_Post(ControllerCreateEditParameter parm)
        {
            //I added the if because when it breaks in get offers, it goes into edit... we
            //dont want that.
            if (parm.ReturnUrl.IsNullOrWhiteSpace())
                parm.ReturnUrl = Url.Action("Edit", new { id = parm.Entity.Id });
        }

        [HttpPost]
        public ActionResult BuyAjax(string productChildId)
        {
            string message;
            bool success;
            try
            {
                SuperBiz.BuyAjax_Code(productChildId, out message, out success);

                return Json(new
                            {
                                success = success,
                                message = message,
                            },
                            JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                return Json(new
                {
                    success = false,
                    message = ErrorsGlobal.ToString(),
                },
                JsonRequestBehavior.DenyGet);

            }
        }




        public ActionResult GetDeliveryOrderTotals()
        {
            try
            {
                GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
                globalObject.IsNullThrowException();
                return View(globalObject);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");
            }
        }

        public ActionResult GetDeliveryOrderTotals_Admin()
        {
            try
            {

                GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
                globalObject.IsNullThrowException("money account not received.");

                if (!globalObject.IsAdmin)
                    throw new Exception("You are not an administrator");

                return View(globalObject);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }

        }




        public ActionResult Delivery(string buySellId)
        {
            BuySellDoc buySellDoc = new BuySellDoc();
            try
            {
                buySellDoc = SuperBiz.GetDeliveryOrder(buySellId);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
            }
            return View(buySellDoc);
        }

        public ActionResult AcceptDeliveryman(string frtOfferId, string returnUrl, decimal insuranceRequired, BuySellDocumentTypeENUM buySellDocumentTypeEnum = BuySellDocumentTypeENUM.Unknown)
        {
            try
            {
                AcceptDeliveryMan_Code(frtOfferId, buySellDocumentTypeEnum, insuranceRequired);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
            }
            return Redirect(returnUrl);

        }

        public ActionResult Deliveryman_Accepts_To_Deliver(string frtOfferId, string buySellId, string returnUrl)
        {
            try
            {
                SuperBiz.Deliveryman_Accepts_To_Deliver(frtOfferId, buySellId, GlobalObject);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
            }
            return Redirect(returnUrl);

        }


        #region Sales Orders Admin
        public ActionResult List_Sale_All_Admin()
        {
            try
            {

                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.All, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_RequestConfirmed_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.RequestConfirmed, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Sale_RequestUnconfirmed_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.RequestUnconfirmed, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }


        public ActionResult List_Sale_CourierAccepted_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_ConfirmedByCourier_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.CourierComingToPickUp, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_ConfirmedBySeller_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.BeingPreparedForShipmentBySeller, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_Delivered_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Delivered, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_InProccess_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.InProccess, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_PickedUp_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.PickedUp, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        //public ActionResult List_Sale_Enroute()
        //{
        //    try
        //    {
        //        return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Enroute, true);

        //    }
        //    catch (Exception e)
        //    {
        //        return throwError(e);

        //    }

        //}
        public ActionResult List_Sale_Enroute_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Enroute, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_Problem_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Problem, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_ReadyForShipment_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.ReadyForPickup, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_Rejected_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Rejected, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }


        public ActionResult List_Sale_CourierAcceptedByBuyerAndSeller_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        #endregion

        #region Purchase Orders Admin
        public ActionResult List_Purchase_All_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.All, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_RequestConfirmed_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.RequestConfirmed, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Purchase_RequestUnconfirmed_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.RequestUnconfirmed, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Purchase_CourierAccepted_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Purchase_ConfirmedByCourier_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.CourierComingToPickUp, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_ConfirmedBySeller_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.BeingPreparedForShipmentBySeller, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_Delivered_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Delivered, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_InProccess_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.InProccess, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_PickedUp_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.PickedUp, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_Enroute_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Enroute, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_Problem_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Problem, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_ReadyForShipment_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.ReadyForPickup, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_Rejected_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Rejected, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }




        //public ActionResult ListAllPurchasesOrders_AdminScreen()
        //{
        //    try
        //    {
        //        if (!Is_Admin)
        //        {
        //            throw new Exception("Unable to continue. You do not have admin rights.");

        //        }
        //        //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
        //        DateTime fromDate = DateTime.Now.AddMonths(-3);
        //        DateTime toDate = DateTime.Now;
        //        BuySellStatementModel salesOrderList = BuySellDocBiz.GetBuySellStatementModel(UserId, fromDate, toDate, true, BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.All);
        //        return View("ListAllPurchasesOrders", salesOrderList);

        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Index");

        //    }

        //}
        //public ActionResult List_Open_PurchasesOrders_AdminScreen()
        //{
        //    try
        //    {
        //        return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.New, true);

        //    }
        //    catch (Exception e)
        //    {
        //        return throwError(e);

        //    }

        //}
        //public ActionResult List_BackOrdered_PurchasesOrders_AdminScreen()
        //{
        //    try
        //    {
        //        return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.BackOrdered, true);

        //    }
        //    catch (Exception e)
        //    {
        //        return throwError(e);

        //    }

        //}
        //public ActionResult List_Live_PurchasesOrders(string toDateString, bool isAdmin = true)
        //{
        //    try
        //    {
        //        if (isAdmin)
        //        {
        //            //check if person is actually admin
        //            if (!Is_Admin)
        //            {
        //                throw new Exception("Unable to continue. You do not have admin rights.");

        //            }
        //        }
        //        DateTime toDateNotNull;
        //        bool success = DateTime.TryParse(toDateString, out toDateNotNull);

        //        if (!success)
        //        {
        //            throw new Exception("Unable to parse date. String recieved: '" + toDateString + "'");
        //        }

        //        toDateNotNull = toDateNotNull.AddDays(-1);
        //        UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
        //        DateTime fromDate = toDateNotNull.AddMonths(-3);
        //        BuySellStatementModel salesOrderList = BuySellDocBiz.GetBuySellStatementModel(UserId, fromDate, toDateNotNull, isAdmin, BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.All);
        //        return View("ListAllPurchasesOrders", salesOrderList);

        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Index");

        //    }

        //}

        //public ActionResult GetPurchaseOrderTotals()
        //{
        //    try
        //    {
        //        GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
        //        globalObject.IsNullThrowException();
        //        return View(globalObject);

        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Index");
        //    }
        //}
        //public ActionResult GetPurchaseOrderTotals_Admin()
        //{
        //    try
        //    {

        //        GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
        //        globalObject.IsNullThrowException("money account not received.");

        //        if (!globalObject.IsAdmin)
        //            throw new Exception("You are not an administrator");

        //        return View(globalObject);

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

        #region Salesman Orders Admin
        public ActionResult List_Salesman_All_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.All, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_RequestConfirmed_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.RequestConfirmed, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Salesman_RequestUnconfirmed_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.RequestUnconfirmed, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Salesman_CourierAccepted_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Salesman_ConfirmedByCourier_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.CourierComingToPickUp, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_ConfirmedBySeller_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.BeingPreparedForShipmentBySeller, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_Delivered_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.Delivered, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_InProccess_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.InProccess, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_PickedUp_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.PickedUp, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_Enroute_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.Enroute, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_Problem_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.Problem, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_ReadyForShipment_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.ReadyForPickup, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_Rejected_Admin()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.Rejected, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }






        #endregion

        #region Sales Orders
        public ActionResult List_Sale_All()
        {
            try
            {

                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.All, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_RequestConfirmed()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.RequestConfirmed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Sale_RequestUnconfirmed()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.RequestUnconfirmed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }


        public ActionResult List_Sale_CourierAccepted()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_ConfirmedByCourier()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.CourierComingToPickUp, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_ConfirmedBySeller()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.BeingPreparedForShipmentBySeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_Delivered()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Delivered, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_InProccess()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.InProccess, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_PickedUp()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.PickedUp, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        //public ActionResult List_Sale_Enroute()
        //{
        //    try
        //    {
        //        return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Enroute, false);

        //    }
        //    catch (Exception e)
        //    {
        //        return throwError(e);

        //    }

        //}
        public ActionResult List_Sale_Enroute()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Enroute, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_Problem()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Problem, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_ReadyForShipment()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.ReadyForPickup, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Sale_Rejected()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Rejected, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }


        public ActionResult List_Sale_CourierAcceptedByBuyerAndSeller()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }


        public ActionResult ListAllSalesOrders_AdminScreen()
        {
            throw new NotImplementedException();
            //try
            //{
            //    if (!Is_Admin)
            //    {
            //        throw new Exception("Unable to continue. You do not have admin rights.");

            //    }
            //    //UserId.IsNullOrWhiteSpaceThrowException("You are not logged in.");
            //    DateTime fromDate = DateTime.Now.AddMonths(-3);
            //    DateTime toDate = DateTime.Now;
            //    BuySellStatementModel salesOrderList = BuySellDocBiz.Get_X_Orders_List(UserId, fromDate, toDate, true, BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.All);
            //    return View("ListAllSalesOrders", salesOrderList);

            //}
            //catch (Exception e)
            //{
            //    ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
            //    ErrorsGlobal.MemorySave();
            //    return RedirectToAction("Index");

            //}

        }
        //public ActionResult List_Open_SalesOrders_AdminScreen()
        //{
        //    try
        //    {
        //        return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.New, true);

        //    }
        //    catch (Exception e)
        //    {
        //        return throwError(e);

        //    }

        //}
        public ActionResult List_BackOrdered_SalesOrders_AdminScreen()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.BackOrdered, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        //public ActionResult List_Canceled_SalesOrders_AdminScreen()
        //{

        //    try
        //    {
        //        return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Canceled, true);

        //    }
        //    catch (Exception e)
        //    {
        //        return throwError(e);

        //    }

        //}
        //public ActionResult List_Closed_SalesOrders_AdminScreen()
        //{
        //    try
        //    {
        //        return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Closed, true);

        //    }
        //    catch (Exception e)
        //    {
        //        return throwError(e);

        //    }

        //}
        //public ActionResult List_Credit_SalesOrders_AdminScreen()
        //{
        //    try
        //    {
        //        return ListOrdersFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.Credit, true);

        //    }
        //    catch (Exception e)
        //    {
        //        return throwError(e);

        //    }

        //}
        public ActionResult List_InProccess_SalesOrders_AdminScreen()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.InProccess, true);

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
                BuySellStatementModel buySellStatementModel = BuySellDocBiz.GetBuySellStatementModel(UserId, fromDate, toDateNotNull, isAdmin, BuySellDocumentTypeENUM.Sale, BuySellDocStateENUM.All);

                return View("ListAllSalesOrders", buySellStatementModel);

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
            GlobalObject moneyItemParent = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();

            return View(moneyItemParent);

        }
        public ActionResult GetSaleOrderTotals_Admin()
        {
            try
            {
                GlobalObject globalAccount = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
                //UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.UserMoneyAccount();

                if (!globalAccount.IsAdmin)
                    throw new Exception("You are not an administrator");
                return View(globalAccount);

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
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.All, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_RequestConfirmed()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.RequestConfirmed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Purchase_RequestUnconfirmed()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.RequestUnconfirmed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Purchase_CourierAccepted()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Purchase_ConfirmedByCourier()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.CourierComingToPickUp, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_ConfirmedBySeller()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.BeingPreparedForShipmentBySeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_Delivered()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Delivered, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_InProccess()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.InProccess, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_PickedUp()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.PickedUp, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_Enroute()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Enroute, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_Problem()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Problem, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_ReadyForShipment()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.ReadyForPickup, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Purchase_Rejected()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.Rejected, false);

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
                BuySellStatementModel salesOrderList = BuySellDocBiz.GetBuySellStatementModel(UserId, fromDate, toDate, true, BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.All);
                return View("ListAllPurchasesOrders", salesOrderList);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }

        }
        //public ActionResult List_Open_PurchasesOrders_AdminScreen()
        //{
        //    try
        //    {
        //        return ListOrdersFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.New, true);

        //    }
        //    catch (Exception e)
        //    {
        //        return throwError(e);

        //    }

        //}
        public ActionResult List_BackOrdered_PurchasesOrders_AdminScreen()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.BackOrdered, true);

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
                BuySellStatementModel salesOrderList = BuySellDocBiz.GetBuySellStatementModel(UserId, fromDate, toDateNotNull, isAdmin, BuySellDocumentTypeENUM.Purchase, BuySellDocStateENUM.All);
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
                GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
                globalObject.IsNullThrowException();
                return View(globalObject);

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

                GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
                globalObject.IsNullThrowException("money account not received.");

                if (!globalObject.IsAdmin)
                    throw new Exception("You are not an administrator");

                return View(globalObject);

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

        #region Salesman Orders
        public ActionResult List_Salesman_All()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.All, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_RequestConfirmed()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.RequestConfirmed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Salesman_RequestUnconfirmed()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.RequestUnconfirmed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Salesman_CourierAccepted()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Salesman_ConfirmedByCourier()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.CourierComingToPickUp, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_ConfirmedBySeller()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.BeingPreparedForShipmentBySeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_Delivered()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.Delivered, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_InProccess()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.InProccess, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_PickedUp()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.PickedUp, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_Enroute()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.Enroute, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_Problem()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.Problem, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_ReadyForShipment()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.ReadyForPickup, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Salesman_Rejected()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.Rejected, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }




        public ActionResult ListAllSalesmansOrders_AdminScreen()
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
                BuySellStatementModel salesOrderList = BuySellDocBiz.GetBuySellStatementModel(UserId, fromDate, toDate, true, BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.All);
                return View("ListAllSalesmansOrders", salesOrderList);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }

        }

        public ActionResult List_BackOrdered_SalesmansOrders_AdminScreen()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.BackOrdered, true);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Live_SalesmansOrders(string toDateString, bool isAdmin = false)
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
                BuySellStatementModel salesOrderList = BuySellDocBiz.GetBuySellStatementModel(UserId, fromDate, toDateNotNull, isAdmin, BuySellDocumentTypeENUM.Salesman, BuySellDocStateENUM.All);
                return View("ListAllSalesmansOrders", salesOrderList);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }

        }

        public ActionResult GetSalesmanOrderTotals()
        {
            try
            {
                GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
                globalObject.IsNullThrowException();
                return View(globalObject);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");
            }
        }
        public ActionResult GetSalesmanOrderTotals_Admin()
        {
            try
            {

                GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
                globalObject.IsNullThrowException("money account not received.");

                if (!globalObject.IsAdmin)
                    throw new Exception("You are not an administrator");

                return View(globalObject);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }
        }








        #endregion

        #region Delivery


        public ActionResult List_Delivery_All()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.All, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Delivery_RequestConfirmed()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.RequestConfirmed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Delivery_RequestUnconfirmed()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.RequestUnconfirmed, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Delivery_CourierAccepted()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Delivery_ConfirmedByCourier()
        {
            try
            {

                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.CourierComingToPickUp, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Delivery_ConfirmedBySeller()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.BeingPreparedForShipmentBySeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Delivery_Delivered()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.Delivered, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Delivery_InProccess()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.InProccess, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Delivery_PickedUp()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.PickedUp, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Delivery_Enroute()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.Enroute, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Delivery_Problem()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.Problem, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Delivery_ReadyForShipment()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.ReadyForPickup, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        public ActionResult List_Delivery_Rejected()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.Rejected, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }

        public ActionResult List_Delivery_CourierAcceptedByBuyerAndSeller()
        {
            try
            {
                return getOrdersListFor(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, false);

            }
            catch (Exception e)
            {
                return throwError(e);

            }

        }
        #endregion

        #region Delivery Orders

        public ActionResult GetAllOrdersReadyForShipment()
        {
            try
            {
                BuySellStatementModel buySellStatementModel = SuperBiz.GetOrdersList(BuySellDocumentTypeENUM.Delivery, BuySellDocStateENUM.ReadyForPickup, false);
                hide_Save_Buton(buySellStatementModel);

                return View("ListOrders", buySellStatementModel);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Menus");
            }
        }

        private void hide_Save_Buton(BuySellStatementModel buySellStatementModel)
        {
            if (buySellStatementModel.GetBuySellDocViewState(BuySellDocStateENUM.ReadyForPickup, BuySellDocumentTypeENUM.Delivery, null, null).HD_Hide_Save_Button_In_Edit)
            {
                ViewBag.ShowEditControls = false.ToString();
            }
        }

        #endregion

        #region Controller Methods

        private void Hide_Save_Button(BuySellDoc buySellDoc)
        {
            buySellDoc.BuySellDocViewState.IsNullThrowException();
            if (buySellDoc.BuySellDocViewState.HD_Hide_Save_Button_In_Edit)
            {
                ViewBag.ShowEditControls = false.ToString();


            }
        }



        //private bool Deliveryman_Accepts_To_Deliver_Code(string frtOfferId)
        //{
        //    try
        //    {
        //        UserId.IsNullOrWhiteSpaceThrowException();
        //        SuperBiz.Deliveryman_Accepts_To_Deliver(frtOfferId);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);

        //    }
        //    return false;
        //}

        private bool AcceptDeliveryMan_Code(string frtOfferId, BuySellDocumentTypeENUM buySellDocumentTypeEnum, decimal insuranceRequired)
        {
            try
            {
                if (buySellDocumentTypeEnum == BuySellDocumentTypeENUM.Unknown)
                {
                    throw new Exception("Document type is not known.");
                }
                UserId.IsNullOrWhiteSpaceThrowException();
                frtOfferId.IsNullOrWhiteSpaceThrowException();

                FreightOfferTrx frt = FreightOfferTrxBiz.Find(frtOfferId);
                frt.IsNullThrowException();
                frt.Deliveryman.IsNullThrowException();
                frt.Deliveryman.PersonId.IsNullOrWhiteSpaceThrowException();

                string deliverymanPersonId = frt.Deliveryman.PersonId;

                //the UserId in this case is the one accepting the eliveryman... maybe the seller, or customer.
                //so we need to get the delveryman


                decimal currRefundabelBalance = SuperBiz.BalanceFor_Person(deliverymanPersonId, CashTypeENUM.Refundable);
                BuySellDocBiz.AcceptCourier(frt, buySellDocumentTypeEnum, currRefundabelBalance, insuranceRequired);
                return true;
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);

            }
            return false;
        }


        private ActionResult getOrdersListFor(BuySellDocumentTypeENUM buySellDocumentTypeEnum, BuySellDocStateENUM buySellDocStateEnum, bool isWantAdminPrivilages)
        {

            try
            {
                BuySellStatementModel buySellStatementModel = SuperBiz.GetOrdersList(buySellDocumentTypeEnum, buySellDocStateEnum, isWantAdminPrivilages);
                return View("ListOrders", buySellStatementModel);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Menus");
            }
        }




        #endregion



 
    }
}