using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationHdrNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsClassLibrary.ModelsNS.MailerNS
{
    [NotMapped]
    public class MailingCostsVM
    {
        public string AddressVerificationHdrId { get; set; }
        public AddressVerificationHdr AddressVerificationHdr { get; set; }

        [Required]
        [Display(Name = "What was your total mailing cost at postal service?")]
        public double Cost { get; set; }

        [Required]
        [Display(Name = "How many letters did you mail?")]
        public int TotalLettersMailed { get; set; }

    }
}
