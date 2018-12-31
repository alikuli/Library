using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.CashNS
{
    public class CashTrxAbstract : CommonWithId
    {
        public CashTrxAbstract()
        {
        }

        [Display(Name = "Doc #")]
        public long DocNumber { get; set; }
        [Display(Name = "From")]
        public string PersonFromId { get; set; }
        [Display(Name = "From")]
        public virtual Person PersonFrom { get; set; }

        [Display(Name = "To")]
        public string PersonToId { get; set; }
        [Display(Name = "To")]
        public virtual Person PersonTo { get; set; }



        public decimal Amount { get; set; }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

            PersonFromId.IsNullOrWhiteSpaceThrowException("PersonId");
            PersonToId.IsNullOrWhiteSpaceThrowException("You need to select who you are paying");
            PersonTo.IsNullThrowException("Person To is Null");
        }

        [Display(Name = "Cash Type")]
        public CashTypeENUM CashTypeEnum { get; set; }


        [NotMapped]
        public SelectList SelectListCashTypeEnum { get { return EnumExtention.ToSelectListSorted<CashTypeENUM>(CashTypeENUM.Unknown); } }


        [NotMapped]
        public SelectList SelectListPeopleFrom { get; set; }

        [NotMapped]
        public SelectList SelectListPeopleTo { get; set; }


        //public VerificaionStatusENUM VerificationStatusEnum { get; set; }


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            CashTrxAbstract pta = icommonWithId as CashTrxAbstract;
            PersonFromId = pta.PersonFromId;
            PersonToId = pta.PersonToId;
            //DateComplex = pta.DateComplex;
            Amount = pta.Amount;
            //DocNumber = pta.DocNumber;
            CashTypeEnum = pta.CashTypeEnum;

        }


    }
}
