using AliKuli.Extentions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS
{
    public class CashPaymentModel
    {
        [NotMapped]
        public SelectList SelectListPeopleTo { get; set; }

        [Display(Name = "To")]
        public string PersonToId { get; set; }

        public decimal Amount { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Type")]
        public string RefundType { get; set; }
        public void SelfErrorCheck()
        {
            PersonToId.IsNullOrWhiteSpaceThrowException("PersonTo");
            RefundType.IsNullOrWhiteSpaceThrowException("RefundType");
            if (Amount == 0)
                throw new Exception("Amount is Zero!");
        }

    }
}
