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
        }
        public long BatchNo {get;set;}
        public override string FullName()
        {
            string fullName = string.Format("{0} -Status: {1} ({4}),  Success Window: {2} to: {3} ",
                Name,
                SuccessEnum.ToString().ToTitleCase(),
                BeginDate.Date_NotNull.ToShortDateString(),
                EndDate.Date_NotNull.ToShortDateString(),
                IsWithinDate ? "Open" : "Closed"
                );
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


        }

        /// <summary>
        /// These are the transactons
        /// </summary>
        public virtual ICollection<AddressVerificationTrx> AddressVerificationTrxs { get; set; }

        public DateAndByComplex BeginDate { get; set; }
        public DateAndByComplex EndDate { get; set; }


        public bool IsWithinDate
        {
            get
            {
                DateParameter dateParm = new DateParameter();
                dateParm.BeginDate = BeginDate.Date_NotNull;
                dateParm.EndDate = EndDate.Date_NotNull;
                return dateParm.IsDateWithinBeginAndEndDatesInclusive(DateTime.UtcNow);
            }
        }


        public string MailerId { get; set; }
        public virtual Mailer Mailer { get; set; }


        public void AddBeginAndEndDateController(MailLocalOrForiegnENUM MailLocalOrForiegnEnum, MailServiceENUM MailServiceEnum, string userName)
        {
            switch (MailLocalOrForiegnEnum)
            {
                case MailLocalOrForiegnENUM.InPakistan:
                    switch (MailServiceEnum)
                    {
                        case MailServiceENUM.Post:
                            AddBeginEndDate_Pakistan_Post(userName);
                            break;

                        case MailServiceENUM.Courier:
                            AddBeginEndDate_Pakistan_Courier(userName);
                            break;


                        default:
                            throw new Exception("Error in switch MailLocalOrForiegnENUM.Local . No such option!");
                    }
                    break;

                case MailLocalOrForiegnENUM.OutOfPakistan:
                    switch (MailServiceEnum)
                    {
                        case MailServiceENUM.Post:
                            AddBeginEndDate_Foreign_Post(userName);
                            break;


                        case MailServiceENUM.Courier:
                            AddBeginEndDate_Foreign_Courier(userName);
                            break;

                        default:
                            throw new Exception("Error in switch MailLocalOrForiegnENUM.Foriegn. No such option!");
                    }
                    break;

                default:
                    throw new Exception("Error in main switch. No such option!");

            }

        }
        private void AddBeginEndDate_Pakistan_Post(string userName)
        {
            int noOfDaysAllowed = VerificationConfig.Number_Of_Days_Allowed_For_Local_Post;
            BeginDate.SetToTodaysDate(userName);
            EndDate.SetDateTo(userName, noOfDaysAllowed);
        }
        private void AddBeginEndDate_Pakistan_Courier(string userName)
        {
            int noOfDaysAllowed = VerificationConfig.Number_Of_Days_Allowed_For_Local_Courier;
            BeginDate.SetToTodaysDate(userName);
            EndDate.SetDateTo(userName, noOfDaysAllowed);

        }

        private void AddBeginEndDate_Foreign_Post(string userName)
        {
            int noOfDaysAllowed = VerificationConfig.Number_Of_Days_Allowed_For_Foreign_Post;
            BeginDate.SetToTodaysDate(userName);
            EndDate.SetDateTo(userName, noOfDaysAllowed);

        }
        private void AddBeginEndDate_Foreign_Courier(string userName)
        {
            int noOfDaysAllowed = VerificationConfig.Number_Of_Days_Allowed_For_Foreign_Courier;
            BeginDate.SetToTodaysDate(userName);
            EndDate.SetDateTo(userName, noOfDaysAllowed);

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
        public MailLocalOrForiegnENUM MailLocalOrForiegnEnum  { get; set; }
        public MailServiceENUM MailServiceEnum  { get; set; }


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
                return AddressVerificationTrxs.Where(x => x.VerificaionStatusEnum == VerificaionStatusENUM.Verified).Count();

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
                return AddressVerificationTrxs.Where(x => x.VerificaionStatusEnum == VerificaionStatusENUM.Failed).Count();

            }
        }




        public int TotalInprocess
        {
            get
            {
                if (AddressVerificationTrxs.IsNull())
                    return 0;

                return AddressVerificationTrxs.Where(x =>
                    x.VerificaionStatusEnum == VerificaionStatusENUM.Mailed ||
                    x.VerificaionStatusEnum == VerificaionStatusENUM.Printed ||
                    x.VerificaionStatusEnum == VerificaionStatusENUM.SelectedForProcessing).Count();

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
                    x.VerificaionStatusEnum == VerificaionStatusENUM.Unknown).Count();

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
