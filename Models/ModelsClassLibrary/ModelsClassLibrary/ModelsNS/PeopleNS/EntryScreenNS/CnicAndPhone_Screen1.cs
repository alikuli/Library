using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ModelsClassLibrary.ModelsNS.SharedNS;

namespace ModelsClassLibrary.ModelsNS.People.EntryScreen
{
    [NotMapped]
    public class CnicAndPhone_Screen1:CommonWithId
    {
        [Required]
        [Display(Name = "Country")]
        public long CountryId { get; set; }

        //[Required]
        //[Display(Name = "Area Code")]
        //[MaxLength(4, ErrorMessage = "Max length allowed is {0} charecters")]
        //public string PhoneCode { get; set; }


        [Required]
        [Display(Name = "Phone number")]
        [MaxLength(20, ErrorMessage = "Max length allowed is {0} charecters")]
        public string PhoneNumber { get; set; }


        [Required]
        [Display(Name = "CNIC")]
        [MaxLength(20, ErrorMessage = "Max length allowed is {0} charecters")]
        public string IdentificationNo { get; set; }

        [MaxLength(50, ErrorMessage = "Max length allowed is {0} charecters")]
        public string Email { get; set; }

    }
}