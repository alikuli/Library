using EnumLibrary.EnumNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS.AddressVerificationHdrNS
{
    [ComplexType]
    public class AddressVerificationStatus
    {

        [Display(Name = "Date Requested (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateRequested { get; set; }
        public DateTime DateRequested_NotNull { get { return DateRequested ?? DateTime.MinValue; } }



        [Display(Name = "Date Verification Selected For Processing(UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateSelectedForProcessing { get; set; }
        public DateTime DateSelectedForProcessing_NotNull { get { return DateSelectedForProcessing ?? DateTime.MinValue; } }



        [Display(Name = "Date Verification Printed (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DatePrinted { get; set; }
        public DateTime DatePrinted_NotNull { get { return DatePrinted ?? DateTime.MinValue; } }



        [Display(Name = "Date Verification Mailed (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateMailed { get; set; }
        public DateTime DateMailed_NotNull { get { return DateMailed ?? DateTime.MinValue; } }



        [Display(Name = "Date Verification Verified (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateVerified { get; set; }
        public DateTime DateVerified_NotNull { get { return DateVerified ?? DateTime.MinValue; } }



        [Display(Name = "Date Verification Failed (UTC)")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateFailed { get; set; }
        public DateTime DateFailed_NotNull { get { return DateFailed ?? DateTime.MinValue; } }

        
        
        public VerificaionStatusENUM VerificaionStatusEnum { get; set; }

        public void SetStatustTo(VerificaionStatusENUM verificaionStatusEnum)
        {
            VerificaionStatusEnum = verificaionStatusEnum;

            switch (verificaionStatusEnum)
            {
                case VerificaionStatusENUM.NotVerified:
                    break;

                case VerificaionStatusENUM.Requested:
                    DateRequested = DateTime.UtcNow;
                    break;
                
                case VerificaionStatusENUM.SelectedForProcessing:
                    DateSelectedForProcessing = DateTime.UtcNow;
                    break;
                
                case VerificaionStatusENUM.Printed:
                    DatePrinted = DateTime.UtcNow;
                    break;
                
                case VerificaionStatusENUM.Mailed:
                    DateMailed = DateTime.UtcNow;
                    break;
                
                case VerificaionStatusENUM.Verified:
                    DateVerified = DateTime.UtcNow;
                    break;
                
                case VerificaionStatusENUM.Failed:
                    DateFailed = DateTime.UtcNow;
                    break;
                
                case VerificaionStatusENUM.Unknown:
                default:
                    throw new Exception(string.Format("Unable to proccess this status {0}", verificaionStatusEnum.ToString().ToTitleSentance()));
            }              
        }




    }
}
