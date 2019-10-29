using AliKuli.Extentions;
using ConfigManagerLibrary;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.SuccessEnumNS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS
{
    public partial class AddressVerificationHdr : CommonWithId, IHaveBeginAndEndDates
    {
        public AddressVerificationHdr()
        {
            BeginDate = new DateAndByComplex();
            EndDate = new DateAndByComplex();
            Verification = new Verification();
        }
        public long BatchNo { get; set; }

        public override string FullName()
        {
            int ttl = Total_Verifications_Qty;


            string fullName = string.Format("{0} -Status: {1} ({4}: {2} to: {3}) [Qty = {5} {6}] Verified Pct = {7:0.00}%",
                Name,
                Verification.VerificaionStatusEnum.ToString().ToTitleSentance(),
                BeginDate.Date_NotNull_Min.ToShortDateString(),
                EndDate.Date_NotNull_Min.ToShortDateString(),
                IsWithinDate ? "Open" : "Closed",
                ttl,
                ttl == 1 ? "Letter" : "Letters",
                Successful_Verifications_Pct);
            return fullName;
        }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.AddressVerificationHdr;
        }

        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {

            base.UpdatePropertiesDuringModify(icommonWithId);
            AddressVerificationHdr hdr = icommonWithId as AddressVerificationHdr;

            BeginDate = hdr.BeginDate;
            EndDate = hdr.EndDate;
            MailerId = hdr.MailerId;
            BatchNo = hdr.BatchNo;

        }

        /// <summary>
        /// These are the transactons
        /// </summary>
        public virtual ICollection<AddressVerificationTrx> AddressVerificationTrxs { get; set; }

        public DateAndByComplex BeginDate { get; set; }
        public DateAndByComplex EndDate { get; set; }


        public double CostOfmailing { get; set; }
        public double BudgetedCost { get; set; }

        public int TotalQtyLettersMailed { get; set; }

        public bool IsWithinDate
        {
            get
            {
                DateParameter dateParm = new DateParameter();
                dateParm.BeginDate = BeginDate.Date_NotNull_Min;
                dateParm.EndDate = EndDate.Date_NotNull_Min;
                return dateParm.IsDateWithinBeginAndEndDatesInclusive(DateTime.UtcNow);
            }
        }


        public string MailerId { get; set; }
        public virtual Mailer Mailer { get; set; }


        public void AddBeginAndEndDateController(MailLocalOrForiegnENUM MailLocalOrForiegnEnum, MailServiceENUM MailServiceEnum, string userName, string userId)
        {
            switch (MailLocalOrForiegnEnum)
            {
                case MailLocalOrForiegnENUM.InPakistan:
                    switch (MailServiceEnum)
                    {
                        case MailServiceENUM.Post:
                            AddBeginEndDate_Pakistan_Post(userName, userId);
                            break;

                        case MailServiceENUM.Courier:
                            AddBeginEndDate_Pakistan_Courier(userName, userId);
                            break;


                        default:
                            throw new Exception("Error in switch MailLocalOrForiegnENUM.Local . No such option!");
                    }
                    break;

                case MailLocalOrForiegnENUM.OutOfPakistan:
                    switch (MailServiceEnum)
                    {
                        case MailServiceENUM.Post:
                            AddBeginEndDate_Foreign_Post(userName, userId);
                            break;


                        case MailServiceENUM.Courier:
                            AddBeginEndDate_Foreign_Courier(userName, userId);
                            break;

                        default:
                            throw new Exception("Error in switch MailLocalOrForiegnENUM.Foriegn. No such option!");
                    }
                    break;

                default:
                    throw new Exception("Error in main switch. No such option!");

            }

        }
        private void AddBeginEndDate_Pakistan_Post(string userName, string userId)
        {
            int noOfDaysAllowed = VerificationConfig.Number_Of_Days_Allowed_For_Local_Post;
            BeginDate.SetToTodaysDate(userName, userId);
            EndDate.SetDateTo(userName,userId, noOfDaysAllowed);
        }
        private void AddBeginEndDate_Pakistan_Courier(string userName, string userId)
        {
            int noOfDaysAllowed = VerificationConfig.Number_Of_Days_Allowed_For_Local_Courier;
            BeginDate.SetToTodaysDate(userName, userId);
            EndDate.SetDateTo(userName, userId, noOfDaysAllowed);

        }

        private void AddBeginEndDate_Foreign_Post(string userName, string userId)
        {
            int noOfDaysAllowed = VerificationConfig.Number_Of_Days_Allowed_For_Foreign_Post;
            BeginDate.SetToTodaysDate(userName, userId);
            EndDate.SetDateTo(userName, userId, noOfDaysAllowed);

        }
        private void AddBeginEndDate_Foreign_Courier(string userName, string userId)
        {
            int noOfDaysAllowed = VerificationConfig.Number_Of_Days_Allowed_For_Foreign_Courier;
            BeginDate.SetToTodaysDate(userName, userId);
            EndDate.SetDateTo(userName, userId, noOfDaysAllowed);

        }



        //All the success below are done for the current headers children.





        ///// <summary>
        ///// These are the number of incomplete mailings allowed at any given time.
        ///// </summary>
        //public int NumberOfOpenMailingsAllowedToMailer
        //{
        //    get
        //    {
        //        return MailersTrustLevelConfig.NumberOfOpenMailingsAllowed;
        //    }
        //}

        //private int TotalOpenMailingsForMailer
        //{
        //    get
        //    {
        //        int noOfOpenMailings = TotalInprocess + TotalSuccessful;
        //        return noOfOpenMailings;
        //    }
        //}

        ///// <summary>
        ///// This decides if mailer is allowed any more mailings.
        ///// </summary>
        //public bool IsMailerAllowedMoreMailings
        //{
        //    get
        //    {
        //        return NumberOfOpenMailingsAllowedToMailer >= TotalOpenMailingsForMailer;
        //    }
        //}

        //public SuccessENUM MailingSuccessStatus
        //{
        //    get
        //    {

        //    }
        //}


        //ICollection<IHasSuccessEnum> _trx;
        public SuccessENUM SuccessEnum
        {
            get
            {


                double requiredPctForSuccessPct = VerificationConfig.SuccessPercentage;

                if (TotalSuccessfulPct >= requiredPctForSuccessPct)
                {
                    return SuccessENUM.Successful;
                }
                else
                {
                    if (IsWithinDate)
                        return SuccessENUM.InProccess;

                    return SuccessENUM.Unsuccessful;
                }


            }
        }


        public int Successful_Verifications_Qty
        {
            get
            {
                if (AddressVerificationTrxs.IsNullOrEmpty())
                    return 0;

                int ttlVerified = AddressVerificationTrxs.Where(x => x.Verification.VerificaionStatusEnum == VerificaionStatusENUM.Verified).Count();
                return ttlVerified;
            }
        }

        public double Successful_Verifications_Pct
        {
            get
            {
                if (Total_Verifications_Qty == 0)
                    return 0;

                if (Successful_Verifications_Qty == 0)
                    return 0;
                double pctRate = Successful_Verifications_Qty / Total_Verifications_Qty * 100;

                return pctRate;
            }
        }
        public int Total_Verifications_Qty
        {
            get
            {
                if (AddressVerificationTrxs.IsNullOrEmpty())
                    return 0;
                return AddressVerificationTrxs.Count;
            }
        }

        public MailLocalOrForiegnENUM MailLocalOrForiegnEnum { get; set; }
        public MailServiceENUM MailServiceEnum { get; set; }

        public Verification Verification { get; set; }


        #region Totals

        public int TotalVerifications
        {
            get
            {
                if (AddressVerificationTrxs.IsNull())
                    return 0;
                return AddressVerificationTrxs.Count();
            }
        }

        public int TotalSuccessful
        {
            get
            {
                if (AddressVerificationTrxs.IsNull())
                    return 0;
                return AddressVerificationTrxs.Where(x => x.Verification.VerificaionStatusEnum == VerificaionStatusENUM.Verified).Count();

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
                if (AddressVerificationTrxs.IsNull())
                    return 0;
                return AddressVerificationTrxs.Where(x => x.Verification.VerificaionStatusEnum == VerificaionStatusENUM.Failed).Count();

            }
        }




        public int TotalInprocess
        {
            get
            {
                if (AddressVerificationTrxs.IsNull())
                    return 0;

                return AddressVerificationTrxs.Where(x =>
                    x.Verification.VerificaionStatusEnum == VerificaionStatusENUM.Mailed ||
                    x.Verification.VerificaionStatusEnum == VerificaionStatusENUM.Printed ||
                    x.Verification.VerificaionStatusEnum == VerificaionStatusENUM.SelectedForProcessing).Count();

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
                if (AddressVerificationTrxs.IsNull())
                    return 0;
                return AddressVerificationTrxs.Where(x =>
                    x.Verification.VerificaionStatusEnum == VerificaionStatusENUM.Unknown).Count();

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
        #endregion

    }
}
