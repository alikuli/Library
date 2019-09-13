using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.CashNS.CashTrDistributionNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.CashNS
{
    /// <summary>
    /// There are 2 types of cash.
    ///    Refundable:     this can be refunded to the customer. All real cash is refundable
    ///    NonRefundable:  This is cash that cannot be used for purchasing goods and it cannot be encashed. It can
    ///                    only be used for making payments for program features.
    /// The cash can have 2 states.
    ///    Available: This cash is available and can be withdrawn or used.
    ///    Allocated: This cash is still in the account, but cannot be used.
    /// </summary>
    public class CashTrxAbstract : CommonWithId
    {
        public CashTrxAbstract()
        {
            CashTypeEnum = CashTypeENUM.Unknown;
            CashStateEnum = CashStateENUM.Available;
        }

        public CashTrxAbstract(long docNumber, DateTime date, string personFromId, string personToId, decimal amount, CashStateENUM cashStateEnum, CashTypeENUM cashTypeEnum)
        {
            DocNumber = docNumber;
            PersonFromId = personFromId;
            PersonToId = personToId;
            Amount = amount;
            CashStateEnum = cashStateEnum;
            CashTypeEnum = cashTypeEnum;
            MetaData.Created.AddDate(date);

        }

        //this holds how this trx is dirtributed.
        public virtual ICollection<CashTrxDistribution> CashTrxDistributions { get; set; }

        /// <summary>
        /// This is the toal amount in the cash distribution. This andthe amount should be equal other wise
        /// an undistributed amount will be left.
        /// </summary>
        public decimal TotalInCashDistribution
        {
            get
            {
                if(CashTrxDistributions.IsNullOrEmpty())
                    return 0;
                decimal ttl = 0;
                foreach (CashTrxDistribution item in CashTrxDistributions)
	            {
                    ttl += item.Amount;
	            }
                return ttl;
            }
        }
        /// <summary>
        /// this is true if total cash distribution and cash amount are equal.
        /// </summary>
        public bool IsCashAndCashDistributionEqual
        {
            get
            {
                if (CashTrxDistributions.IsNullOrEmpty())
                    return false;
                return TotalInCashDistribution == Amount;
            }
        }

        [Display(Name = "Cash Status")]
        public CashStateENUM CashStateEnum { get; set; }

        public SelectList CashStateEnumSelectList { get { return EnumExtention.ToSelectListSorted<CashStateENUM>(CashStateENUM.Allocated); } }

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

            //PersonFromId.IsNullOrWhiteSpaceThrowException("PersonId");
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
            Amount = pta.Amount;
            CashTypeEnum = pta.CashTypeEnum;
            //DateComplex = pta.DateComplex;
            //DocNumber = pta.DocNumber;

        }


    }
}
