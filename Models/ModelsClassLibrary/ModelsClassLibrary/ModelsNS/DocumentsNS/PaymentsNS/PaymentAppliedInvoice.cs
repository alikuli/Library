using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS
{
    /// <summary>
    /// One record will be created per applied payment per invoice per sales order
    /// 
    /// </summary>
    public class PaymentAppliedInvoice: PaymentAppliedAbstract
    {


        #region Invoice
        public Invoice Invoice { get; set; }
        public Guid InvoiceId { get; set; }
        
        #endregion
        
        
        
        public void LoadFrom(PaymentAppliedInvoice p)
        {
            LoadFrom(p as PaymentAppliedAbstract);
            Invoice = p.Invoice;
            InvoiceId = p.InvoiceId;
            PaymentsAppliedFromSalesOrders = p.PaymentsAppliedFromSalesOrders;
        }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            Check_Invoice();
        }

        #region SelfErrorCheck
        private void Check_Invoice()
        {
            if (Invoice.IsNull())
            {
                throw new Exception("No Invoice. PaymentAppliedInvoice.Check_Invoice");
            }

            if (InvoiceId.IsNullOrEmpty())
            {
                throw new Exception("No Invoice Id. PaymentAppliedInvoice.Check_Invoice");
            }
        }
        
        #endregion



        public override string MakeUniqueName()
        {
            throw new NotImplementedException("PaymentAppliedInvoice.MakeUniqueName");
            //Name = PaymentId.ToString() + InvoiceId.ToString();
            //return Name;

        }


        /// <summary>
        /// This holds all the salesorders that have sent their payment amounts to this invoice
        /// </summary>
        public virtual ICollection<PaymentAppliedSalesOrder> PaymentsAppliedFromSalesOrders { get; set; }


    }

}