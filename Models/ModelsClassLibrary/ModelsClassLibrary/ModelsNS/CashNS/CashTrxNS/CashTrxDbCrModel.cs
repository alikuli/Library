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
        public CashTrxDbCrModel(List<CashTrxVM> receivedTrx, List<CashTrxVM> paidTrx, DateTime fromDate, DateTime toDate, string personName, CashTypeENUM cashTypeEnum)
        {
            //Person = person;
            ReceivedTrx = receivedTrx;
            PaidTrx = paidTrx;
            FromDate = fromDate;
            ToDate = toDate;
            CashTypeEnum = cashTypeEnum;
            PersonName = personName;

        }
        //public Person Person { get; set; }
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

        /// <summary>
        /// This calculates the brought forward.
        /// </summary>
        public decimal BroughtForward
        {
            get
            {
                decimal _broughtForward = 0;
                if (!PaidTrx.IsNullOrEmpty())
                {
                    foreach (var trx in PaidTrx)
                    {
                        DateTime date = trx.Date;
                        if (date < FromDate)
                            _broughtForward -= trx.PaymentAmount;
                    }
                }
                if (!ReceivedTrx.IsNullOrEmpty())
                {
                    foreach (var trx in ReceivedTrx)
                    {
                        DateTime date = trx.Date;
                        if (date < FromDate)
                            _broughtForward += trx.ReceiptAmount;
                    }
                }

                return _broughtForward;

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




    }
}
