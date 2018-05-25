//using AliKuli.Extentions;
//using ModelsClassLibrary.ModelsNS.CommonAndSharedNS;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Web;
//using EnumLibrary.EnumNS;
//using ErrorHandlerLibrary.ExceptionsNS;
//using ErrorHandlerLibrary.ExceptionsNS;

//namespace ModelsClassLibrary.ModelsNS.People.EntryScreen
//{
//    [NotMapped]
//    public class PersonWithAddress_Screen2 : CnicAndPhone_Screen1
//    {
//        //[Display(Name = "Address Name")]
//        //[MaxLength(200, ErrorMessage = "Max length allowed is {0} charecters")]
//        //public string Name { get; set; }

//        //[Display(Name = "CNIC")]
//        //[MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
//        //public string IdentificationNo { get; set; }

//        private readonly ErrorSet _err;

//        public PersonWithAddress_Screen2()
//        {
//            Initialize();
//            _err = new ErrorSet("ModelsClassLibrary", "PersonWithAddress_Screen2", "");
//        }

//        public PersonWithAddress_Screen2(CnicAndPhone_Screen1 cs)
//        {

//            IdentificationNo = cs.IdentificationNo;
//            PhoneNumber = cs.PhoneNumber;
//            CountryId = cs.CountryId;
//            Created.DateStart = cs.Created.DateStart;
//            Initialize();
//        }

//        public override void Initialize()
//        {
            

//            DisabilatiesENUM = YesNoENUM.Unknown;
//            Sex = SexENUM.Unknown;
//            BoardingStatus = BoardingENUM.Unknown;
//            SonOfOrWifeOf = SonOfWifeOfDotOfENUM.Unknown;
//        }

//        public string CountryNameString { get; set; }

//        [MaxLength(20, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "Enter SMS code.")]
//        [Required]
//        public string SmsCodeNumber { get; set; }


//        //[MaxLength(20, ErrorMessage = "Max length allowed is {0} charecters")]
//        //[Display(Name = "Scratch Card No.")]
//        //[DataType(DataType.Password)]
//        //public long ScratchCardNumberIdNo { get; set; }

//        /// REQUIRED
//        [MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "F-Name")]
//        public string FName { get; set; }




//        [MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "M-Name")]
//        public string MName { get; set; }





//        [MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "L-Name")]
//        public string LName { get; set; }


//        public SexENUM Sex { get; set; }





//        [Display(Name = "S/O W/O")]
//        public SonOfWifeOfDotOfENUM SonOfOrWifeOf { get; set; }




//        [Display(Name = "Name of S/O-W/O")]
//        [MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
//        public string NameOfFatherOrHusband { get; set; }




//        [Display(Name = "Tax Code")]
//        public string TaxType { get; set; }



//        [Display(Name = "Needs Boarding")]

//        public BoardingENUM BoardingStatus { get; set; }


//        //----------------------------------------------------------- Address ----

//        [MaxLength(200, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "House#")]
//        public string HouseNo { get; set; }



//        [MaxLength(200, ErrorMessage = "Max length allowed is {0} charecters")]
//        public string Road { get; set; }


//        [MaxLength(200, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "Extra Info")]
//        public string Address2 { get; set; }


//        [Display(Name = "Town")]
//        public long? TownID { get; set; }


//        [MaxLength(200, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "Zip/Postal#")]
//        public string Zip { get; set; }


//        //[Display(Name = "Phone")]
//        //[MaxLength(20, ErrorMessage = "Max length allowed is {0} charecters")]
//        //public string ContactPhone { get; set; }


//        [MaxLength(200, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "Email")]
//        public string ContactEmail { get; set; }


//        [MaxLength(200, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "Web Site")]
//        public string ContactWeb { get; set; }

//        public YesNoENUM DisabilatiesENUM { get; set; }

//        [MaxLength(500, ErrorMessage = "Max length allowed is {0} charecters")]
//        [Display(Name = "List Disabilities")]
//        public string Disabilities { get; set; }

