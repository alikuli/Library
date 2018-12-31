using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.PaymentsNS
{
    /// <summary>
    /// This record holds the total paid amount applied to an invoice for a single payment from a salesorder to the invoice
    /// This can never be greater than the orignal payment amount.
    /// </summary>
    public class PaymentAppliedAbstract:AbstractDocHeader
    {


        #region Payment
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
        
        #endregion

        #region Properties
        /// <summary>
        /// This is the amount of payment being applied to this invoice/salesorder. It can be less than or equal to the
        /// total payment amount.
        /// </summary>
        public decimal Amount { get; set; }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            Check_Amount(); //Allowing negative amounts.
            Check_Payment();
        }
        
        #endregion
        #region SelfErrorCheck
        private void Check_Payment()
        {
            if (Payment.IsNull())
                throw new Exception(string.Format("No payment for '{0}'. PaymentAppliedAbstract.Check_Payment", this.ToString()));

            if (PaymentId.IsNullOrEmpty())
                throw new Exception(string.Format("No payment Id for {0}. PaymentAppliedAbstract.Check_Payment", this.ToString()));

        }


        private void Check_Amount()
        {
            if (Amount == 0)
                throw new Exception(string.Format("Payment Amount is zero for {0}. PaymentAppliedAbstract.Check_Amount", this.ToString()));

            if(Amount>Payment.Amount)
                throw new Exception(string.Format("Amount being applied is greater than the original amount of payment. {0}. PaymentAppliedAbstract.Check_Amount", this.ToString()));


        }
        
        #endregion

        #region LoadFrom
        public void LoadFrom(PaymentAppliedAbstract a)
        {
            LoadFrom(a as AbstractDocHeader);
            Amount = a.Amount;
            Payment = a.Payment;
            PaymentId = a.PaymentId;
        } 
        #endregion

    }
}