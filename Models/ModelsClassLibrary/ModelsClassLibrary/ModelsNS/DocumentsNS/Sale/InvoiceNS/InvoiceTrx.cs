using AliKuli.UtilitiesNS;
using AliKuli.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.SaleNS
{
    /// <summary>
    /// This is a single invoice transaction. A product is only allowed to be shown once in an invoice.
    /// </summary>
    public class InvoiceTrx : AbstractDocumentTrx
    {

        #region Invoice

        /// <summary>
        /// This is the invoice to whom the transaction belongs to.
        /// </summary>
        public Guid InvoiceID { get; set; }
        public Invoice Invoice { get; set; }
        
        #endregion

        #region Overrides

        /// <summary>
        /// This returns the total invoice transaction information.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string message = string.Format("{0} Ordered: {1}, Price: {2} with Discount {3:p2}",
                Product.Name,
                OrderedQty,
                ListedPrice,
                DiscountPct);
            return message;
        }


        /// <summary>
        /// This makes a new name for the Invoice Trx - InvoiceId + ProductId. 
        /// A product is only allowed to show once in an invoice.
        /// </summary>
        /// <returns></returns>
        public override string MakeUniqueName()
        {
            return string.Format("{0}{1}", InvoiceID,ProductID);
        }
        
        #endregion

        #region SelfErrorCheck

        /// <summary>
        /// Checks InvoiceId and InvoiceHeader, if they have values.
        /// </summary>
        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

            Check_InvoiceId();
            Check_Invoice_Header();

        }

        #region SelfErrorCheck helpers

        private void Check_Invoice_Header()
        {
            if (Invoice.IsNull())
            {
                throw new Exception("No Invoice passed. Error. 2.InvoiceTrx.Check_Invoice ");
            }
        }

        private void Check_InvoiceId()
        {
            if (InvoiceID.IsNullOrEmpty())
            {
                throw new Exception("No Invoice passed. Error. 1.InvoiceTrx.Check_InvoiceId ");
            }
        }


        #endregion        
        
        #endregion
        

        /// <summary>
        /// This is the list of SalesOrder transactions.
        /// </summary>
        public virtual ICollection<SalesOrderTrx> SalesOrderTrxs { get; set; }

        #region Loaders
        public void LoadFrom(InvoiceTrx i)
        {
            LoadFrom(i as AbstractDocumentTrx);

            Invoice = i.Invoice;
            InvoiceID = i.InvoiceID;
            SalesOrderTrxs = i.SalesOrderTrxs;
        }
        
        #endregion
    }
}