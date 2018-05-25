using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{
    /// <summary>
    /// This represents the Invoice Header.
    /// </summary>
    public class Invoice : AbstractSaleDocumentHeader
    {
        #region Constructors
        public Invoice()
        {

            TotalPaymentApplied = new CounterClass();
            TotalDoc_Ordered_MoneyAmount.Calculator(Calculator_TotalPaymentApplied);

            TotalUnappliedAmount = new CounterClass();
            TotalUnappliedAmount.Calculator(Calculator_TotalUnappliedAmount);
        }


        #endregion

        #region Properties

        /// <summary>
        /// This is the date of the document... not sure what this is doing anymore...
        /// </summary>
        //[Column(TypeName = "DateTime2")]
        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date and Number")]
        public string OrderDateAndNumber { get; set; }

        
        /// <summary>
        /// This is the alias number. Initially it is set up as the ID number. Later it can be changed by the user.
        /// </summary>
        [Display(Name = "No")]
        [MaxLength(1000,ErrorMessage="The maximum length of {0} is {1}")]
        public string DocAliasID { get; set; }

        
        [Display(Name = "Has Alias")]
        [NotMapped]
        public bool HasAlias { get; set; }

        #endregion

        #region Navigation Properties

        #region SalesOrders


        /// <summary>
        /// You can have more than one sales order contributing to an invoice.
        /// </summary>
        [Display(Name = "Sales Order")]
        //public Guid? SalesOrderId { get; set; }
        public virtual ICollection<SalesOrder> SalesOrders { get; set; }

        #endregion
        public virtual ICollection<PaymentAppliedInvoice> PaymentsApplied { get; set; }
        public virtual ICollection<InvoiceTrx> InvoiceTrxs { get; set; }

        
        #endregion

        #region CounterClass
        /// <summary>
        /// This is the total payment amount applied to Invoice
        /// </summary>
        public CounterClass TotalPaymentApplied { get; set; }

        /// <summary>
        /// This represents the total invoice amount to which payment has NOT
        /// been applied to.
        /// </summary>
        public CounterClass TotalUnappliedAmount { get; set; }


        #region CounterClass Calculators
        private decimal Calculator_TotalPaymentApplied()
        {
            decimal ttlPayment = 0.00M;

            if (((ICollection<object>) PaymentsApplied).IsNullOrEmpty())
                return ttlPayment;

            var paymentsApplied = PaymentsApplied.ToList();
            foreach (var payment in paymentsApplied)
            {
                ttlPayment += payment.Amount;
            }

            return ttlPayment;
        }


        private decimal Calculator_TotalUnappliedAmount()
        {
            return TotalDoc_Shipped_MoneyAmount.Amount - TotalPaymentApplied.Amount;
        }
        protected override decimal Calculator_TotalDoc_Ordered_MoneyAmount()
        {
            return base.Calculator_TotalDoc_Ordered_MoneyAmount();
        }

        protected override decimal Calculator_TotalDoc_Shipped_MoneyAmount()
        {
            return base.Calculator_TotalDoc_Shipped_MoneyAmount();
        }

        protected override decimal Calculator_TotalItems_Ordered_MoneyAmount()
        {
            return base.Calculator_TotalItems_Ordered_MoneyAmount();
        }

        protected override decimal Calculator_TotalItems_Ship_MoneyAmount()
        {
            return base.Calculator_TotalItems_Ship_MoneyAmount();
        }

        #endregion
        #endregion
        
        #region Overrides

        public override string FullName()
        {
            return this.ToString();
        }

        public override string ToString()
        {

            string s = string.Format("{0} {1} From: {2} To: {3} Amount: {4}",
            DocAliasID,
            Date,
            Owner.FullName(),
            ConsignTo.FullName(),
            TotalDoc_Ordered_MoneyAmount.Amount);

            return s;
        }

        #endregion

        #region SelfErrorCheck
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

            Check_DocAliasID_Not_Empty();
            Check_OrderDateAndNumber();

        }

        #region SelfErrorCheck Checks

        private void Check_DocAliasID_Not_Empty()
        {
            if (DocAliasID.IsNullOrEmpty())
                throw new Exception("The Alias Doc number for the invoice is empty. Invoice.Check_DocAliasID_Not_Empty");
        }

        private void Check_OrderDateAndNumber()
        {
            //No checks required.
        }

        #endregion
        
        #endregion

        #region Is...
        //public bool IsSalesOrderNull
        //{
        //    get
        //    {
        //        return SalesOrder == null;
        //    }
        //}
        //public bool IsSalesOrderIdNullOrEmpty
        //{
        //    get
        //    {
        //        return GuidHelper.IsNullOrEmpty(SalesOrderId);
        //    }
        //}
        
        //public bool IsSalesOrderAbsent
        //{
        //    get
        //    {
        //        return IsSalesOrdersNullOrEmpty;
        //    }
        //}

        //public bool IsSalesOrdersNullOrEmpty
        //{
        //    get
        //    {
        //        return SalesOrders == null || SalesOrders.Count() == 0;
        //    }
        //}

        #endregion






        #region Loaders
        
        public void LoadFor(Invoice i)
        {
            LoadFrom(i as AbstractSaleDocumentHeader);

            InvoiceTrxs = i.InvoiceTrxs;
            SalesOrders = i.SalesOrders;
            PaymentsApplied = i.PaymentsApplied;

        }


        #endregion

    }
}