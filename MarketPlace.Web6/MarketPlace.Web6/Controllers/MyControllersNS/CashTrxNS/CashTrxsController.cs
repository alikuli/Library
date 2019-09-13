using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.BuySellDocNS;
using UowLibrary.CashEncashmentTrxNS;
using UowLibrary.CashTtxNS;
using UowLibrary.PaymentMethodNS;
using UowLibrary.PlayersNS.BankNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.DeliverymanNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using UowLibrary.SuperLayerNS;
namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class CashTrxsController : EntityAbstractController<CashTrx>
    {

        SuperBiz _superCashBiz;

        public CashTrxsController(SuperBiz superCashBiz)
            : base(superCashBiz.CashTrxBiz, superCashBiz.AbstractControllerParameters)
        {

            _superCashBiz = superCashBiz;

        }


        CashEncashmentTrxBiz CashEncashmentTrxBiz
        {
            get
            {
                return SuperBiz.CashEncashmentTrxBiz;
            }
        }
        PaymentMethodBiz PaymentMethodBiz
        {
            get
            {
                return SuperBiz.PaymentMethodBiz;
            }
        }
        BankBiz BankBiz
        {
            get
            {
                return SuperBiz.BankBiz;

            }
        }

        PersonBiz PersonBiz
        {
            get
            {
                return CashTrxBiz.PersonBiz;
                //return _cashTrxsBiz.PersonBiz;
            }
        }

        UserBiz UserBiz
        {
            get
            {
                return PersonBiz.UserBiz;
            }
        }

        CashTrxBiz CashTrxBiz
        {
            get
            {
                return SuperBiz.CashTrxBiz;

            }
        }
        BuySellDocBiz BuySellDocBiz
        {
            get
            {
                return SuperBiz.BuySellDocBiz;

                //_buySellDocBiz.IsNullThrowException();
                //_buySellDocBiz.UserId = UserId;
                //_buySellDocBiz.UserName = UserName;

                //return _buySellDocBiz;
            }
        }

        CustomerBiz CustomerBiz
        {
            get
            {
                return BuySellDocBiz.CustomerBiz;
            }
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                return BuySellDocBiz.OwnerBiz;
            }
        }

        DeliverymanBiz DeliverymanBiz
        {
            get
            {
                return BuySellDocBiz.DeliverymanBiz;
            }
        }

        SuperBiz SuperBiz
        {
            get
            {
                _superCashBiz.IsNullThrowException();
                _superCashBiz.UserId = UserId;
                _superCashBiz.UserName = UserName;

                return _superCashBiz;
            }
        }
        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {

            CashTrx paymentTrx = parm.Entity as CashTrx;
            paymentTrx.IsNullThrowException("Unable to unbox paymentTrx");


            if (UserBiz.IsAdmin(UserId))
            {
                paymentTrx.SelectListPeopleFrom = PersonBiz.SelectList();
                paymentTrx.SelectListPeopleTo = PersonBiz.SelectList();

            }
            else
            {
                paymentTrx.SelectListPeopleFrom = PersonBiz.SelectListWithoutPersonFor(UserId);
                paymentTrx.SelectListPeopleTo = PersonBiz.SelectListWithoutPersonFor(UserId);
            }
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }


        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            CashTrx paymentTrx = parm.Entity as CashTrx;
            paymentTrx.IsNullThrowException("Unable to unbox paymentTrx");


            if (UserBiz.IsAdmin(UserId))
            {
                paymentTrx.SelectListPeopleFrom = PersonBiz.SelectList();
                paymentTrx.SelectListPeopleTo = PersonBiz.SelectList();

            }
            else
            {
                paymentTrx.SelectListPeopleFrom = PersonBiz.SelectListWithoutPersonFor(UserId);
                paymentTrx.SelectListPeopleTo = PersonBiz.SelectListWithoutPersonFor(UserId);
            }
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }



        //[HttpPost]
        //public ActionResult MakeRefundablePayment(CashPaymentModel cashPaymentModel)
        //{
        //    try
        //    {
        //        UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
        //        BankBiz.Add_RefundablePayment(cashPaymentModel);
        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
        //    }
        //    return RedirectToAction("Index");

        //}




        //const string NON_REFUNDABLE = "Non Refundable";
        //const string REFUNDABLE = "Refundable";

        public ActionResult MakeNonRefundablePayment(CashPaymentModel cashPaymentModel)
        {
            try
            {
                cashPaymentModel = SuperBiz.Setup_CashPaymentModel(cashPaymentModel, CashTypeENUM.NonRefundable);
                return View("MakePayment", cashPaymentModel);

            }
            catch (Exception e)
            {

                return throwError(cashPaymentModel, e);

                //ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), e);
                //ErrorsGlobal.MemorySave();
                //if (cashPaymentModel.IsNull())
                //    return RedirectToAction("Index");

                //switch (cashPaymentModel.CashTypeEnum)
                //{
                //    case CashTypeENUM.NonRefundable:
                //        RedirectToAction("MakeNonRefundablePayment", cashPaymentModel);
                //        break;
                //    case CashTypeENUM.Refundable:
                //        RedirectToAction("MakeRefundablePayment", cashPaymentModel);
                //        break;
                //    default:
                //        break;
                //}

            }
        }

        public ActionResult MakeRefundablePayment(CashPaymentModel cashPaymentModel)
        {
            try
            {
                cashPaymentModel = SuperBiz.Setup_CashPaymentModel(cashPaymentModel, CashTypeENUM.Refundable);
                return View("MakePayment", cashPaymentModel);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }




        public ActionResult YesOrNo_MakeRefundablePayment(CashPaymentModel cashPaymentModel)
        {
            try
            {
                UserId.IsNullOrWhiteSpaceThrowException("User is not logged in!");

                if (UserBiz.IsAdmin(UserId))
                {
                    cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectList();

                }
                else
                {
                    cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectListWithoutPersonFor(UserId);
                }

                cashPaymentModel.SelfErrorCheck();
                checkIfUserHasAvailableFunds(cashPaymentModel);
                return View(cashPaymentModel);


            }
            catch (Exception e)
            {
                return throwError(cashPaymentModel, e);
            }

        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YesOrNo_MakeRefundablePayment(CashPaymentModel cashPaymentModel, string action)
        {
            try
            {
                if (action.ToLower() == "yes")
                {
                    UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");

                    Person person = UserBiz.GetPersonFor(UserId);
                    person.IsNullThrowException();

                    //payment is being made by the current user.
                    cashPaymentModel.PersonFromId = person.Id;
                    cashPaymentModel.SelfErrorCheck();
                    SuperBiz.Make_Payment(cashPaymentModel);

                }
            }
            catch (Exception e)
            {

                //   ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
                return throwError(cashPaymentModel, e);

            }
            return RedirectToAction("Index");
        }

        private void checkIfUserHasAvailableFunds(CashPaymentModel cashPaymentModel)
        {
            //check if User has money
            string userIdOfPayee = UserId;
            //check if payee is a bank
            bool isBank = BankBiz.IsBanker_User(userIdOfPayee);
            
            decimal currBalance = 0;
            if (!SuperBiz.HasAvailableBalancePerson(cashPaymentModel, userIdOfPayee, isBank, out currBalance))
            {
                string err = string.Format("Insufficent funds. Your balance is {0:n2} and your payment amount of '{1:n2} is greater!'",
                    currBalance,
                    cashPaymentModel.Amount);
                throw new Exception(err);
            }
        }
        private ActionResult throwError(CashPaymentModel cashPaymentModel, Exception e)
        {
            ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), e);
            ErrorsGlobal.MemorySave();
            if (cashPaymentModel.IsNull())
                return RedirectToAction("Index");

            switch (cashPaymentModel.CashTypeEnum)
            {
                case CashTypeENUM.NonRefundable:
                    RedirectToAction("MakeNonRefundablePayment", cashPaymentModel);
                    break;
                case CashTypeENUM.Refundable:
                    RedirectToAction("MakeRefundablePayment", cashPaymentModel);
                    break;
                default:
                    break;
            }
            return RedirectToAction("Index");
        }

        //private CashPaymentModel makePayment(CashPaymentModel cashPaymentModel, CashTypeENUM CashTypeEnum)
        //{
        //    //if (cashPaymentModel.IsNull())
        //    //    cashPaymentModel = new CashPaymentModel();

        //    //if (UserBiz.IsAdmin(UserId))
        //    //{
        //    //    cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectList();

        //    //}
        //    //else
        //    //{
        //    //    cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectListWithoutPersonFor(UserId);
        //    //}

        //    ////cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectListWithoutPersonFor(UserId);
        //    //cashPaymentModel.CashTypeEnum = CashTypeEnum;
        //    //return cashPaymentModel;

        //    return SuperCashBiz.
        //}


        #region Person Cash Reports

        public ActionResult DebitCredit_Refundable_Available()
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(CashTypeENUM.Refundable, CashStateENUM.Available, toDate, false);
        }


        public ActionResult DebitCredit_Refundable_Allocated()
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(CashTypeENUM.Refundable, CashStateENUM.Allocated, toDate, false);
        }

        public ActionResult DebitCredit_Refundable_All()
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(CashTypeENUM.Refundable, CashStateENUM.All, toDate, false);
        }


        public ActionResult DebitCredit_NonRefundable_Available()
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(CashTypeENUM.NonRefundable, CashStateENUM.Available, toDate, false);
        }

        public ActionResult DebitCredit_NonRefundable_Allocated()
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(CashTypeENUM.NonRefundable, CashStateENUM.Allocated, toDate, false);
        }

        public ActionResult DebitCredit_NonRefundable_All()
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(CashTypeENUM.NonRefundable, CashStateENUM.All, toDate, false);
        }


        public ActionResult DebitCredit_Total_Refundable_And_NonRefundable_Available()
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(CashTypeENUM.Total, CashStateENUM.Available, toDate, false);

        }

        public ActionResult DebitCredit_Total_Refundable_And_NonRefundable_Allocated()
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(CashTypeENUM.Total, CashStateENUM.Allocated, toDate, false);

        }

        public ActionResult DebitCredit_Total_Refundable_And_NonRefundable_All()
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(CashTypeENUM.Total, CashStateENUM.All, toDate, false);

        }


        #endregion



        #region Admin Cash Reports
        public ActionResult DebitCredit_Refundable_Available_Admin()
        {
            try
            {
                string toDate = DateTime.Now.ToString();
                return getCashStatement(CashTypeENUM.Refundable, CashStateENUM.Available, toDate, true);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }

        public ActionResult DebitCredit_Refundable_Allocated_Admin()
        {
            try
            {
                string toDate = DateTime.Now.ToString();
                return getCashStatement(CashTypeENUM.Refundable, CashStateENUM.Allocated, toDate, true);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }

        public ActionResult DebitCredit_Refundable_All_Admin()
        {
            try
            {
                string toDate = DateTime.Now.ToString();
                return getCashStatement(CashTypeENUM.Refundable, CashStateENUM.All, toDate, true);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }



        public ActionResult DebitCredit_NonRefundable_Available_Admin()
        {
            try
            {
                string toDate = DateTime.Now.ToString();
                return getCashStatement(CashTypeENUM.NonRefundable, CashStateENUM.Available, toDate, false);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }

        public ActionResult DebitCredit_NonRefundable_Allocated_Admin()
        {
            try
            {
                string toDate = DateTime.Now.ToString();
                return getCashStatement(CashTypeENUM.NonRefundable, CashStateENUM.Allocated, toDate, false);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }


        public ActionResult DebitCredit_NonRefundable_All_Admin()
        {
            try
            {
                string toDate = DateTime.Now.ToString();
                return getCashStatement(CashTypeENUM.NonRefundable, CashStateENUM.All, toDate, false);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }





        public ActionResult DebitCredit_NonRefundable_And_Refundable_Allocated_Admin()
        {
            try
            {
                string toDate = DateTime.Now.ToString();
                return getCashStatement(CashTypeENUM.Total, CashStateENUM.Allocated, toDate, false);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }

        public ActionResult DebitCredit_NonRefundable_And_Refundable_Available_Admin()
        {
            try
            {
                string toDate = DateTime.Now.ToString();
                return getCashStatement(CashTypeENUM.Total, CashStateENUM.Available, toDate, false);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }

        public ActionResult DebitCredit_NonRefundable_And_Refundable_All_Admin()
        {
            try
            {
                string toDate = DateTime.Now.ToString();
                return getCashStatement(CashTypeENUM.Total, CashStateENUM.All, toDate, false);
            }
            catch (Exception ex)
            {
                return throwError(ex);
            }
        }

        #endregion

        //private ActionResult getCashStatemetView(CashTypeENUM cashTypeEnum)
        //{
        //    string toDate = DateTime.Now.ToString();
        //    return getCashStatement(cashTypeEnum, toDate, false);
        //}

        //private ActionResult getCashListAdmin(CashTypeENUM cashTypeEnum)
        //{
        //    DateTime toDate = DateTime.Now;
        //    return getCashStatement(cashTypeEnum, toDate, true);
        //}


        public ActionResult getCashStatement(CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, string toDateStr, bool isShowAdminReports)
        {
            CashTrxDbCrModel cashTrxDbCrModel = SuperBiz.GetCashStatement(cashTypeEnum, cashStateEnum, toDateStr, isShowAdminReports);
            return View("DebitCredit", cashTrxDbCrModel);

        }





        [HttpGet]
        public ActionResult ShowCashMenu()
        {
            //UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.UserMoneyAccount();
            GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();

            return View(globalObject);
        }


        [HttpGet]
        public ActionResult ShowCashMenu_Admin()
        {
            try
            {
                GlobalObject globalObj = ViewBag.GlobalObject as GlobalObject ?? new GlobalObject();
                if (!globalObj.IsAdmin)
                    throw new Exception("You are not an administrator");
                return View(globalObj);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index");

            }
        }

        public CashTrxDbCrModel cashTrxDbCrModel { get; set; }


        #region Encashment

        public ActionResult Get_Cash_From_EncashmentRequest()
        {
            try
            {
                if (UserId.IsNullOrEmpty())
                    throw new Exception("You must be logged in.");

                if (!UserBiz.IsAdmin(UserId))
                    throw new Exception("You must be an Admin to continue.");

                Encashment_ForGettingCash encashment = new Encashment_ForGettingCash();
                //                encashment.SelectListPersonRecivingPayment = PersonBiz.SelectList();
                encashment.SelectListCashEncashmentTrx = CashEncashmentTrxBiz.SelectList_Only_Unpaid_Trx();
                return View(encashment);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                return RedirectToAction("index", "menus");

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Get_Cash_From_EncashmentRequest(Encashment_ForGettingCash encashment)
        {
            try
            {
                if (UserId.IsNullOrEmpty())
                    throw new Exception("You must be logged in.");

                if (!UserBiz.IsAdmin(UserId))
                    throw new Exception("You must be an Admin to continue.");

                encashment.SelfCheck();
                //get the record and check the code.
                CashEncashmentTrx cashEncashmentTrx = CashEncashmentTrxBiz.Find(encashment.CashEncashmentTrxId);
                cashEncashmentTrx.IsNullThrowException("Cash Trx not found");

                cashEncashmentTrx.NoOfTriesToEnterSecretNumber += 1;
                if (cashEncashmentTrx.SecretNumber.Trim() != encashment.Code.Trim())
                {
                    CashEncashmentTrxBiz.UpdateAndSave(cashEncashmentTrx);
                    throw new Exception("The secret code does not match!");

                }
                cashEncashmentTrx.PersonRequestingPaymentId.IsNullOrWhiteSpaceThrowException("Person Requesting payment is missing");
                decimal currentBalance_Refundable = SuperBiz.BalanceFor_Person(cashEncashmentTrx.PersonRequestingPaymentId, CashTypeENUM.Refundable);

                cashEncashmentTrx.CurrentBalance_Refundable = currentBalance_Refundable;
                cashEncashmentTrx.SecretNumberEntered.Value = encashment.Code.Trim();
                cashEncashmentTrx.SecretNumberEntered.SetToTodaysDate(UserName, UserId);
                cashEncashmentTrx.ReceiversIdentificationCardNumber = encashment.IdentificationCardNumber;
                CashEncashmentTrxBiz.UpdateAndSave(cashEncashmentTrx);

                return View("Encashment_Code_Accepted", cashEncashmentTrx);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                return RedirectToAction("index", "menus");

            }

        }


        public ActionResult EncashmentRequest()
        {

            try
            {
                if (UserId.IsNullOrEmpty())
                    throw new Exception("You must be logged in.");

                decimal currentBalance_Refundable = SuperBiz.BalanceFor_User(UserId, CashTypeENUM.Refundable);

                if (currentBalance_Refundable <= 0)
                    throw new Exception(string.Format("You have no money to encash. Your balance is {0}.", currentBalance_Refundable));

                string systemMessageToApplicant = "Please select a method of payment.";
                SelectList selectListPaymentType = PaymentMethodBiz.SelectList();
                CashEncashmentTrx cashEncashmentTrx = new CashEncashmentTrx(currentBalance_Refundable, systemMessageToApplicant, selectListPaymentType);
                return View(cashEncashmentTrx);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                return RedirectToAction("index", "menus");

            }

        }

        [HttpPost]
        public ActionResult EncashmentRequest(CashEncashmentTrx cashEncashmentTrx, string button)
        {

            try
            {
                if (UserId.IsNullOrEmpty())
                    throw new Exception("You must be logged in.");

                switch (button)
                {
                    case "accept":
                        SuperBiz.PrepareEncashmentForUser(UserId, UserName, cashEncashmentTrx);
                        ErrorsGlobal.AddMessage("Encashment certificate prepared. Go to Money - List Encashment");
                        return RedirectToAction("index", "menus");

                    default:
                        ErrorsGlobal.AddMessage("Encashment request canceled.");
                        return RedirectToAction("EncashmentRequest");
                }
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), ex);
                return RedirectToAction("index", "menus");

            }
        }

        #endregion

    }
}