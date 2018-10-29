﻿using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    [ComplexType]
    public class Verification : IVerification
    {
        public Verification()
            : this("")
        {
            RequestDate = new DateAndByComplex();
            AcceptDate = new DateAndByComplex();
            PrintedDate = new DateAndByComplex();
            FailedDate = new DateAndByComplex();
            VerifiedDate = new DateAndByComplex();
            MailedDate = new DateAndByComplex();
            ProccessExpirationDate = new DateAndByComplex();
        }
        public Verification(string userName)
        {
            UserName = userName;
        }

        [Display(Name = "Verification Number")]
        public long VerificationNumber { get; set; }

        [Display(Name = "User Name")]
        private string UserName { get; set; }

        [Display(Name = "Status")]
        public VerificaionStatusENUM VerificaionStatusEnum { get; set; }

        public string VerificaionStatusEnumToString
        {
            get
            {
                return VerificaionStatusEnum.ToString().ToTitleSentance();
            }
        }

        [Display(Name = "Requested Info")]
        public DateAndByComplex RequestDate { get; set; }

        [Display(Name = "Accepted Info")]
        public DateAndByComplex AcceptDate { get; set; }

        [Display(Name = "Printed Info")]
        public DateAndByComplex PrintedDate { get; set; }

        [Display(Name = "Failed Info")]
        public DateAndByComplex FailedDate { get; set; }

        [Display(Name = "Verified Info")]
        public DateAndByComplex VerifiedDate { get; set; }

        [Display(Name = "Mailed Info")]
        public DateAndByComplex MailedDate { get; set; }

        //this updates the correct verification.
        public void UpdateDateFor(VerificaionStatusENUM addressVerificaionEnum)
        {
            VerificaionStatusEnum = addressVerificaionEnum;

            switch (addressVerificaionEnum)
            {


                case VerificaionStatusENUM.Failed:
                    FailedDate.SetToTodaysDate(UserName);
                    break;

                case VerificaionStatusENUM.Printed:
                    PrintedDate.SetToTodaysDate(UserName);
                    break;

                case VerificaionStatusENUM.Mailed:
                    MailedDate.SetToTodaysDate(UserName);
                    break;

                case VerificaionStatusENUM.Requested:
                    RequestDate.SetToTodaysDate(UserName);
                    break;

                case VerificaionStatusENUM.SelectedForProcessing:
                    AcceptDate.SetToTodaysDate(UserName);
                    break;

                case VerificaionStatusENUM.Verified:
                    VerifiedDate.SetToTodaysDate(UserName);
                    break;

                case VerificaionStatusENUM.NotVerified:
                case VerificaionStatusENUM.Unknown:
                default:
                    break;
            }
        }

        public void UpdateProccessExpirationDate(int noOfDays)
        {
            ProccessExpirationDate.SetToTodaysDate(UserName);
        }

        [Display(Name = "Proccess Expiration")]
        public DateAndByComplex ProccessExpirationDate { get; set; }
    }
}
