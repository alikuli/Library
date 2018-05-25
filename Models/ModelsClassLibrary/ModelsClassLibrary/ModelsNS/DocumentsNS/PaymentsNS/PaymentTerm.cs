using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelsClassLibrary.ModelsNS.DeliveryMethodNS
{

    
    public class PaymentTerm : CommonWithId
    {

        #region Properties

        [Display(Name = "No. Of Days Credit")]
        public int NoOfDaysCredit { get; set; }


        [Display(Name = "No. Of Days Early Payment")]
        public int NoOfDaysEarlyPayment { get; set; }


        [Display(Name = "Early Payment Discount")]
        public decimal EarlyPaymentDiscount { get; set; }


        public override string FullName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("[{0}] ", Name));

            if (NoOfDaysCredit > 0)
            {
                sb.Append(string.Format("Net {0} day ", NoOfDaysCredit));
                if (NoOfDaysCredit > 1)
                {
                    sb.Length = sb.Length - 1;
                    sb.Append("s ");
                }
            }

            if (NoOfDaysEarlyPayment > 0)
            {
                if (EarlyPaymentDiscount > 0)
                    sb.Append(string.Format("Pay in {0} day ", NoOfDaysEarlyPayment));
                if (NoOfDaysEarlyPayment > 1)
                {
                    sb.Length = sb.Length - 1;
                    sb.Append("s ");
                }

                sb.Append(string.Format("and get {0:P2} Discount ", EarlyPaymentDiscount));

            }
            else
                if (EarlyPaymentDiscount > 0)
                {
                    sb.Append(string.Format("Pay Cash and get {0:P2} discount ", EarlyPaymentDiscount));

                }
            //else
            //    sb.Append(string.Format("Cash "));

            return sb.ToString();
        }

        #endregion


        public override EnumLibrary.EnumNS.ClassesWithRightsENUM ClassNameForRights()
        {
            return EnumLibrary.EnumNS.ClassesWithRightsENUM.PaymentTerm;
        }

        public override List<string> FieldsToLoadFromView()
        {
            List<string> lst = base.FieldsToLoadFromView();
            lst.Add("NoOfDaysCredit");
            lst.Add("NoOfDaysEarlyPayment");
            lst.Add("NoOfDaysEarlyPayment");

            return lst;
        }

        public override void UpdatePropertiesDuringModify(InterfacesLibrary.SharedNS.ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);

            PaymentTerm paymentTerm = icommonWithId as PaymentTerm;

            if (paymentTerm == null)
            {
                throw new Exception("Unable to box PaymentTerm. PaymentTerm.UpdatePropertiesDuringModify");
            }

            NoOfDaysCredit = paymentTerm.NoOfDaysCredit;
            NoOfDaysEarlyPayment = paymentTerm.NoOfDaysEarlyPayment;
            EarlyPaymentDiscount = paymentTerm.EarlyPaymentDiscount;

        }

    }
}