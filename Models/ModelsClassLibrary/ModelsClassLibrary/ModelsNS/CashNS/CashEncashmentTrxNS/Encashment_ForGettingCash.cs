using AliKuli.Extentions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.CashNS.CashEncashmentTrxNS
{
    [NotMapped]
    public class Encashment_ForGettingCash
    {
        //public string PersonRecivingPaymentId { get; set; }
        //public Person PersonRecivingPayment { get; set; }
        //public SelectList SelectListPersonRecivingPayment { get; set; }


        [Display(Name = "Cash Trx")]
        public string CashEncashmentTrxId { get; set; }
        public CashEncashmentTrx CashEncashmentTrx { get; set; }
        public SelectList SelectListCashEncashmentTrx { get; set; }

        [Display(Name = "Identification Card #")]
        public string IdentificationCardNumber { get; set; }

        //public decimal Amount { get; set; }
        public string Code { get; set; }


        public void SelfCheck()
        {
            CashEncashmentTrxId.IsNullOrWhiteSpaceThrowArgumentException("No Encashment Number entered.");
            IdentificationCardNumber.IsNullOrWhiteSpaceThrowArgumentException("No Identification Card number entered.");
            CashEncashmentTrxId.IsNullOrWhiteSpaceThrowArgumentException("No Code entered.");

        }
    }
}
