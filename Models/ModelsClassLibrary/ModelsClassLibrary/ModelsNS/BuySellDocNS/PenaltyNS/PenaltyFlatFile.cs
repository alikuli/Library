using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.CashTrxNS;
using EnumLibrary.EnumNS;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.BuySellDocNS.PenaltyNS
{
    [NotMapped]
    public class PenaltyFlatFile
    {
        public PenaltyFlatFile(
            string id, 
            DateTime date, 
            decimal paymentAmount, 
            decimal receiptAmount,
            string commentHeader,
            string commentTrx, 
            Person fromPerson, 
            Person toPerson, 
            string documentNumber, 
            BuySellDocStateENUM buySellDocStateEnum, 
            BuySellDocumentTypeENUM buySellDocumentTypeEnum, 
            BuySellDocStateModifierENUM buySellDocStateModifierEnum)
        {
            Id = id;
            Date = date;
            PaymentAmount = paymentAmount;
            RecieptAmount = receiptAmount;
            CommentHeader = commentHeader;
            CommentTrx = commentTrx;
            FromPerson = fromPerson;
            ToPerson = toPerson;
            DocumentNumber = documentNumber;
            BuySellDocStateEnum = buySellDocStateEnum;
            BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
            BuySellDocStateModifierEnum = buySellDocStateModifierEnum;

        }
        public string Id { get; set; }

        [Display(Name = "Document Status")]
        public BuySellDocStateENUM BuySellDocStateEnum { get; set; }

        public BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }
        public BuySellDocStateModifierENUM BuySellDocStateModifierEnum { get; set; }

        [Display(Name="Document Number")]
        public string DocumentNumber { get; set; }
        public DateTime Date { get; set; }

        [Display(Name = "From Person")]
        public Person FromPerson { get; set; }

        [Display(Name = "To Person")]
        public Person ToPerson { get; set; }

        [Display(Name = "Receipt Amount")]
        public decimal RecieptAmount { get; set; }

        [Display(Name = "Payment Amount")]
        public decimal PaymentAmount { get; set; }

        public string CommentHeader { get; set; }

        public string CommentHeaderFixed()
        {
            string str = CommentHeader;
            if (CommentHeader.Length > 20)
                str = CommentHeader.Substring(0, 17) + "...";
            return str;

        }
        public string CommentTrx { get; set; }

        public string CommentFixed()
        {
            string str = string.Format("{0}. {1}",CommentTrx, CommentHeaderFixed());
            return str;
        }

        public CashTrxVM2 ConvertTo_CashTrxVM2 ()
        {
            CashTrxVM2 cashTrx = new CashTrxVM2(
                Id, 
                Date, 
                PaymentAmount, 
                RecieptAmount,
                CommentFixed(), 
                FromPerson, 
                ToPerson, 
                CashTypeENUM.Refundable, 
                CashStateENUM.Available, 
                CashTrxVmDocumentTypeENUM.Penalty, 
                DocumentNumber, 
                BuySellDocStateEnum);

            return cashTrx;
        }
    }
}
