using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.CashsNS.CashTrxNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
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
            paymentTrx.SelectListPeopleFrom = PersonBiz.SelectList();
            paymentTrx.SelectListPeopleTo = PersonBiz.SelectList();

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




        const string NON_REFUNDABLE = "Non Refundable";
        const string REFUNDABLE = "Refundable";

        public ActionResult MakeNonRefundablePayment(CashPaymentModel cashPaymentModel)
        {
            if (cashPaymentModel.IsNull())
                cashPaymentModel = new CashPaymentModel();

            cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectList();
            cashPaymentModel.RefundType = NON_REFUNDABLE;
            return View("MakePayment", cashPaymentModel);
        }

        public ActionResult MakeRefundablePayment(CashPaymentModel cashPaymentModel)
        {
            if (cashPaymentModel.IsNull())
                cashPaymentModel = new CashPaymentModel();

            cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectList();
            cashPaymentModel.RefundType = REFUNDABLE;
            return View("MakePayment", cashPaymentModel);
        }

        public ActionResult YesOrNo(CashPaymentModel cashPaymentModel)
        {
            try
            {
                cashPaymentModel.SelectListPeopleTo = PersonBiz.SelectList();
                cashPaymentModel.SelfErrorCheck();
                return View(cashPaymentModel);
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong!", MethodBase.GetCurrentMethod(), e);
                if (cashPaymentModel.IsNull())
                    return RedirectToAction("Index");

                switch (cashPaymentModel.RefundType)
                {
                    case NON_REFUNDABLE:
                        RedirectToAction("MakeNonRefundablePayment", cashPaymentModel);
                        break;
                    case REFUNDABLE:
                        RedirectToAction("MakeRefundablePayment", cashPaymentModel);
                        break;
                    default:
                        break;
                }
            }
            return RedirectToAction("Index");

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
                    switch (cashPaymentModel.RefundType)
                    {
                        case NON_REFUNDABLE:
                            BankBiz.Add_NON_RefundablePayment(cashPaymentModel);
                            break;

                        case REFUNDABLE:
                            BankBiz.Add_RefundablePayment(cashPaymentModel);
                            break;

                        default:
                            throw new Exception("Cannot tell if this is refundable or not.");
                    }
                }
            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
            }
            return RedirectToAction("Index");
        }

    }
}