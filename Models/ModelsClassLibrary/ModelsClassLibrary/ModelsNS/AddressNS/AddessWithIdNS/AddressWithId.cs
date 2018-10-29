using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.AddressNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.GeoLocationNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using UserModels;

namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    /// <summary>
    /// Addresses are unique for the user. The Unique name governs what is a unique address.
    /// The Dal layer checks only against the User's addresses to make sure the address is unique.
    /// That is because GetDomainDataForDuplicateNameSearch is overridden
    /// </summary>
    public class AddressWithId : CommonWithId, IAddressWithId, IHasVerification
    {
        public AddressWithId()
        {
            //default is true for all address types
            //AddressComplex = new AddressComplex();

            AddressType = new AddressTypeComplex();


            GeoPosition = new GeoLocationComplex();
            Verification = new Verification();
        }


        [MaxLength(200, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "House#")]
        public string HouseNo { get; set; }



        [MaxLength(200, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public string Road { get; set; }

        [MaxLength(1000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Extra Info")]
        public string Address2 { get; set; }



        [MaxLength(200, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Web Site")]
        public string WebAddress { get; set; }



        [MaxLength(200, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Zip/Postal#")]
        public string Zip { get; set; }



        [Display(Name = "Phone")]
        [MaxLength(1000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public string Phone { get; set; }



        [MaxLength(200, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Attention")]
        public string Attention { get; set; }



        [MaxLength(200, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Town")]
        public string TownName { get; set; }


        [MaxLength(200, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "City")]
        public string CityName { get; set; }


        [MaxLength(200, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "State")]
        public string StateName { get; set; }



        [Display(Name = "Country")]
        public string CountryId { get; set; }
        public virtual Country Country { get; set; }

        public string CountryName
        {
            get
            {
                //   Country.IsNullThrowException("Country not loaded. Programming error");
                if (Country.IsNull())
                    return "";

                string countryName = Country.Name;
                return countryName;

            }
        }

        [Display(Name = "Geo Position")]
        public GeoLocationComplex GeoPosition { get; set; }




        /// <summary>
        /// If general, then none is selected.
        /// </summary>
        [Display(Name = "Type")]
        public AddressTypeComplex AddressType { get; set; }



        public virtual ApplicationUser User { get; set; }

        public virtual string UserId { get; set; }



        public Verification Verification { get; set; }


        public virtual ICollection<AddressVerificationTrx> AddressVerificationTrxs { get; set; }


        public override ClassesWithRightsENUM ClassNameForRights()
        {
            return ClassesWithRightsENUM.Address;
        }


        //this makes sure the same address is not added several times.
        public override string MakeUniqueName()
        {

            StringBuilder sb = new StringBuilder();

            if (!HouseNo.IsNullOrWhiteSpace())
            {
                sb.Append(HouseNo);
            }


            if (!Road.IsNullOrWhiteSpace())
            {
                sb.Append(", " + Road);
            }

            if (!Address2.IsNullOrWhiteSpace())
            {
                sb.Append(", " + Address2);
            }

            if (!TownName.IsNullOrWhiteSpace())
            {
                sb.Append(", " + TownName);
            }
            if (!CityName.IsNullOrWhiteSpace())
            {
                sb.Append(", " + CityName);
            }

            if (!StateName.IsNullOrWhiteSpace())
            {
                sb.Append(", " + StateName);
            }

            if (Country.IsNull())
            {
                throw new Exception("The Country is null in the address.");
            }
            else
            {
                sb.Append(", " + Country.Name);
            }

            if (!Attention.IsNullOrWhiteSpace())
            {
                sb.Append(", Atn: " + Attention);
            }
            if (!Phone.IsNullOrWhiteSpace())
            {
                sb.Append(", Ph: " + Phone);
            }



            return sb.ToString();

        }




        public override void UpdatePropertiesDuringModify(ICommonWithId ic)
        {
            base.UpdatePropertiesDuringModify(ic);

            AddressWithId addy = ic as AddressWithId;
            addy.IsNullThrowException("Unable to box ic in AddressWithId");

            HouseNo = addy.HouseNo;
            Road = addy.Road;
            Address2 = addy.Address2;
            TownName = addy.TownName;
            CityName = addy.CityName;
            StateName = addy.StateName;

            WebAddress = addy.WebAddress;
            Phone = addy.Phone;


            CountryId = addy.CountryId;
            UserId = addy.UserId;
            GeoPosition = addy.GeoPosition;
            AddressType = addy.AddressType;
            Attention = addy.Attention;

            Verification = addy.Verification;


            //AddressWithId a = ic as AddressWithId;

            //if (a == null)
            //{
            //    throw new Exception("Unable to box AddressWithId. AddressWithId.UpdatePropertiesDuringModify");
            //}


            //AddressComplex = a.AddressComplex;
            //AddressType = a.AddressType;
            //UserId = a.UserId;
            //CountryId = a.CountryId;

        }

        public override string ToString()
        {
            return CreateAddressWithLineBreakAs("");
        }



        public string CreateAddressWithLineBreakAs(string lineBreak)
        {
            StringBuilder sb = new StringBuilder();

            //if (!Name.IsNullOrWhiteSpace())
            //{
            //    sb.AppendLine(Name.ToUpper());
            //}

            if (!HouseNo.IsNullOrWhiteSpace())
            {
                sb.Append("House#: " + HouseNo);
            }

            if (Road.IsNullOrWhiteSpace())
            {
                //sb.AppendLine();
            }
            else
            {
                if (!HouseNo.IsNullOrWhiteSpace())
                    sb.Append(",");

                sb.Append(" " + Road + ", ");
            }

            if (!Address2.IsNullOrWhiteSpace())
            {
                sb.Append(lineBreak);
                sb.AppendLine(Address2);
            }
            if (!TownName.IsNullOrWhiteSpace())
            {
                sb.Append(lineBreak);
                sb.AppendLine(TownName);
            }
            if (!CityName.IsNullOrWhiteSpace())
            {
                sb.Append(lineBreak);
                sb.Append(CityName);

                if (!StateName.IsNullOrEmpty())
                    sb.Append(", " + StateName);
            }
            else
            {
                if (!StateName.IsNullOrEmpty())
                    sb.Append(StateName);

            }
            sb.Append(lineBreak);
            sb.AppendLine();

            if (!CountryName.IsNullOrWhiteSpace())
            {
                sb.Append(CountryName.ToUpper());

                if (!Zip.IsNullOrWhiteSpace())
                {
                    sb.Append(" " + Zip);
                }
                sb.Append(lineBreak);
                sb.AppendLine();
            }
            else
            {
                if (!Zip.IsNullOrWhiteSpace())
                {
                    sb.Append("Postal Code: " + Zip);
                    sb.Append(lineBreak);
                    sb.AppendLine();
                }

            }

            if (!Phone.IsNullOrWhiteSpace())
            {
                sb.Append(lineBreak);
                sb.AppendLine("Ph: " + Phone);
            }
            if (!Attention.IsNullOrWhiteSpace())
            {
                sb.Append(lineBreak);
                sb.AppendLine("ATTENTION: " + Attention.ToTitleCase());
                sb.Append(lineBreak);

            }
            return sb.ToString();

        }
        public string ToStringTwoLine()
        {
            StringBuilder sb = new StringBuilder();
            //if (!Name.IsNullOrWhiteSpace())
            //{
            //    sb.Append(Name.ToUpper() + ", ");
            //}

            if (!HouseNo.IsNullOrWhiteSpace())
            {
                sb.Append("House#: " + HouseNo.ToUpper() + ", ");
            }


            if (!Road.IsNullOrWhiteSpace())
            {
                sb.Append(Road.ToTitleCase() + ", ");
                //sb.AppendLine();
            }

            if (!Address2.IsNullOrWhiteSpace())
                sb.Append(Address2.ToTitleCase() + ", ");


            if (!TownName.IsNullOrWhiteSpace())
                sb.Append(TownName.ToTitleCase() + ", ");


            if (!CityName.IsNullOrWhiteSpace())
            {
                sb.Append(CityName.ToTitleCase() + ", ");

            }

            if (!StateName.IsNullOrEmpty())
                sb.Append(StateName.ToTitleCase() + ", ");


            if (!CountryName.IsNullOrWhiteSpace())
            {
                sb.Append(CountryName.ToUpper());

                if (!Zip.IsNullOrWhiteSpace())
                {
                    sb.Append(" " + Zip.ToUpper());
                }
            }
            else
            {
                if (!Zip.IsNullOrWhiteSpace())
                {
                    sb.Append("Postal Code: " + Zip.ToUpper() + ", ");
                }

            }

            sb.AppendLine();

            if (!Phone.IsNullOrWhiteSpace())
            {
                sb.Append("Ph: " + Phone.ToUpper());
            }
            if (!WebAddress.IsNullOrWhiteSpace())
            {
                sb.Append(" - Web: " + WebAddress);
            }

            if (!sb.ToString().IsNullOrWhiteSpace())
                sb.Append(".");
            return sb.ToString();
        }


        public string ToPostal()
        {
            return ToString();
        }

        public string ToPostalHTML()
        {
            return CreateAddressWithLineBreakAs("<BR />");
        }

        //public double GetPaymentAmoung()
        //{
        //    switch (IsLocal)
        //    {
        //        case true:
        //            switch (MailServiceEnum)
        //            {
        //                case MailServiceENUM.Post:
        //                    break;

        //                case MailServiceENUM.Courier:
        //                    break;

        //                default:
        //                    break;
        //            }
        //            break;
        //        case false:
        //            break;

        //        default:
        //            break;
        //    }

        //}

    }
}