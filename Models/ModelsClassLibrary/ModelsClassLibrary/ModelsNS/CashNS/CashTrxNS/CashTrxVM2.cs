using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.CashTrxNS
{
    [NotMapped]
    public class CashTrxVM2
    {
        public CashTrxVM2()
        {

        }
        public CashTrxVM2(string id, DateTime date, decimal paymentAmount, decimal receiptAmount, string comment, Person fromPerson, Person toPerson, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, CashTrxVmDocumentTypeENUM cashTrxVmDocumentTypeEnum, string docNo, BuySellDocStateENUM buySellDocStateEnum)
        {
            Id = id;
            Date = date;
            PaymentAmount = paymentAmount;
            ReceiptAmount = receiptAmount;
            Comment = comment;
            CashTypeEnum = cashTypeEnum;
            CashStateEnum = cashStateEnum;
            CashTrxVmDocumentTypeEnum = cashTrxVmDocumentTypeEnum;
            FromPerson = fromPerson;
            ToPerson = toPerson;
            DocNo = docNo;
            BuySellDocStateEnum = buySellDocStateEnum;
        }

        public string Id { get; set; }

        string DocNo { get; set; }

        public string DocNoFixed()
        {
            DocNo.IsNullOrWhiteSpaceThrowException();
            string docNo = string.Format("{0} #{1} [{2}]",
                CashTrxVmDocumentTypeEnum.ToString().ToTitleSentance(),
                DocNo,
                BuySellDocStateEnum.ToString().ToTitleSentance());
            return docNo;
        }
        public bool HasValue
        {
            get
            {
                return !(ReceiptAmount == 0 && PaymentAmount == 0);
            }
        }
        public BuySellDocStateENUM BuySellDocStateEnum { get; set; }
        public Person FromPerson { get; set; }
        public Person ToPerson { get; set; }

        public DateTime Date { get; set; }

        public CashTrxVmDocumentTypeENUM CashTrxVmDocumentTypeEnum { get; set; }
        public CashTypeENUM CashTypeEnum { get; set; }
        public CashStateENUM CashStateEnum { get; set; }



        [Display(Name = "Paid")]
        public decimal PaymentAmount { get; set; }


        [Display(Name = "Received")]
        public decimal ReceiptAmount { get; set; }


        [Display(Name = "Paid")]
        public decimal PaymentAmount_ForTotal
        {
            get
            {
                return PaymentAmount;
            }
        }

        [Display(Name = "Received")]
        public decimal ReceiptAmount_ForTotal
        {
            get
            {
                if (CashStateEnum == CashStateENUM.Allocated)
                    return 0;
                return ReceiptAmount;
            }
        }




        /// <summary>
        /// This where the description for the cash item is created. 
        /// </summary>
        public string FromTo
        {
            get
            {
                string str = "";
                str = string.Format("from: {1} -> To: {0} ", FromPerson.FullName(), ToPerson.FullName());
                return str;
            }
        }




        string Comment { get; set; }

        public string FixedComment
        {
            get
            {

                string strCashType = "";
                string strAllocated = "";
                string cashTrxVmDocumentType = CashTrxVmDocumentTypeEnum.ToString().ToTitleSentance();

                switch (CashTypeEnum)
                {
                    case CashTypeENUM.Unknown:
                        strCashType = Comment + " [Unknown Cash Type]";
                        break;
                    case CashTypeENUM.Refundable:
                        strCashType = Comment + " [REFUNDABLE]";
                        break;
                    case CashTypeENUM.NonRefundable:
                        strCashType = Comment + " [Not Refundable]";
                        break;
                    default:
                        break;

                }


                if (CashStateEnum == CashStateENUM.Allocated)
                    strAllocated = string.Format("{0}", "(Not Available)");
                else
                    strAllocated = string.Format("{0}", " AVAILABLE");

                string finalComment = string.Format("{0} {1} {2}",
                    cashTrxVmDocumentType,
                    strCashType,
                    strAllocated);



                return finalComment;
            }
        }


        public static decimal RunningTotal(List<CashTrxVM2> listOfCashTrxVM2)
        {
            decimal totalRecipts = CashTrxVM2.TotalReceiptsCol(listOfCashTrxVM2);
            decimal totalPayments = CashTrxVM2.TotalPaymentsCol(listOfCashTrxVM2);
            decimal total = totalRecipts - totalPayments;
            return total;
        }


        public static decimal TotalReceiptsCol(List<CashTrxVM2> listOfCashTrxVM2)
        {
            decimal ttl = 0;

            if (listOfCashTrxVM2.IsNullOrEmpty())
                return 0;

            foreach (CashTrxVM2 cashTrxVM2 in listOfCashTrxVM2)
            {
                ttl += cashTrxVM2.ReceiptAmount_ForTotal;
            }

            return ttl;

        }

        public static decimal TotalPaymentsCol(List<CashTrxVM2> listOfCashTrxVM2)
        {
            decimal ttl = 0;

            if (listOfCashTrxVM2.IsNullOrEmpty())
                return 0;

            foreach (CashTrxVM2 cashTrxVM2 in listOfCashTrxVM2)
            {
                ttl += cashTrxVM2.PaymentAmount_ForTotal;
            }

            return ttl;

        }
        public static List<CashTrxVM2> ConvertCashTrxListToCashVM2List(List<CashTrx> lst_CashTrx, string personIdForWhomWeAreWorking)
        {
            List<CashTrxVM2> lst_CashTrxVM2 = new List<CashTrxVM2>();
            if (!lst_CashTrx.IsNullOrEmpty())
            {
                foreach (var cashTrx in lst_CashTrx)
                {
                    CashTrxVM2 cashTrxVM2 = cashTrx.ConvertToCashTrxVM2("(From Cash)", personIdForWhomWeAreWorking);
                    if (cashTrxVM2.HasValue)
                        lst_CashTrxVM2.Add(cashTrxVM2);
                }
            }
            return lst_CashTrxVM2;
        }



    }


}
