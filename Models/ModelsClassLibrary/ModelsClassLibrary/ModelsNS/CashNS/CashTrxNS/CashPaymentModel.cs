using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS
{
    [NotMapped]
    public class CashPaymentModel
    {
        public CashPaymentModel()
        {

        }

        /// <summary>
        /// these are the fields required to make a payment
        /// </summary>
        /// <param name="personToId"></param>
        /// <param name="amount"></param>
        /// <param name="comment"></param>




        public SelectList SelectListPeopleTo { get; set; }

        [NotMapped]
        public SelectList SelectListCashTypeEnum { get { return AliKuli.Extentions.EnumExtention.ToSelectListSorted<CashTypeENUM>(CashTypeENUM.Unknown); } }



        [Display(Name = "From")]
        public string PersonFromId { get; set; }

        [Display(Name = "To")]
        public string PersonToId { get; set; }

        public decimal Amount { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Type")]
        public CashTypeENUM CashTypeEnum { get; set; }
        public void SelfErrorCheck()
        {
            PersonToId.IsNullOrWhiteSpaceThrowException("PersonTo");
            //when cash is being created, the PersonFrom is null... so...
            //PersonFromId.IsNullOrWhiteSpaceThrowException("PersonFrom");

            if (CashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is not known!");

            if (Amount == 0)
                throw new Exception("Amount is Zero!");
        }

    }
}
