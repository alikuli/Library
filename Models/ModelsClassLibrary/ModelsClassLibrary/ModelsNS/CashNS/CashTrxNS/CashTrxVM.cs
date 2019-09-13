using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AliKuli.Extentions;

namespace ModelsClassLibrary.CashTrxNS
{
    [NotMapped]
    public class CashTrxVM
    {
        public CashTrxVM()
        {

        }
        public CashTrxVM(string id, DateTime date, string name, decimal paymentAmount, decimal receiptAmount, string comment, string ownerName, string customerName, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum)
        {
            Id = id;
            Date = date;
            Name = name;
            PaymentAmount = paymentAmount;
            ReceiptAmount = receiptAmount;
            Comment = comment;
            OwnerName = ownerName;
            CustomerName = customerName;
            CashTypeEnum = cashTypeEnum;
            CashStateEnum = cashStateEnum;
        }

        public string Id { get; set; }

        public string OwnerName { get; set; }
        public string CustomerName { get; set; }

        public DateTime Date { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// If true then we have an encashment
        /// </summary>
        public bool IsEncashment { get; set; }
        [Display(Name = "Paid")]
        public decimal PaymentAmount { get; set; }


        [Display(Name = "Received")]
        public decimal ReceiptAmount { get; set; }


        public decimal PaymentAmount_Adjusted_For_CashState
        {
            get
            {
                return PaymentAmount;
            }
        }

        public decimal ReceiptAmount_Adjusted_For_Cash_State
        {
            get
            {
                if (CashStateEnum == CashStateENUM.Allocated)
                    return 0;
                return ReceiptAmount;
            }
        }
        //[Display(Name = "Running Balance")]
        //public decimal RunningBalance { get; set; }

        public string Comment { get; set; }



        /// <summary>
        /// This where the description for the cash item is created. 
        /// </summary>
        public string FromTo
        {
            get
            {
                if (PaymentAmount != 0)
                {
                    string str = "";

                    //if it is a payment and allocatted,
                    //then it is a purchase order or encashment
                    if (CashStateEnum == CashStateENUM.Allocated)
                    {
                        str = string.Format("from: {1} -> To: {0} ", OwnerName, CustomerName);
                        return str;

                    }
                    str = string.Format("{0} paid {1}", CustomerName, OwnerName);
                    return str;
                }
                if (ReceiptAmount != 0)
                {
                    string str = string.Format("{0} Received From {1}", CustomerName, OwnerName);
                    return str;
                }

                return "ERROR";
            }
        }

        public CashTypeENUM CashTypeEnum { get; set; }
        public CashStateENUM CashStateEnum { get; set; }




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
