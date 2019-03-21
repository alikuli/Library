using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.CashNS;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.CashTrxNS
{
    public class CashTrx : CashTrxAbstract
    {
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

            string fullName = string.Format("[#{1:000000#}] {0} From: {3} To: {4} Rs{2:#,0.00} {5} {6}",
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

    }
}