//        [Display(Name = "No of Children")]
//        public int NumberOfChildren { get; set; }

//        [Display(Name = "No of Other Dependants living with him")]
//        public int OtherDependants { get; set; }


//        [Display(Name = "Monthly Salary Expected")]
//        public long MonthlySalaryExpected { get; set; }



//        [DataType(DataType.Upload)]
//        [Display(Name = "Front ID Picture")]
//        public HttpPostedFileBase FrontIdPictureUpload { get; set; }
//        public string FrontIdPictureUploadUrl { get; set; }



//        [DataType(DataType.Upload)]
//        [Display(Name = "Back ID Picture")]
//        public HttpPostedFileBase BackIdPictureUpload { get; set; }
//        public string BackIdPictureUploadUrl { get; set; }



//        [DataType(DataType.Upload)]
//        [Display(Name = "Front Face Picture")]
//        public HttpPostedFileBase FrontFacePictureUpload { get; set; }
//        public string FrontFacePictureUploadUrl { get; set; }

//        [DataType(DataType.Upload)]
//        [Display(Name = "Side Face Picture")]
//        public HttpPostedFileBase SideFacePictureUpload { get; set; }
//        public string SideFacePictureUploadUrl { get; set; }

//        public override void SelfErrorCheck()
//        {
//            //base.SelfErrorCheck(); //We do not want a name check.

//            //Make sure SMS Code has been received.
//            if (SmsCodeNumber.IsNullOrEmpty())
//            {
//                string error = "No sms code received. ";
//                _err.Add(error, "SelfErrorCheck");
//            }

//            //ScratchcardIdNo 
//            //if (ScratchCardNumberIdNo == 0)
//            //{
//            //    string error = "No Scratch card received. ";
//            //}

//            //Make sure FName is there at least
//            if (FName.IsNullOrEmpty())
//            {
//                string error = "No First Name received. ";
//                _err.Add(error, "SelfErrorCheck");


//            }
//            //Sex
//            if (Sex == SexENUM.Unknown)
//            {
//                string error = "Please say the sex of the person. ";
//                _err.Add(error, "SelfErrorCheck");

//            }

//            //son
//            if (SonOfOrWifeOf == SonOfWifeOfDotOfENUM.Unknown)
//            {
//                string error = "Is this person wife of or son of? ";
//                _err.Add(error, "SelfErrorCheck");

//            }
//            else
//            {
//                if (NameOfFatherOrHusband.IsNullOrEmpty())
//                {
//                    if (SonOfOrWifeOf == SonOfWifeOfDotOfENUM.DaughterOf)
//                    {
//                        string error = "No father's name received. ";
//                        _err.Add(error, "SelfErrorCheck");

//                    }

//                    if (SonOfOrWifeOf == SonOfWifeOfDotOfENUM.SonOf)
//                    {
//                        string error = "No father's name received. ";
//                        _err.Add(error, "SelfErrorCheck");

//                    }

//                    if (SonOfOrWifeOf == SonOfWifeOfDotOfENUM.WifeOf)
//                    {
//                        string error = "No husband's name received. ";
//                        _err.Add(error, "SelfErrorCheck");


//                    }
//                }
//            }

//            if (BoardingStatus == BoardingENUM.Unknown)
//            {
//                string error = "No boarding status received.";

//                _err.Add(error, "SelfErrorCheck");
//            }

//            if (HouseNo.IsNullOrEmpty())
//            {
//                string error = "No house number received. ";
//                _err.Add(error, "SelfErrorCheck");

//            }

//            if (Road.IsNullOrEmpty())
//            {
//                string error = "No road name received. ";
//                _err.Add(error, "SelfErrorCheck");

//            }

//            if (TownID == null || TownID == 0)
//            {
//                string error = "No Town received. ";
//                _err.Add(error, "SelfErrorCheck");

