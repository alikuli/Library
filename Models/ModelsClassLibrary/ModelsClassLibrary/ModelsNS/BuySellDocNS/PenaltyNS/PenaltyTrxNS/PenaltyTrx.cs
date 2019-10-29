using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS
{
    public class PenaltyTrx : CommonWithId
    {
        public PenaltyTrx()
        {

        }

        public PenaltyTrx(string personId, decimal amount, string comment, bool isToPay)
        {
            PersonId = personId;
            Amount = amount;
            Comment = comment;
            IsToPay = isToPay;
        }
        public static PenaltyTrx Unbox(ICommonWithId icommonWithId)
        {
            PenaltyTrx penaltyTrx = icommonWithId as PenaltyTrx;
            penaltyTrx.IsNullThrowException();
            return penaltyTrx;
        }

        /// <summary>
        /// If true, then the money is being paid. False means the money is being recieved
        /// </summary>
        public bool IsToPay { get; set; }
        public string PersonId { get; set; }
        public virtual Person Person { get; set; }
        public decimal Amount { get; set; }

        [Display(Name = "Penalty")]
        public string PenaltyHeaderId { get; set; }
        public virtual PenaltyHeader PenaltyHeader { get; set; }


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.PenaltyTrx;
        }
        public override string MakeUniqueName()
        {
            //string name = string.Format("{0}")
            return Id;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            //PenaltyTrx p = PenaltyTrx.Unbox(icommonWithId);
            //PersonId = p.PersonId;
            //IsToPay = p.IsToPay;
            //Amount = p.Amount;
            //PenaltyHeaderId = p.PenaltyHeaderId;
        }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();

        }

        public override string FullName()
        {
            if (!PenaltyHeader.IsNull())
                return PenaltyHeader.FullName();
            return base.FullName();
        }





        //private void createCashTrxVM2(CashStateENUM cashStateEnum, Person fromPerson, Person ToPerson, decimal receivedAmount, decimal paymentAmount, string commentFixed)
        //{
        //    CashTrxVM2 cashTrxVm2 = new CashTrxVM2(
        //        Id,
        //        MetaData.Created.Date_NotNull_Min,
        //        paymentAmount,
        //        receivedAmount,
        //        commentFixed,
        //        fromPerson,
        //        ToPerson,
        //        CashTypeENUM.Refundable,
        //        cashStateEnum,
        //        CashTrxVmDocumentTypeENUM.Penalty,
        //        DocumentNo.ToString("N0"),
        //        BuySellDocStateEnum);
        //}



    }
}
