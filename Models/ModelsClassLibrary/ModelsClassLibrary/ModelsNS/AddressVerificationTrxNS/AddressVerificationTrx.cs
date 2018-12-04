using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS
{
    /// <summary>
    /// A verification is complete if it's status is Verified or VerificationFailed.
    /// </summary>
    public class AddressVerificationTrx : CommonWithId
    {
        public AddressVerificationTrx()
        {
            Verification = new Verification();
            Verification.VerificaionStatusEnum = VerificaionStatusENUM.NotVerified;
            //SuccessEnum = SuccessENUM.Unknown;

        }
        public long LetterNo { get; set; }
        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.AddressVerificationTrx;
        }


        [Display(Name = "Mail Service")]
        public MailServiceENUM MailServiceEnum { get; set; }
        public SelectList MailServiceEnumSelectList { get { return EnumExtention.ToSelectListSorted<MailServiceENUM>(MailServiceENUM.Unknown); } }



        /// <summary>
        /// When the record is saved, the system updates this and sets it to local or 
        /// foreign depending on if he country is Pakistan or not
        /// </summary>
        /// 
        [Display(Name = "Local or Foreign")]
        public MailLocalOrForiegnENUM MailLocalOrForiegnEnum { get; set; }
        public SelectList MailLocalOrForiegnEnumSelectList { get { return EnumExtention.ToSelectListSorted<MailLocalOrForiegnENUM>(MailLocalOrForiegnENUM.Unknown); } }





        public SelectList VerificaionStatusEnumSelectList { get { return EnumExtention.ToSelectListSorted<VerificaionStatusENUM>(VerificaionStatusENUM.Unknown); } }






        [Display(Name = "Date Verification Accepted (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateVerifcationPaymentAccepted { get; set; }
        public DateTime DateVerifcationPaymentAccepted_NotNull { get { return DateVerifcationPaymentAccepted ?? DateTime.MinValue; } }




        /// <summary>
        /// This is where we know if the address is verified. The last verificationTrx is used.
        /// </summary>
        [Display(Name = "Verification Status")]
        public Verification Verification { get; set; }

        public bool IsComplete
        {
            get
            {
                return (
                    Verification.VerificaionStatusEnum == VerificaionStatusENUM.Failed ||
                    Verification.VerificaionStatusEnum == VerificaionStatusENUM.Verified);
            }
        }


        [Display(Name = "Address")]
        public string AddressId { get; set; }
        public virtual AddressMain Address { get; set; }





        ///// <summary>
        ///// I had to create this to use in a linq expression
        ///// </summary>
        //[NotMapped]
        //public string PersonIdOfUser { get; set; }











        //[Display(Name = "Verification No")]
        //public long VerificationNumber { get; set; }


        [Display(Name = "Header")]
        public virtual string AddressVerificationHdrId { get; set; }
        public virtual AddressVerificationHdr AddressVerificationHdr { get; set; }


        public override void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {
            base.UpdatePropertiesDuringModify(icommonWithId);
            AddressVerificationTrx addtrx = icommonWithId as AddressVerificationTrx;

            MailServiceEnum = addtrx.MailServiceEnum;
            MailLocalOrForiegnEnum = addtrx.MailLocalOrForiegnEnum;
            Verification = addtrx.Verification;
            //SuccessEnum = addtrx.SuccessEnum;
            AddressId = addtrx.AddressId;
            AddressVerificationHdrId = addtrx.AddressVerificationHdrId;
            DateVerifcationPaymentAccepted = addtrx.DateVerifcationPaymentAccepted;
            //VerificationNumber = addtrx.VerificationNumber;
            LetterNo = addtrx.LetterNo;

        }
    }
}
