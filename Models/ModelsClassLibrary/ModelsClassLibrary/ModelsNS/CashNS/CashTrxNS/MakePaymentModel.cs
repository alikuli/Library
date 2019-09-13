using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.CashNS.CashTrxNS
{
    public class MakePaymentModel
    {
        public MakePaymentModel()
        {

        }
        public MakePaymentModel(string personFromId, string personToId, decimal amount, string comment, CashTypeENUM cashTypeEnum)
            : this()
        {
            PersonFromId = personFromId;
            PersonToId = personToId;
            Amount = amount;
            Comment = comment;
            CashTypeEnum = cashTypeEnum;
        }


        [Display(Name = "To")]
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
            if (CashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is not known!");

            if (Amount == 0)
                throw new Exception("Amount is Zero!");
        }

    }
}
