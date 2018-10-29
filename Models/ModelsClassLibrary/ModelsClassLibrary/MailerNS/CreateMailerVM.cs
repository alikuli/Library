using AliKuli.Extentions;
using EnumLibrary.EnumNS.VerificationNS;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS
{
    public class CreateMailerVM
    {
        [Display(Name = "User")]
        [Required]
        public string UserId { get; set; }

        ///// <summary>
        ///// This is used to get the name of the user to display from controller.
        ///// </summary>
        //public ApplicationUser User { get; set; }

        public SelectList UserSelect { get; set; }
        public double CurrentAccountBalance { get; set; }
        public TrustLevelENUM TrustLevelEnum { get; set; }
        public SelectList TrustLevelSelectList
        {
            get
            {
                return EnumExtention.ToSelectListSorted<TrustLevelENUM>(TrustLevelENUM.Unknown);
            }
        }

    }
}
