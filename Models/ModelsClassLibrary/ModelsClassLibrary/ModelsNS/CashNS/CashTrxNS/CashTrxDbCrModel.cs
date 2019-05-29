using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.CashTrxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.CashNS.CashTrxNS
{
    /// <summary>
    /// Note. The cash transactions received by this class (Transactions) ara already filtered by the entity for whom the trxs are meant for.
    /// </summary>
    [NotMapped]
    public class CashTrxDbCrModel
    {

        public CashTrxDbCrModel()
        {

        }
        public CashTrxDbCrModel(List<CashTrxVM> receivedTrx, List<CashTrxVM> paidTrx, DateTime fromDate, DateTime toDate, string personName, CashTypeENUM cashTypeEnum, bool isAdmin)
        {
            //Person = person;
            ReceivedTrx = receivedTrx;
            PaidTrx = paidTrx;
            FromDate = fromDate;
            ToDate = toDate;
            CashTypeEnum = cashTypeEnum;
            PersonName = personName;
            IsAdmin = isAdmin;

        }
        //public Person Person { get; set; }

        public bool IsAdmin { get; private set; }
        public string Heading
        {
            get
            {
                string heading = "Error";
                switch (CashTypeEnum)
                {
                    case CashTypeENUM.Refundable:
                    case CashTypeENUM.NonRefundable:
                    case CashTypeENUM.Total:
                        heading = string.Format("{0} Cash", CashTypeEnum.ToString());
                        break;
                    case CashTypeENUM.Unknown:
                    default:
                        break;
                }

                heading += " for " + PersonName;

                if (IsAdmin)
                    heading += " -Admin Screen";
                return heading;

            }
        }

        public string SubHeading
        {
            get
            {
                string heading = string.Format("Between {0:dd-MMM-yyyy} and {1:dd-MMM-yyyy}", FromDate, ToDate);


                return heading;

            }
        }
        public CashTypeENUM CashTypeEnum { get; set; }

        public ApplicationUser User { get; set; }

        /// <summary>
        /// This is tne name of the person for whom we are conducting
        /// the trxs. So in Paid, it will be the from name, and in received it will be the To name
        /// </summary>
        public string PersonName { get; set; }

        public List<CashTrxVM> ReceivedTrx { get; set; }

        public List<CashTrxVM> PaidTrx { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }


        public decimal PaidBF
        {
            get
            {
                decimal _paidBf = 0;
                if (!PaidTrx.IsNullOrEmpty())
                {
                    foreach (var trx in PaidTrx)
                    {
                        DateTime date = trx.Date;
                        if (date < FromDate)
                            _paidBf += trx.PaymentAmount;
                    }
                }
                return _paidBf;
            }
        }

        public decimal RecievedBF
        {
            get
            {
                decimal _receivedBF = 0;
                if (!ReceivedTrx.IsNullOrEmpty())
                {
                    foreach (var trx in ReceivedTrx)
                    {
                        DateTime date = trx.Date;
                        if (date < FromDate)
                            _receivedBF += trx.ReceiptAmount;
                    }
                }
                return _receivedBF;

            }
        }
        /// <summary>
        /// This calculates the brought forward.
        /// </summary>
        public decimal BroughtForward
        {
            get
            {

                return RecievedBF - PaidBF;

            }
        }

        /// <summary>
        /// This gets the transactions within date. It also fixes the description. If from/to is null, it adds bank in there.
        /// </summary>
        public List<CashTrxVM> CashTrxs
        {
            get
            {
                List<CashTrxVM> lst = new List<CashTrxVM>();
                if (!PaidTrx.IsNullOrEmpty())
                {
                    foreach (var trx in PaidTrx)
                    {
                        lst.Add(trx);
                    }
                }
                if (!ReceivedTrx.IsNullOrEmpty())
                {
                    foreach (var trx in ReceivedTrx)
                    {
                        lst.Add(trx);
                    }
                }

                if (lst.IsNullOrEmpty())
                    return null;

                return lst.OrderBy(x => x.Date).ToList();
            }
        }


        public decimal FinalBalance
        {
            get
            {
                decimal finalBalance = BroughtForward;
                if (CashTrxs.IsNullOrEmpty())
                    return finalBalance;
                foreach (CashTrxVM trx in CashTrxs)
                {
                    finalBalance += trx.ReceiptAmount - trx.PaymentAmount;
                }
                return finalBalance;
            }
        }

        public decimal TotalPaid
        {
            get
            {
                decimal _ttlPaid = 0;
                if (!PaidTrx.IsNullOrEmpty())
                {
                    foreach (var trx in PaidTrx)
                    {
                        _ttlPaid += trx.PaymentAmount;
                    }
                }
                return _ttlPaid;
            }
        }

        public decimal TotalRecieved
        {
            get
            {
                decimal _totalReceived = 0;
                if (!ReceivedTrx.IsNullOrEmpty())
                {
                    foreach (var trx in ReceivedTrx)
                    {
                        _totalReceived += trx.ReceiptAmount;
                    }
                }
                return _totalReceived;

            }
        }



    }
}