//            }
//            if (Zip.IsNullOrEmpty())
//            {
//                string error = "No zip received. ";
//                _err.Add(error, "SelfErrorCheck");

//            }

//            if (PhoneNumber.IsNullOrEmpty())
//            {
//                string error = "No contact phone received. This is required.";
//                _err.Add(error, "SelfErrorCheck");

//            }

//            if (DisabilatiesENUM == YesNoENUM.Unknown)
//            {
//                string error = "Please enter if they have disabilaties. ";
//                _err.Add(error, "SelfErrorCheck");

//            }
//            else
//            {
//                if (DisabilatiesENUM == YesNoENUM.Yes)
//                {
//                    if (Disabilities.IsNullOrEmpty())
//                    {
//                        string error = "Please enter the names of the disabilaties. ";
//                        _err.Add(error, "SelfErrorCheck");

//                    }
//                }
//            }


//            if (MonthlySalaryExpected == 0)
//            {
//                string error = "Enter some monthly salary that is expected. ";
//                _err.Add(error, "SelfErrorCheck");

//            }


//            if (FrontIdPictureUpload == null)
//            {
//                string error = "The front Id Picture has not been uploaded. ";
//                _err.Add(error, "SelfErrorCheck");

//            }
//            else
//            {
//                if (FrontIdPictureUpload.ContentLength == 0)
//                {
//                    string error = "Front picture not received.";
//                    _err.Add(error, "SelfErrorCheck");

//                }
//            }

//            if (BackIdPictureUpload == null)
//            {
//                string error = "Front picture not received. (2)";
//                _err.Add(error, "SelfErrorCheck");


//            }
//            else
//            {
//                if (BackIdPictureUpload.ContentLength == 0)
//                {
//                    string error = "Back Id picture not received.";
//                    _err.Add(error, "SelfErrorCheck");

//                }
//            }

//            if (FrontFacePictureUpload == null)
//            {
//                string error = "Back Id picture not received. (2)";
//                _err.Add(error, "SelfErrorCheck");

//            }
//            else
//            {
//                if (FrontFacePictureUpload.ContentLength == 0)
//                {
//                    string error = "Front face picture not received.";
//                    _err.Add(error, "SelfErrorCheck");
//                }
//            }


//            if (SideFacePictureUpload == null)
//            {
//                string error = "Front face picture not received. (2)";
//                _err.Add(error, "SelfErrorCheck");

//            }
//            else
//            {
//                if (SideFacePictureUpload.ContentLength == 0)
//                {
//                    string error = "Side face picture not received.";
//                    _err.Add(error, "SelfErrorCheck");
//                }
//            }

//            if (!_err.HasErrors)
//            {
//                throw new Exception(_err.ToString());

//            }

//        }

//        public void SelfErrorCheck_CheckIfAllUrlsLoaded()
//        {
//            if (FrontIdPictureUploadUrl.IsNullOrEmpty())
//            {
//                string error = string.Format("The '{0}' not saved... try again", "Front Id Picture");
//                _err.Add(error, "SelfErrorCheck_CheckIfAllUrlsLoaded");
//            }

//            if (BackIdPictureUploadUrl.IsNullOrEmpty())
//            {
//                string error = string.Format("The '{0}' not saved... try again", "Back Id Picture");
//                _err.Add(error, "SelfErrorCheck_CheckIfAllUrlsLoaded");
//            }

//            if (FrontFacePictureUploadUrl.IsNullOrEmpty())
//            {
//                string error = string.Format("The '{0}' not saved... try again", "Front Face Picture");
//                _err.Add(error, "SelfErrorCheck_CheckIfAllUrlsLoaded");
//            }

//            if (SideFacePictureUploadUrl.IsNullOrEmpty())
//            {
//                string error = string.Format("The '{0}' not saved... try again", "Side Face Picture");
//                _err.Add(error, "SelfErrorCheck_CheckIfAllUrlsLoaded");
//            }
//        }

//    }
//}