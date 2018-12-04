
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    public class AddressVerificationRequest
    {
        public AddressVerificationRequest()
        {
            //MailServiceEnum = MailServiceENUM.Post;
            IsSure1 = false;
        }

        [Display(Name = "Mailing Address")]
        public string AddressMailForm { get; set; }

        public string CourierInternationalPayment { get; set; }
        public string PostalInternationalPayment { get; set; }

        public string CourierLocalPayment { get; set; }
        public string PostalLocalPayment { get; set; }

        public string AddressId { get; set; }
        public string CountryId { get; set; }


        [Display(Name = "I accept the Payment will be deducted from my account.")]
        public bool IsSure1 { get; set; }
        public DateTime DateIsSure1 { get; set; }


        [Display(Name = "Please deduct payment from my account.")]
        public bool IsSure2 { get; set; }
        public DateTime DateIsSure2 { get; set; }

        public bool IsLocal { get; set; }
        [Display(Name = "Payment Amount")]
        public double PaymentAmount { get; set; }



        [Display(Name = "Type of Service")]
        public MailServiceENUM MailServiceEnum { get; set; }

        [Display(Name = "In Pakistan / Foreign")]
        public MailLocalOrForiegnENUM MailLocalOrForeignEnum { get; set; }

        //public string MailServiceEnumToString { get { return MailServiceEnum.ToString().ToTitleSentance(); } }
        public SelectList MailServiceEnumSelectList
        {
            get
            {
                return EnumExtention.ToSelectListSorted<MailServiceENUM>(MailServiceENUM.Post);
            }
        }

        public string CourierBtnCaption { get; set; }
        public string PostalBtnCaption { get; set; }

        public string PaymentConfirmationString
        {
            get
            {
                return string.Format("Your mail will be sent by <strong>{0}</strong> for a cost of <strong>PKRS {1}</strong> which will be deducted from your account.", MailServiceEnum.ToString().ToTitleSentance(), PaymentAmount);
            }
        }

        public string Confirmation2_CheckBox
        {
            get
            {
                return string.Format("Please deduct - <strong>PKRS {0}</strong> - from my account.", PaymentAmount);
            }
        }
    }
}
