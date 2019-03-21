using EnumLibrary.EnumNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.CashTrxNS
{
    [NotMapped]
    public class CashTrxVM
    {
        public CashTrxVM()
        {

        }
        public CashTrxVM(string id, DateTime date, string name, decimal paymentAmount, decimal receiptAmount, string comment, string fromName, string toName, CashTypeENUM cashTypeEnum)
        {
            Id = id;
            Date = date;
            Name = name;
            PaymentAmount = paymentAmount;
            ReceiptAmount = receiptAmount;
            Comment = comment;
            FromName = fromName;
            ToName = toName;
            CashTypeEnum = cashTypeEnum;
        }

        public string FromName { get; set; }
        public string ToName { get; set; }

        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        [Display(Name = "Paid")]
        public decimal PaymentAmount { get; set; }

        [Display(Name = "Received")]
        public decimal ReceiptAmount { get; set; }

        //[Display(Name = "Running Balance")]
        //public decimal RunningBalance { get; set; }

        public string Comment { get; set; }

        public string FromTo
        {
            get
            {
                if (PaymentAmount != 0)
                {
                    string str = string.Format("{0} paid {1}", FromName, ToName);
                    return str;
                }
                if (ReceiptAmount != 0)
                {
                    string str = string.Format("{0} Received From {1}", ToName, FromName);
                    return str;
                }

                return "ERROR";
            }
        }

        public CashTypeENUM CashTypeEnum { get; set; }


        //private List<CashTrx> fixTheComment(IQueryable<CashTrx> cashTrxPaid)
        //{
        //    var lst = cashTrxPaid.ToList();
        //    if (lst.IsNullOrEmpty())
        //        return lst;

        //    foreach (var item in lst)
        //    {
        //        item.CommentExtended = fixComment(item.Comment, item.CashTypeEnum);
        //    }

        //    return lst;
        //}

        public string FixedComment
        {
            get
            {
                string extendedComment = "";
                switch (CashTypeEnum)
                {
                    case CashTypeENUM.Unknown:
                        extendedComment = Comment + " [ERR]";
                        break;
                    case CashTypeENUM.Refundable:
                        extendedComment = Comment + " [R]";
                        break;
                    case CashTypeENUM.NonRefundable:
                        extendedComment = Comment + " [NR]";
                        break;
                    default:
                        break;
                }
                return extendedComment.Trim();
            }
        }

    }
}
