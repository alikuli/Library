using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ModelsNS.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }


    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        //[Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }


        //[Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        public string CountryId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public SelectList CountrySelectList { get; set; }
    }




    public class RegisterViewModel
    {
        //public RegisterViewModel()
        //{
        //    SonOfOrWifeOf = SonOfWifeOfDotOfENUM.Unknown;
        //    Sex = SexENUM.Unknown;

        //}

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        ////[Required]
        //[Phone]
        //[Display(Name = "Phone")]
        //public string Phone { get; set; }


        ////[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Display(Name = "Country Id Card #")]
        //public string CountryIdCardNumber { get; set; }

        //public string CountryID { get; set; }
        ////public virtual Country Country { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public PersonComplex Person { get; set; }
        public AddressComplex Address { get; set; }

        //[Display(Name = "First Name")]
        //public string FName { get; set; }


        //[Display(Name = "Middle Name")]
        //public string MName { get; set; }


        //[Display(Name = "Last Name")]
        //public string LName { get; set; }

        //public SexENUM Sex { get; set; }

        //[Display(Name = "Name Of Father/Husband")]
        //public string NameOfFatherOrHusband { get; set; }


        //[Display(Name = "Son Of/Wife Of")]
        //public SonOfWifeOfDotOfENUM SonOfOrWifeOf { get; set; }


        //[Display(Name = "Country Id Card Number")]
        //public string CountryIdCardNumber { get; set; }

        //[Display(Name = "House #")]
        //public string HouseNo { get; set; }

        //[Display(Name = "Road")]
        //public string Road { get; set; }


        //[Display(Name = "Address 2")]
        //public string Address2 { get; set; }


        //[Display(Name = "Town")]
        //public string TownId { get; set; }
        //public virtual Town Town { get; set; }


        //[Display(Name = "City")]
        //public string CityName { get; set; }

        //[Display(Name = "Province")]
        //public string StateName { get; set; }



        //[Display(Name = "Country")]
        //public string CountryName{ get; set; }

        //[Display(Name = "Attention")]
        //public string Attention { get; set; }

        //[Display(Name = "Web")]
        //public string WebAddress { get; set; }

        //[Display(Name = "Zip")]
        //public string Zip { get; set; }

        public IEnumerable<SelectListItem> CountrySelectList { get; set; }


    }


    /// <summary>
    /// This gets the person's information
    /// </summary>
    public class RegisterViewModelPerson
    {
        public string IdentificationNo { get; set; }
        public string FName { get; set; }

        public string LName { get; set; }

        public string MName { get; set; }
        public SexENUM Sex { get; set; }

        public SonOfWifeOfDotOfENUM SonOfOrWifeOf { get; set; }
        public string NameOfFatherOrHusband { get; set; }


    }

    public class RegisterViewModelAddress
    {
        [MaxLength(1000, ErrorMessage = "Max length allowed is {0} charecters")]
        [Display(Name = "House#")]
        public string HouseNo { get; set; }

        [MaxLength(1000, ErrorMessage = "Max length allowed is {0} charecters")]
        [Display(Name = "Road")]
        public string Road { get; set; }

        [MaxLength(1000, ErrorMessage = "Max length allowed is {0} charecters")]
        [Display(Name = "Extra Info")]
        public string Address2 { get; set; }


        [MaxLength(1000, ErrorMessage = "Max length allowed is {1} charecters")]
        [Display(Name = "Zip/Postal#")]
        public string Zip { get; set; }


        [MaxLength(1000, ErrorMessage = "Max length allowed is {1} charecters")]
        [Display(Name = "Web Site")]
        public string WebAddress { get; set; }

        [Display(Name = "Email Addresses")]
        [MaxLength(100, ErrorMessage = "Max length allowed is {1} charecters")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [MaxLength(100, ErrorMessage = "Max length allowed is {1} charecters")]
        public string Phone { get; set; }

        [MaxLength(1000, ErrorMessage = "Max length allowed is {1} charecters")]
        [Display(Name = "Attention")]
        public string Attention { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
    }
}
