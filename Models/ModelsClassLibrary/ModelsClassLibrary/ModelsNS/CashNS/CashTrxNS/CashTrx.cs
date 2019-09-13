using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
//using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.CashNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.CashTrxNS
{

    /// <summary>
    /// When the buyselldoc item is added as a cashtrx for the statement, its Id is from the buyselldoc
    /// When the From is blank ... money has been created.
    /// When the To is blank ... money has been destroyed.
    /// Only Bank is allowed to create and destroy money.
    /// </summary>
    public class CashTrx : CashTrxAbstract
    {
        public CashTrx()
            : base()
        {

        }
        public CashTrx(string id, long docNumber, DateTime date, string personFromId, string personToId, decimal amount, CashStateENUM cashStateEnum, CashTypeENUM cashTypeEnum) :
            base(docNumber, date, personFromId, personToId, amount, cashStateEnum, cashTypeEnum)
        {
            // _docNumberStr = docNumber.ToString();
            Id = id;

        }

        public static CashTrx Unbox(ICommonWithId ICommonWithId)
        {
            CashTrx cashTrx = ICommonWithId as CashTrx;
            cashTrx.IsNullThrowException("Unboxing");
            return cashTrx;
        }
        //public CashTrx(string docNumberStr, DateTime date, string personFromId, string personToId, decimal amount, CashStateENUM cashStateEnum, CashTypeENUM cashTypeEnum) :
        //    base(0, date, personFromId, personToId, amount, cashStateEnum, cashTypeEnum)
        //{
        //    _docNumberStr = docNumberStr;
        //}

        //string _docNumberStr = "";
        //public string DocNumberStr { get { return _docNumberStr; } }



        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.CashTrx;
        }
        public override bool HideNameInView()
        {
            return true;
        }

        public override string MakeUniqueName()
        {
            string uniqueName = string.Format("{0:0000000#}", DocNumber);
            return uniqueName;
        }


        public override void SelfErrorCheck()
        {
            if (DocNumber == 0)
                throw new Exception("Document number is zero");


            if (Amount == 0)
                throw new Exception("Amount is zero");

            if (CashTypeEnum == CashTypeENUM.Unknown)
                throw new Exception("Cash Type is Unknown");
            base.SelfErrorCheck();
        }

        public override string FullName()
        {
            string personToName = PersonTo.IsNull() ? "-" : PersonTo.Name;
            string personFromName = PersonFrom.IsNull() ? "-" : PersonFrom.Name;
            string nonRefundable = "[" + CashTypeEnum.ToString().ToTitleSentance() + "]";
            //string nonRefundable = CashTypeEnum == CashTypeENUM.NonRefundable ? "[" + CashTypeEnum.ToString().ToTitleSentance() + "]" : "";

            string commentFixed = "";
            if (!Comment.IsNullOrWhiteSpace())
                commentFixed = string.Format("({0})", Comment);

            string fullName = string.Format("[#{1:000000#}] {0} From: {3} => {4} Rs{2:#,0.00} {5} {6}",
                MetaData.Created.Date_NotNull_Max,
                DocNumber,
                Amount,
                personFromName,
                personToName,
                nonRefundable.ToUpper(),
                commentFixed);
            return fullName.Trim();
        }

        [NotMapped]
        public string CommentExtended { get; set; }

        /// <summary>
        /// All cash transactions that are applied to this will be saved here.
        /// </summary>
        //public virtual ICollection<BuySellDocCashTrxApplied> BuySellDocCashTrxApplieds { get; set; }

        public void SetupCashTrx(Person personFrom, Person personTo, decimal amount, CashStateENUM cashStateEnum, CashTypeENUM cashTypeEnum, UserParameter userParameter, string comment, string name, string id)
        {
            MetaData.Created.SetToTodaysDate(userParameter.UserName, userParameter.UserId);

            PersonFrom = personFrom;
            PersonTo = personTo;

            PersonFromId = personFrom.IsNull() ? "" : personFrom.Id;
            PersonToId = personTo.IsNull() ? "" : personTo.Id;

            Amount = amount;
            CashStateEnum = cashStateEnum;
            CashTypeEnum = cashTypeEnum;
            Comment = comment;
            Name = name;
            if (!id.IsNullOrWhiteSpace())
                Id = id;

        }


        public CashTrxVM2 ConvertToCashTrxVM2(string comment, string personIdForWhomWeAreWorking)
        {
            string fixedComment = "";
            if (comment.IsNullOrWhiteSpace())
            {
                if (Comment.IsNullOrWhiteSpace())
                {

                }
                else
                {
                    fixedComment = Comment;
                }
            }
            else
            {
                if (Comment.IsNullOrWhiteSpace())
                {
                    fixedComment = comment;
                }
                else
                {
                    fixedComment = comment + " - " + Comment;
                }
            }
            decimal amountReceived = 0;
            decimal amountPaid = 0;

            if (PersonFromId == personIdForWhomWeAreWorking)
                amountPaid = Amount;

            if (PersonToId == personIdForWhomWeAreWorking)
                amountReceived = Amount;
            Person fakeBank = new Person();
            fakeBank.Name = "Bank";
            Person personFrom = PersonFrom;
            Person personTo = PersonTo;

            if (personFrom.IsNull())
                personFrom = fakeBank;
            if (personTo.IsNull())
                personFrom = fakeBank;

            //if either personFrom or personTo are blank, then it is to or from bank
            //if (personFrom.IsNull())
            //    return null;
            //if (personTo.IsNull())
            //    return null;

            CashTrxVM2 cashTrxVM2 = new CashTrxVM2(Id, MetaData.Created.Date_NotNull_Min, amountPaid, amountReceived, fixedComment, personFrom, personTo, CashTypeEnum, CashStateEnum, CashTrxVmDocumentTypeENUM.CashTrx, Name, BuySellDocStateENUM.CashTransaction);

            return cashTrxVM2;
        }

    }
}
