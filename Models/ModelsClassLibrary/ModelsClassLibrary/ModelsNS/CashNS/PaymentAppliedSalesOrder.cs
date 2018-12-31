using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS
{
    /// <summary>
    /// One record will be created per applied payment per sales order. Note, more than
    /// one payment can be applied to a sales order
    /// 
    /// </summary>
    public class PaymentAppliedSalesOrder: PaymentAppliedAbstract
    {
        public PaymentAppliedSalesOrder()
        {
            AmountAppliedToInvoices = new CounterClass();
            AmountAppliedToInvoices.Calculator(Calculator_AmountAppliedToInvoices);
        }


        #region SalesOrder
        public Guid SalesOrderId { get; set; }
        public virtual SalesOrder SalesOrder { get; set; }
        
        #endregion

        #region CounterClass
        /// <summary>
        /// This is the amount of payment which has been applied 
        /// invoices. Sometimes, one saleorder will make 2 invoices
        /// and so the payments may be spread over several invoices.
        /// Therefore, this is different from 'Amount' which only is
        /// the amount that has been applied to the sale order
        /// </summary>
        public CounterClass AmountAppliedToInvoices { get; set; }

        private decimal Calculator_AmountAppliedToInvoices()
        {
            decimal amnt = 0.00M;

            if (PaymentsAppliedToInvoices.IsNullOrEmpty())
                return amnt;

            var paymentsAppliedToInvoices = PaymentsAppliedToInvoices.ToList();

            foreach (var payment in paymentsAppliedToInvoices)
            {
                amnt += payment.Amount;
            }

            return amnt;
        }
        
        #endregion        

        public override string MakeUniqueName()
        {
            Name = PaymentId.ToString() + SalesOrderId.ToString();
            return Name;

        }
        /// <summary>
        /// This holds all the invoices that this payment has been applied to
        /// </summary>
        public virtual ICollection<PaymentAppliedInvoice> PaymentsAppliedToInvoices { get; set; }


        #region Loaders
        public void LoadFrom(PaymentAppliedSalesOrder p)
        {
            LoadFrom(p as PaymentAppliedAbstract);
            SalesOrder = p.SalesOrder;
            SalesOrderId = p.SalesOrderId;
            PaymentsAppliedToInvoices = p.PaymentsAppliedToInvoices;
        }
        
        #endregion
    }
}