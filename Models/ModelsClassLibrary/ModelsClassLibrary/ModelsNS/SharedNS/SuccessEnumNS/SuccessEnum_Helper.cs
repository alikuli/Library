using AliKuli.Extentions;
using ConfigManagerLibrary;
using EnumLibrary.EnumNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelsClassLibrary.ModelsNS.SharedNS.SuccessEnumNS
{
    public class SuccessEnum_Helper
    {

        readonly ICollection<IHasSuccessEnum> _trx;
        readonly IHaveBeginAndEndDates _iHaveBeginAndEndDates;
        public SuccessEnum_Helper(ICollection<IHasSuccessEnum> trx, IHaveBeginAndEndDates iHaveBeginAndEndDates)
        {
            _trx = trx;
            _iHaveBeginAndEndDates = iHaveBeginAndEndDates;
        }


        public SuccessENUM SuccessStatus
        {
            get
            {
                //if Mail is within dates
                DateParameter dateParam = new DateParameter();
                dateParam.BeginDate = _iHaveBeginAndEndDates.BeginDate.Date ?? DateTime.MinValue;
                dateParam.EndDate = _iHaveBeginAndEndDates.EndDate.Date ?? DateTime.MaxValue ;

                if (dateParam.IsDateWithinBeginAndEndDatesInclusive(DateTime.UtcNow))
                    return SuccessENUM.InProccess;

                //if we are here, the time window has expired.

                return SuccessENUM.Successful;
            }
        }

        public double TotalPctRequiredForSuccess()
        {
            return VerificationConfig.SuccessPercentage;

        }
        public int TotalVerifications
        {
            get
            {
                if (_trx.IsNull())
                    return 0;
                return _trx.Count();
            }
        }

        public int TotalSuccessful
        {
            get
            {
                if (_trx.IsNull())
                    return 0;
                return _trx.Where(x => x.SuccessEnum == SuccessENUM.Successful).Count();

            }
        }

        public double TotalSuccessfulPct
        {
            get
            {
                if (TotalVerifications == 0)
                    return 0;

                if (TotalSuccessful == 0)
                    return 0;

                return TotalSuccessful / TotalVerifications;

            }
        }




        public int TotalUnsuccessful
        {
            get
            {
                if (_trx.IsNull())
                    return 0;
                return _trx.Where(x => x.SuccessEnum == SuccessENUM.Unsuccessful).Count();

            }
        }


        public double TotalUnsuccessfulPct
        {
            get
            {
                if (TotalVerifications == 0)
                    return 0;

                if (TotalUnsuccessful == 0)
                    return 0;

                return TotalUnsuccessful / TotalVerifications;

            }
        }


        public int TotalInprocess
        {
            get
            {
                if (_trx.IsNull())
                    return 0;
                return _trx.Where(x => x.SuccessEnum == SuccessENUM.InProccess).Count();

            }
        }

        public double TotalInprocessPct
        {
            get
            {
                if (TotalVerifications == 0)
                    return 0;

                if (TotalInprocess == 0)
                    return 0;

                return TotalInprocess / TotalVerifications;

            }
        }



        public int TotalUnknown
        {
            get
            {
                if (_trx.IsNull())
                    return 0;
                return _trx.Where(x => x.SuccessEnum == SuccessENUM.Unknown).Count();

            }
        }

        public double TotalUnknownPct
        {
            get
            {
                if (TotalVerifications == 0)
                    return 0;

                if (TotalUnknown == 0)
                    return 0;

                return TotalUnknown / TotalVerifications;

            }
        }


















        //public int TotalVerifications
        //{

        //    if (trx.IsNull())
        //        return 0;

        //    var successTrx = trx.Cast<IHasSuccessEnum>().ToList();

        //    if (successTrx.IsNull())
        //        return 0;

        //    return successTrx.Count();
        //}

        //public int TotalSuccessful (ICollection<ICommonWithId> trx)
        //{
        //    if (trx.IsNull())
        //        return 0;

        //    var successTrx = trx.Cast<IHasSuccessEnum>().ToList();

        //    if (successTrx.IsNull())
        //        return 0;

        //    return successTrx.Where(x => x.SuccessEnum == SuccessENUM.Successful).Count();

        //}

        //public double TotalSuccessfulPct  (ICollection<ICommonWithId> trx)
        //{
        //    if (TotalVerifications(trx) == 0)
        //        return 0;

        //    if (TotalSuccessful(trx) == 0)
        //        return 0;


        //    return TotalSuccessful / TotalVerifications;

        //}




        //public int TotalUnsuccessful  (ICollection<ICommonWithId> trx)
        //{
        //        if (AddressVerificationTrx.IsNull())
        //            return 0;
        //        return AddressVerificationTrx.Where(x => x.SuccessEnum == SuccessENUM.Unsuccessful).Count();

        //}




        //public int TotalInprocess  (ICollection<ICommonWithId> trx)
        //{
        //        if (AddressVerificationTrx.IsNull())
        //            return 0;
        //        return AddressVerificationTrx.Where(x => x.SuccessEnum == SuccessENUM.Inproccess).Count();

        //}

        //public double TotalInprocessPct  (ICollection<ICommonWithId> trx)
        //{
        //        if (TotalVerifications == 0)
        //            return 0;

        //        if (TotalInprocess == 0)
        //            return 0;

        //        return TotalInprocess / TotalVerifications;

        //}



        //public int TotalUnknown  (ICollection<ICommonWithId> trx)
        //{
        //    get
        //    {
        //        if (AddressVerificationTrx.IsNull())
        //            return 0;
        //        return AddressVerificationTrx.Where(x => x.SuccessEnum == SuccessENUM.Unknown).Count();

        //    }
        //}

        //public double TotalUnknownPct(ICollection<ICommonWithId> trx)
        //{
        //        if (TotalVerifications == 0)
        //            return 0;

        //        if (TotalUnknown == 0)
        //            return 0;

        //        return TotalUnknown / TotalVerifications;

        //}

    }
}
