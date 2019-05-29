using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.CashTtxNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.BankNS;
using UowLibrary.PlayersNS.PersonNS;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class CashTrxsController : EntityAbstractController<CashTrx>
    {

        CashTrxBiz _cashTrxsBiz;
        BankBiz _bankBiz;

        public CashTrxsController(BankBiz bankBiz, AbstractControllerParameters param)
            : base(bankBiz.CashTrxBiz, param)
        {
            _cashTrxsBiz = bankBiz.CashTrxBiz;
            _bankBiz = bankBiz;
        }

        BankBiz BankBiz
        {
            get
            {
                _bankBiz.IsNullThrowException();
                _bankBiz.UserId = UserId;
                _bankBiz.UserName = UserName;

                return _bankBiz;
            }
        }

        PersonBiz PersonBiz
        {
            get
            {
                return _cashTrxsBiz.PersonBiz;
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

                return _cashTrxsBiz;
            }
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
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
            return base.Event_CreateViewAndSetupSelectList(parm);
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
                cashPaymentModel = makePayment(cashPaymentModel, CashTypeENUM.NonRefundable);
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
            cashPaymentModel = makePayment(cashPaymentModel, CashTypeENUM.Refundable);
            return View("MakePayment", cashPaymentModel);
        }




        public ActionResult YesOrNo(CashPaymentModel cashPaymentModel)
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
        public ActionResult YesOrNo(CashPaymentModel cashPaymentModel, string action)
        {
            try
            {
                if (action.ToLower() == "yes")
                {
                    UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
                    checkIfUserHasAvailableFunds(cashPaymentModel);
                    switch (cashPaymentModel.CashTypeEnum)
                    {
                        case CashTypeENUM.NonRefundable:
                            BankBiz.Add_NON_RefundablePayment(cashPaymentModel);
                            break;

                        case CashTypeENUM.Refundable:
                            BankBiz.Add_RefundablePayment(cashPaymentModel);
                            break;

                        default:
                            throw new Exception("Cannot tell if this is refundable or not.");
                    }
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
            bool isBanker = BankBiz.IsBankerFor(UserId);
            //check if User has money
            string userIdOfPayee = UserId;
            if (!CashTrxBiz.HasAvailableBalance(cashPaymentModel, userIdOfPayee, isBanker))
            {
                string err = string.Format("Insufficent funds. Your balance is {0:n2} and your payment amount of '{1:n2} is greater!'",
                    CashTrxBiz.BalanceForPerson(cashPaymentModel),
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
        private CashPaymentModel makePayment(CashPaymentModel cashPaymentModel, CashTypeENUM CashTypeEnum)
        {
            if (cashPaymentModel.IsNull())
                cashPaymentModel = new CashPaymentModel();

            if (UserBiz.IsAdmin(UserId))
            {
                cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectList();

            }
            else
            {
                cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectListWithoutPersonFor(UserId);
            }

            //cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectListWithoutPersonFor(UserId);
            cashPaymentModel.CashTypeEnum = CashTypeEnum;
            return cashPaymentModel;
        }


        #region Person Cash Reports
        public ActionResult DebitCredit_Refundable()
        {
            CashTypeENUM cashTypeEnum = CashTypeENUM.Refundable;
            return getCashStatemetView(cashTypeEnum);
        }
        public ActionResult DebitCredit_NonRefundable()
        {
            CashTypeENUM cashTypeEnum = CashTypeENUM.NonRefundable;
            return getCashStatemetView(cashTypeEnum);
        }



        public ActionResult DebitCredit_TotalRefundableAndNonRefundable()
        {
            CashTypeENUM cashTypeEnum = CashTypeENUM.Total;
            return getCashStatemetView(cashTypeEnum);

        }

        //public ActionResult DebitCredit_NonRefundable_And_Refundable()
        //{
        //    CashTypeENUM cashTypeEnum = CashTypeENUM.Unknown;
        //    return getCashStatemetView(cashTypeEnum);
        //}


        
        #endregion
        #region Admin Cash Reports
        public ActionResult DebitCredit_Refundable_Admin()
        {
            string toDate = DateTime.Now.ToString();
            CashTypeENUM cashTypeEnum = CashTypeENUM.Refundable;
            return getCashStatement(cashTypeEnum, toDate, true);
        }


        public ActionResult DebitCredit_NonRefundable_Admin()
        {
            string toDate = DateTime.Now.ToString();
            CashTypeENUM cashTypeEnum = CashTypeENUM.NonRefundable;
            return getCashStatement(cashTypeEnum, toDate, false);
        }

        public ActionResult DebitCredit_NonRefundable_And_Refundable_Admin()
        {
            string toDate = DateTime.Now.ToString();
            CashTypeENUM cashTypeEnum = CashTypeENUM.Total;
            return getCashStatement(cashTypeEnum, toDate, false);
        }

        #endregion

        private ActionResult getCashStatemetView(CashTypeENUM cashTypeEnum)
        {
            string toDate = DateTime.Now.ToString();
            return getCashStatement(cashTypeEnum, toDate, false);
        }

        //private ActionResult getCashListAdmin(CashTypeENUM cashTypeEnum)
        //{
        //    DateTime toDate = DateTime.Now;
        //    return getCashStatement(cashTypeEnum, toDate, true);
        //}


        public ActionResult getCashStatement(CashTypeENUM cashTypeEnum, string toDateStr, bool isAdmin)
        {
            UserId.IsNullOrWhiteSpaceThrowArgumentException("You are not logged in");
            CashTrxDbCrModel cashTrxDbCrModel = new CashTrxDbCrModel();
            DateTime toDate = DateTime.Now;
            if(!toDateStr.IsNullOrWhiteSpace())
            {
                bool success = DateTime.TryParse(toDateStr, out toDate);
                if (!success)
                    toDate = DateTime.Now;
            }

            DateTime fromDate = toDate.AddMonths(-3).AddDays(-1);
            if (isAdmin)
            {
                if (Is_Admin)
                {
                    cashTrxDbCrModel = CashTrxBiz.GetCashTrxDbCrModel("", cashTypeEnum, fromDate, toDate, true);
                    return View("DebitCredit", cashTrxDbCrModel);

                }
                ErrorsGlobal.Add("You are not authourized", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }
            Person person = PersonBiz.GetPersonForUserId(UserId);
            person.IsNullThrowException("No person attached to user");
            cashTrxDbCrModel = CashTrxBiz.GetCashTrxDbCrModel(person.Id, cashTypeEnum, fromDate, toDate, false);
            return View("DebitCredit", cashTrxDbCrModel);

        }


        //private ActionResult getCashStatement(CashTypeENUM cashTypeEnum, DateTime fromDate, DateTime toDate)
        //{
        //}

        [HttpGet]
        public ActionResult ShowCashMenu()
        {
            UserMoneyAccount moneyAccount = ViewBag.MoneyAccount as UserMoneyAccount ?? new ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.UserMoneyAccount();
            return View(moneyAccount);
        }
        [HttpGet]
        public ActionResult ShowCashMenu_Admin()
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

        public CashTrxDbCrModel cashTrxDbCrModel { get; set; }
    }
}