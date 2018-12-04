using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.PlayersNS
{



    /// <summary>
    /// This class is only used to do calculations etc for IPerson. 
    /// </summary>
    [ComplexType]
    public class PersonComplex
    {
        private ErrorSet _err;

        public PersonComplex()
        {
            _err = new ErrorSet();
            _err.SetLibAndClass(Assembly.GetCallingAssembly().GetName().Name, this.GetType().Name);
        }


        [Display(Name = "Identification #")]
        [MaxLength(50, ErrorMessage = "Max length allowed is {0} charecters")]
        public string IdentificationNo { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "Max length allowed is {0} charecters")]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "Max length allowed is {0} charecters")]
        public string LName { get; set; }

        [Display(Name = "Middle Name")]
        [MaxLength(50, ErrorMessage = "Max length allowed is {0} charecters")]
        public string MName { get; set; }



        [Display(Name = "Name of Relative")]
        [MaxLength(100, ErrorMessage = "Max length allowed is {0} charecters")]
        public string NameOfFatherOrHusband { get; set; }


        [NotMapped]
        public SelectList SelectListSonOfOrWifeOf { get; set; }

        [NotMapped]
        public SelectList SelectListSex { get; set; }

        [Display(Name = "Sex")]
        public SexENUM SexEnum { get; set; }


        [Display(Name = "Relation Type")]
        public SonOfWifeOfDotOfENUM SonOfOrWifeOfEnum { get; set; }


        //public void LoadFrom(IPerson p)
        //{
        //    IdentificationNo = p.IdentificationNo;
        //    FName = p.FName;
        //    LName = p.LName;
        //    MName = p.MName;
        //    Sex = p.Sex;
        //    NameOfFatherOrHusband = p.NameOfFatherOrHusband ?? "";
        //    SonOfOrWifeOf = p.SonOfOrWifeOf;

        //}

        public string Helper_CreateFullName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(FName.ToTitleCase());

            if (!MName.IsNullOrEmpty())
            {
                sb.Append(" ");
                sb.Append(MName.ToTitleCase());
            }
            if (!LName.IsNullOrEmpty())
            {
                sb.Append(" ");
                sb.Append(LName.ToTitleCase());
            }

            //if (SonOfOrWifeOf != SonOfWifeOfDotOfENUM.Unknown)
            //{

            //    sb.Append(" ");
            //    sb.Append(SonOfOrWifeOf.ToString());
            //    sb.Append(" ");
            //    sb.Append(NameOfFatherOrHusband.ToTitleCase());
            //}

            //Add the Identity Card Number to make sure there are no unneccessary duplicates in the names
            //The Id card will be the item tjhat will bring about the uniqueness
            if (!IdentificationNo.IsNullOrEmpty())
            {
                sb.Append(" ");
                sb.Append(string.Format("- National ID: {0}", IdentificationNo.ToPakistanCnicFormat()));
            }

            return sb.ToString();
        }




        //private void Check_FathersNameOrHusbandsNameEntered()
        //{
        //    if (NameOfFatherOrHusband.IsNullOrEmpty())
        //    {
        //        string error;

        //        if (SonOfOrWifeOf == SonOfWifeOfDotOfENUM.WifeOf)
        //        {
        //            error = string.Format("You have not entered the name of the Husband for '{0}'. Please enter the Husband Name. ", PersonFullName());
        //            _err.Add(error, MethodBase.GetCurrentMethod());
        //        }

        //        if (SonOfOrWifeOf == SonOfWifeOfDotOfENUM.SonOf)
        //        {
        //            error = string.Format("You have not entered the name of the Father for '{0}'. Please enter the Father Name. ", PersonFullName());
        //            _err.Add(error, MethodBase.GetCurrentMethod());
        //        }
        //    }
        //}

        //private void Check_SonOfWifeOfField()
        //{
        //    //We want to know the father's name
        //    if (SonOfOrWifeOf == SonOfWifeOfDotOfENUM.Unknown)
        //    {
        //        string error = (string.Format("You have not entered the parentage/husband for '{0}'. Please enter the parentage or Husband. (W/O, S/O. D/O)", PersonFullName()));
        //        _err.Add(error, MethodBase.GetCurrentMethod());

        //    }
        //}


        private void Check_FName()
        {
            if (FName.IsNullOrEmpty())
            {
                string error = "First Name missing.";
                _err.Add(error, MethodBase.GetCurrentMethod());

            }
        }

        private void Check_NationalIdentificationNumber()
        {
            if (IdentificationNo.IsNullOrEmpty())
            {
                string error = "Country Identifican Card Number missing.";
                _err.Add(error, MethodBase.GetCurrentMethod());
            }
        }

        //private void Check_Sex()
        //{
        //    if (Sex == SexENUM.Unknown)
        //    {
        //        string error = "Person's sex is missing.";
        //        _err.Add(error, MethodBase.GetCurrentMethod());
        //    }
        //}





        public string PersonFullName()
        {
            return Helper_CreateFullName();
        }


        public void SelfErrorCheck()
        {
            //Check_FathersNameOrHusbandsNameEntered();
            //Check_SonOfWifeOfField();
            Check_FName();
            Check_NationalIdentificationNumber();
            //Check_Sex();
        }

    }
}