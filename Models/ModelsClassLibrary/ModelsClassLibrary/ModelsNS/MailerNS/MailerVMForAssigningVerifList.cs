using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS
{
    public class MailerVMForAssigningVerifList
    {
        public MailerVMForAssigningVerifList()
        {
            MailLocalOrForiegnEnum = MailLocalOrForiegnENUM.Unknown;
            MailServiceEnum = MailServiceENUM.Unknown;
            AddressVerificationHdrList_InProcessOrPrinted = new List<AddressVerificationHdr>();

        }
        [Display(Name = "Pakistan / other")]
        public MailLocalOrForiegnENUM MailLocalOrForiegnEnum { get; set; }
        //public SelectList SelectList_MailLocalOrForiegnEnum { get { return EnumExtention.ToSelectListSorted<MailLocalOrForiegnENUM>(MailLocalOrForiegnENUM.OutOfPakistan); } }

        [Display(Name = "Service")]
        public MailServiceENUM MailServiceEnum { get; set; }
        //public SelectList SelectList_MailServiceEnum { get { return EnumExtention.ToSelectListSorted<MailServiceENUM>(MailServiceENUM.Courier); } }


        public string MailerId { get; set; }


        [Display(Name = "Total Open Mailings For Mailer")]
        public string Total_Open_Mailings_For_Mailer { get; set; }


        [Display(Name = "Enter Costs for these to qualify for your payments")]
        public string Total_Mails_Awaiting_Costs { get; set; }


        public ICollection<AddressVerificationHdr> AddressVerificationHdrList_InProcessOrPrinted { get; set; }
        //public ICollection<AddressVerificationHdr> AddressVerificationHdrList_Printed {get;set;}


        //these carry the amount of verifications that are available to
        //assist the mailer in choosing
        [Display(Name = "Pakistan Postal Verifications Available")]
        public int Pakistan_Postal_Verifications_Available { get; set; }


        [Display(Name = "Pakistan Courier Verifications Available")]
        public int Pakistan_Courier_Verifications_Available { get; set; }



        [Display(Name = "Foreign Postal Verifications Available")]
        public int Foreign_Postal_Verifications_Available { get; set; }



        [Display(Name = "Foreign Courier Verifications Available")]
        public int Foreign_Courier_Verifications_Available { get; set; }


    }
}
