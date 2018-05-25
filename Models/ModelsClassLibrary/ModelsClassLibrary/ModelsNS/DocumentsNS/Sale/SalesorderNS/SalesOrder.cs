using System;
using System.Collections.Generic;
using System.Linq;
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{

    /// <summary>
    /// When using the SaleOrderTrx, do not use the SalesOrderTrx virual property because it also contains deleted transactions. Instead, use
    /// SalesOrderTrxsLive.
    /// The issuer or the SO, the Owner, will have their name stored from the db called the owner.
    /// The customer will have their name stored from the customer.
    /// The Ship to will be the addresses of the customer.
    /// the Inform to will also be one of the address of the customer.
    /// </summary>
    public class SalesOrder : AbstractSaleDocumentHeader
    {


        #region Constructors

        public SalesOrder()
        {
            SaleTypeENUM = SaleTypeEnum.Sale;

            Date = DateTime.Now;
            TotalPaymentAmount = new CounterClass();
            TotalPaymentAmount.Calculator(Calculate_TotalPaymentAmount);

            TotalPaymentsAmountUnused = new CounterClass();
            TotalPaymentsAmountUnused.Calculator(Calculate_TotalPaymentsAmountUnused);
        }


        #endregion


        #region Properties
        /// <summary>
        /// This is the next consecutive SO number
        /// </summary>
        public long SoNumber { get; set; }


        #endregion

        /// <summary>
        /// This is an override and returns full details of the SaleOrder
        /// </summary>
        public override string ToString()
        {
            string s = string.Format("#: {0} dated: {1:yyyy-MMM-dd hh:mm:ss tt} From: {2} To: {3} Ordered: {4:C}",
            Id,
            Date,
            Owner.FullName(),
            ConsignTo.FullName(),
            TotalDoc_Ordered_MoneyAmount.Amount);

            return s;
        }

        #region CounterClass
        /// <summary>
        /// This holds the money amount value of the total payments applied to SalesOrder.
        /// </summary>
        public CounterClass TotalPaymentAmount { get; set; }
        public CounterClass TotalPaymentsAmountUnused { get; set; }
        public CounterClass TotalPaymentsAmountUsed { get; set; }

        #endregion

        #region CounterClass Calculators
        /// <summary>
        /// This returns the total doc value in money terms for ordered amount.
        /// </summary>
        /// <returns></returns>
        protected override decimal Calculator_TotalDoc_Ordered_MoneyAmount()
        {
            decimal amount = TotalMiscCharges.Amount + TotalItems_Ordered_MoneyAmount.Amount;
            return amount;
        }

        /// <summary>
        /// This returns the total doc value for ship amounts
        /// </summary>
        /// <returns></returns>
        protected override decimal Calculator_TotalDoc_Shipped_MoneyAmount()
        {
            decimal amount = TotalMiscCharges.Amount + TotalItems_Ship_MoneyAmount.Amount;
            return amount;
        }

        /// <summary>
        /// This returns the total value of ordered items only. It leaves out the misc costs.
        /// </summary>
        /// <returns></returns>
        protected override decimal Calculator_TotalItems_Ordered_MoneyAmount()
        {

            decimal amount = 0.00M;

            if (((ICollection<object>)SalesOrderTrxs).IsNullOrEmpty())
                return amount;

            var soTrxs = SalesOrderTrxs.ToList();

            foreach (var trx in soTrxs)
            {
                amount += trx.LineTotal_Money_Ordered.Amount;
            }

            return amount;

        }


        /// <summary>
        /// This returns the total value of shipped items only. It leaves out the misc costs.
        /// </summary>
        /// <returns></returns>
        protected override decimal Calculator_TotalItems_Ship_MoneyAmount()
        {
            var soTrxs = SalesOrderTrxs.ToList();
            decimal amount = 0.00M;

            if (((ICollection<object>)soTrxs).IsNullOrEmpty())
                return amount;

            foreach (var trx in soTrxs)
            {
                amount += trx.LineTotal_Money_Ship.Amount;
            }

            return amount;
        }


        /// <summary>
        /// This returns the total amount of payments applied.
        /// </summary>
        /// <returns></returns>
        protected decimal Calculate_TotalPaymentAmount()
        {
            var lstPaymentAppliedSalesOrder = PaymentsApplied.ToList();

            decimal ttlPayments = 0.00M;

            if (((ICollection<object>)lstPaymentAppliedSalesOrder).IsNullOrEmpty())
                return ttlPayments;

            foreach (var paymentApplied in lstPaymentAppliedSalesOrder)
            {
                ttlPayments += paymentApplied.Amount;
            }

            return ttlPayments;
        }

        /// <summary>
        /// This is the total payment amount left unused.
        /// </summary>
        /// <returns></returns>
        protected decimal Calculate_TotalPaymentsAmountUnused()
        {
            return TotalPaymentAmount.Amount - TotalPaymentsAmountUsed.Amount;
        }

        /// <summary>
        /// This returns the total amount of payment that has been used to pay invoices.
        /// This amount less the total amount would then be the unused amount.
        /// </summary>
        /// <returns></returns>
        protected decimal Calculate_TotalPaymentsAmountUsed()
        {
            //get payment amounts transferred to invoices
            decimal ttlApplied = 0.00M;

            if (((ICollection<object>)PaymentsApplied).IsNullOrEmpty())
                return ttlApplied;

            var paymentsApplied = PaymentsApplied.ToList();

            foreach (var paymentInSo in paymentsApplied)
            {
                if (paymentInSo.AmountAppliedToInvoices.Amount == 0)
                    continue;
                ttlApplied += paymentInSo.AmountAppliedToInvoices.Amount;
            }

            return ttlApplied;

        }

        #endregion

        #region SelfErrorCheck

        /// <summary>
        /// This does a list of error checks for all the properties.
        /// </summary>
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            Check_SaleOrder_Cannot_Be_Unknown_Type();
        }


        #region SelfErrorCheck Checks


        /// <summary>
        /// Ensures that the sales order is not unknown type
        /// </summary>
        private void Check_SaleOrder_Cannot_Be_Unknown_Type()
        {
            if (SaleTypeENUM == SaleTypeEnum.Unknown)
            {
                throw new Exception("Sales order type is UNKNOW");
            }
        }

        #endregion

        #endregion

        #region Is...

        /// <summary>
        /// Not valid if SaleType is Unknown or a quotation.
        /// </summary>
        public bool IsValidSaleDocument
        {
            get
            {
                if (SaleTypeENUM == SaleTypeEnum.Unknown || SaleTypeENUM == SaleTypeEnum.Quotation)
                    return false;
                else
                    return true;

            }
        }
        #endregion



        public void LoadFrom(SalesOrder s)
        {
            LoadFrom(s as AbstractSaleDocumentHeader);

            Invoices = s.Invoices;
            SalesOrderTrxs = s.SalesOrderTrxs;
            SoNumber = s.SoNumber;
            //Navigations...
            PaymentsApplied = s.PaymentsApplied;
            SalesOrderTrxs = s.SalesOrderTrxs;
            Invoices = s.Invoices;
        }


        #region Navigations

        /// <summary>
        /// These are the payments that have been applied to this sales order
        /// </summary>
        public virtual ICollection<PaymentAppliedSalesOrder> PaymentsApplied { get; set; }

        /// <summary>
        /// These are the sales orders trasnsaction list.
        /// </summary>
        public virtual ICollection<SalesOrderTrx> SalesOrderTrxs { get; set; }
        /// <summary>
        /// These are all the invoices created by this sales order
        /// </summary>
        public virtual ICollection<Invoice> Invoices { get; set; }


        #endregion
    }
}