using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ModelsClassLibrary.ModelsNS.CashNS.PenaltyNS
{
    public class PenaltyHeader : CommonWithId
    {
        public PenaltyHeader()
        {

        }
        public PenaltyHeader(BuySellDoc buySellDoc, string reason, decimal penaltyAmount, Person personFrom, bool isToPay)
        {
            BuySellDoc = buySellDoc;
            BuySellDocId = buySellDoc.Id;
            BuySellDocStateEnum = buySellDoc.BuySellDocStateEnum;
            BuySellDocStateModifierEnum = buySellDoc.BuySellDocStateModifierEnum;
            BuySellDocumentTypeEnum = buySellDoc.BuySellDocumentTypeEnum;
            Comment = reason;
            Amount = penaltyAmount;
            FromPersonId = personFrom.Id;
            FromPerson = personFrom;
            IsToPay = isToPay;

        }

        public bool IsToPay { get; set; }
        public long DocumentNo { get; set; }

        [Display(Name = "From Person")]
        public string FromPersonId { get; set; }
        public virtual Person FromPerson { get; set; }
        public decimal Amount { get; set; }

        public static PenaltyHeader Unbox(ICommonWithId icommonWithId)
        {
            PenaltyHeader penaltyHdr = icommonWithId as PenaltyHeader;
            penaltyHdr.IsNullThrowException();
            return penaltyHdr;
        }

        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.PenaltyHeader;
        }


        //we take a snapshot of the state for our records because BuySellDoc state can
        //change in the future.
        public BuySellDocStateENUM BuySellDocStateEnum { get; set; }
        public BuySellDocStateModifierENUM BuySellDocStateModifierEnum { get; set; }
        public BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }

        public override string Input1SortString
        {
            get
            {
                return DocumentNo.ToString("000000");
            }
        }
        public override string NameInput1
        {
            get
            {
                return "Number";
            }
        }


        public virtual ICollection<PenaltyTrx> PenaltyTrxs { get; set; }
        public virtual List<PenaltyTrx> PenaltyTrxs_Fixed
        {
            get
            {
                if (PenaltyTrxs.IsNullOrEmpty())
                    return new List<PenaltyTrx>();

                List<PenaltyTrx> notDeletedAmounts = PenaltyTrxs.Where(x => x.MetaData.IsDeleted == false).ToList();
                return notDeletedAmounts;
            }
        }

        public string BuySellDocId { get; set; }
        public virtual BuySellDoc BuySellDoc { get; set; }


        public override string MakeUniqueName()
        {
            if (DocumentNo == 0)
                throw new Exception("Document number not initialized");

            string name = string.Format("{0}-{1}", MetaData.Created.By, DocumentNo);
            return name;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            PenaltyHeader p = PenaltyHeader.Unbox(icommonWithId);
            Amount = p.Amount;
            BuySellDocStateEnum = p.BuySellDocStateEnum;
            BuySellDocStateModifierEnum = p.BuySellDocStateModifierEnum;
            BuySellDocumentTypeEnum = p.BuySellDocumentTypeEnum;
            BuySellDocId = p.BuySellDocId;
        }

        public override void SelfErrorCheck()
        {
            base.SelfErrorCheck();
            //amountIsNotZero();
            buySellDocIsAttached();
            buySellDocStateEnumIsUnknown();
            buySellDocStateModifierEnumIsUnknown();
            buySellDocumentTypeEnumIsUnknown();
            documentNumberIsNotZero();
            //totalAmountNotUtilized();

        }

        private void totalAmountNotUtilized()
        {
            decimal calculatedAmount = 0;

            if (PenaltyTrxs.IsNullOrEmpty())
            {
                //do nothing
            }
            else
            {
                calculatedAmount = PenaltyTrxs_Fixed.Sum(x => x.Amount);
            }

            if (Amount == calculatedAmount)
                return;
            string err = string.Format("Amount not fully utilized. Amount available is {0}, and utilized is {1}",
                Amount,
                calculatedAmount);
            throw new Exception(err);

        }
        public string CommentFixed
        {
            get
            {
                string str = Comment;
                if (Comment.Length > 17)
                    str = Comment.Substring(0, 17) + "...";
                return str;
            }
        }

        private void documentNumberIsNotZero()
        {
            if (DocumentNo == 0)
            {
                throw new Exception("Document No is zero");
            }

        }

        private void buySellDocumentTypeEnumIsUnknown()
        {
            if (BuySellDocumentTypeEnum == BuySellDocumentTypeENUM.Unknown)
                throw new Exception("BuySellDocumentTypeEnum Unknown");
        }

        private void buySellDocStateModifierEnumIsUnknown()
        {
            if (BuySellDocStateModifierEnum == BuySellDocStateModifierENUM.Unknown)
                throw new Exception("BuySellDocStateModifierEnum Unknown");
        }

        private void buySellDocStateEnumIsUnknown()
        {
            if (BuySellDocStateEnum == BuySellDocStateENUM.Unknown)
                throw new Exception("BuySellDocStateEnum Unknown");

        }

        private void buySellDocIsAttached()
        {
            BuySellDocId.IsNullOrWhiteSpaceThrowException("BuysellDoc not attached");
        }

        //private void amountIsNotZero()
        //{
        //    if(Amount == 0)
        //    {
        //        throw new Exception("Amount is zero");
        //    }
        //}


    }
}
