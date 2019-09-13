using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.CashTrxNS;
using ModelsClassLibrary.ModelsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
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
        public CashTrxDbCrModel(List<CashTrxVM2> cashTrx, DateTime fromDate, DateTime toDate, CashTypeENUM cashTypeEnum, CashStateENUM cashStateEnum, UserParameter userParameter, bool isShowAdminReports)
        {
            CashTrx = cashTrx;
            FromDate = fromDate;
            ToDate = toDate;
            CashTypeEnum = cashTypeEnum;
            CashStateEnum = cashStateEnum;
            UserParameter = userParameter;
            IsShowAdminReports = isShowAdminReports;

        }
        public bool IsShowAdminReports { get; private set; }

        //public Person Person { get; set; }
        UserParameter UserParameter { get; set; }
        public CashStateENUM CashStateEnum { get; set; }

        public bool IsAdmin
        {
            get
            {
                UserParameter.IsNullThrowException();
                return UserParameter.IsAdmin;
            }
        }
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
        public string PersonName
        {
            get
            {
                UserParameter.IsNullThrowException();
                return UserParameter.PersonName;
            }
        }

        //public List<CashTrxVM2> ReceivedTrx { get; set; }

        //public List<CashTrxVM2> PaidTrx { get; set; }

        public List<CashTrxVM2> CashTrx { get; set; }
        

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }


        public decimal PaidBF
        {
            get
            {
                decimal _paidBf = 0;
                if (!CashTrx.IsNullOrEmpty())
                {
                    DateParameter dateparam = new DateParameter();

                    foreach (var trx in CashTrx)
                    {
                        DateTime date = trx.Date;
                        if (dateparam.Date1BeforeDate2(trx.Date, FromDate))
                            _paidBf += trx.PaymentAmount_ForTotal;
                    }
                }
                return _paidBf;
            }
        }

        public decimal RecievedBF
        {
            get
            {
                DateParameter dateparam = new DateParameter();
                decimal _receivedBF = 0;
                if (!CashTrx.IsNullOrEmpty())
                {
                    foreach (var trx in CashTrx)
                    {
                        if (dateparam.Date1BeforeDate2(trx.Date, FromDate))
                            if (trx.CashStateEnum == CashStateENUM.Available)
                                _receivedBF += trx.ReceiptAmount_ForTotal;
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



        public bool IsNoMoreData
        {
            get
            {
                if (CashTrxs_DateDelimited.IsNullOrEmpty())
                {
                    FromDate = DateTime.MinValue;
                }

                DateParameter dateParm = new DateParameter();

                if (!CashTrx.IsNull())
                {
                    foreach (var item in CashTrx)
                    {
                        if (dateParm.IsDateWithinBeginAndEndDatesInclusive(item.Date, DateTime.MinValue, ToDate))
                            return false;
                    }
                }

                //if (!ReceivedTrx.IsNull())
                //{
                //    foreach (var item in ReceivedTrx)
                //    {
                //        if (dateParm.IsDateWithinBeginAndEndDatesInclusive(item.Date, DateTime.MinValue, ToDate))
                //            return false;
                //    }
                //}



                return true;
            }

        }

        /// <summary>
        /// This gets the transactions within date. It also fixes the description. If from/to is null, it adds bank in there.
        /// </summary>
        public List<CashTrxVM2> CashTrxs_DateDelimited
        {
            get
            {

                if (CashTrx.IsNullOrEmpty())
                    return null;

                List<CashTrxVM2> lst = new List<CashTrxVM2>();
                DateParameter dateParam = new DateParameter();
                if (!CashTrx.IsNullOrEmpty())
                {
                    foreach (var trx in CashTrx)
                    {
                        if (dateParam.IsDateWithinBeginAndEndDatesInclusive(trx.Date, FromDate, ToDate))
                            lst.Add(trx);
                    }
                }

                return lst.OrderBy(x => x.Date).ToList();


                //DateParameter dateParam = new DateParameter();

                //if (!PaidTrx.IsNullOrEmpty())
                //{
                //    foreach (var trx in PaidTrx)
                //    {
                //        if(dateParam.IsDateWithinBeginAndEndDatesInclusive(trx.Date, FromDate, ToDate))
                //            lst.Add(trx);
                //    }
                //}
                //if (!ReceivedTrx.IsNullOrEmpty())
                //{
                //    foreach (var trx in ReceivedTrx)
                //    {
                //        if (dateParam.IsDateWithinBeginAndEndDatesInclusive(trx.Date, FromDate, ToDate))
                //            lst.Add(trx);
                //    }
                //}

            }
        }
        //public List<CashTrxVM2> PaidTrx_DateDelimited
        //{
        //    get
        //    {
        //        List<CashTrxVM2> lst = new List<CashTrxVM2>();
        //        lst = Get_PaidTrx_DateDelimited(lst);

        //        return lst;
        //    }
        //}
        //public List<CashTrxVM2> ReceivedTrx_DateDelimited
        //{
        //    get
        //    {
        //        List<CashTrxVM2> lst = new List<CashTrxVM2>();
        //        lst = Get_ReceivedTrx_DateDelimited(lst);


        //        return lst;
        //    }
        //}


        public List<CashTrxVM2> Get_Trx_DateDelimited(List<CashTrxVM2> lst)
        {
            DateParameter dateParam = new DateParameter();

            if (!CashTrx.IsNullOrEmpty())
            {
                foreach (var trx in CashTrx)
                {
                    if (dateParam.IsDateWithinBeginAndEndDatesInclusive(trx.Date, FromDate, ToDate))
                        lst.Add(trx);
                }
            }
            return lst;
        }

        //public List<CashTrxVM2> Get_PaidTrx_DateDelimited(List<CashTrxVM2> lst)
        //{
        //    DateParameter dateParam = new DateParameter();

        //    if (!CashTrx.IsNullOrEmpty())
        //    {
        //        foreach (var trx in CashTrx)
        //        {
        //            if (dateParam.IsDateWithinBeginAndEndDatesInclusive(trx.Date, FromDate, ToDate))
        //                lst.Add(trx);
        //        }
        //    }
        //    return lst;
        //}

        //public List<CashTrxVM2> Get_ReceivedTrx_DateDelimited(List<CashTrxVM2> lst)
        //{
        //    DateParameter dateParam = new DateParameter();

        //    if (!ReceivedTrx.IsNullOrEmpty())
        //    {
        //        foreach (var trx in ReceivedTrx)
        //        {
        //            if (dateParam.IsDateWithinBeginAndEndDatesInclusive(trx.Date, FromDate, ToDate))
        //                lst.Add(trx);
        //        }
        //    }
        //    return lst;
        //}
        public decimal FinalBalance
        {
            get
            {
                decimal finalBalance = BroughtForward;
                if (CashTrxs_DateDelimited.IsNullOrEmpty())
                    return finalBalance;
                foreach (CashTrxVM2 trx in CashTrxs_DateDelimited)
                {
                    finalBalance += trx.ReceiptAmount_ForTotal - trx.PaymentAmount_ForTotal;
                }
                return finalBalance;
            }
        }

        public decimal TotalPaid
        {
            get
            {
                decimal _ttlPaid = 0;
                if (!CashTrxs_DateDelimited.IsNullOrEmpty())
                {
                    foreach (var trx in CashTrxs_DateDelimited)
                    {
                        //in paid you have to let allocated go otherwise
                        //it becomes part of the balance if you hide it.
                        //if (trx.CashStateEnum != CashStateENUM.Allocated)
                        _ttlPaid += trx.PaymentAmount_ForTotal;
                    }
                }
                return _ttlPaid + PaidBF;
            }
        }

        public decimal TotalRecieved
        {
            get
            {
                decimal _totalReceived = 0;
                if (!CashTrxs_DateDelimited.IsNullOrEmpty())
                {
                    foreach (var trx in CashTrxs_DateDelimited)
                    {
                        if (trx.CashStateEnum != CashStateENUM.Allocated)
                            _totalReceived += trx.ReceiptAmount_ForTotal;
                    }
                }
                return _totalReceived + RecievedBF;

            }
        }



    }
}
