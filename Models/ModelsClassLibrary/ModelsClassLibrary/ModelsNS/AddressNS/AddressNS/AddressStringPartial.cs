//using System.ComponentModel.DataAnnotations;
//using InterfacesLibrary.AddressNS;

//namespace ModelsClassLibrary.ModelsNS.AddressNS
//{
//    public abstract class AddressStringPartial : IAddressStringPartial
//    {





//        [MaxLength(1000, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "House#")]
//        public string HouseNo { get; set; }


//        [MaxLength(1000, ErrorMessage = "Max length allowed is {0} charecters")]
//        public string Road { get; set; }

//        [MaxLength(1000, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "Extra Info")]
//        public string Address2 { get; set; }

//        [MaxLength(1000, ErrorMessage = "Max length allowed is {1} charecters")]
//        [Display(Name = "Web Site")]
//        public string WebAddress { get; set; }


//        [MaxLength(1000, ErrorMessage = "Max length allowed is {1} charecters")]
//        [Display(Name = "Zip/Postal#")]
//        public string Zip { get; set; }


//        //[Display(Name = "Email Addresses")]
//        //[MaxLength(100, ErrorMessage = "Max length allowed is {1} charecters")]
//        //public string Email { get; set; }


//        [Display(Name = "Phone")]
//        [MaxLength(100, ErrorMessage = "Max length allowed is {1} charecters")]
//        public string Phone { get; set; }



//        [MaxLength(1000, ErrorMessage = "Max length allowed is {1} charecters")]
//        [Display(Name = "Attention")]
//        public string Attention { get; set; }



//        public void LoadFrom(IAddressStringPartial iaddressStringPartial)
//        {
//            Address2 = iaddressStringPartial.Address2;
//            Attention = iaddressStringPartial.Attention;
//            HouseNo = iaddressStringPartial.HouseNo;
//            Phone = iaddressStringPartial.Phone;
//            Road = iaddressStringPartial.Road;
//            WebAddress = iaddressStringPartial.WebAddress;
//            Zip = iaddressStringPartial.Zip;
//        }

//    }
//}