using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.AddressNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddessWithIdNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.GeoLocationNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    /// <summary>
    /// Addresses are unique for the user. The Unique name governs what is a unique address.
    /// The Dal layer checks only against the User's addresses to make sure the address is unique for the user 
    /// o.w 2 different users can have same address.
    /// That is because GetDomainDataForDuplicateNameSearch is overridden
    /// Address will also double as a warehouse for the customer and for the vendor.
    /// </summary>
    public class AddressMain : PhoneEmailAddressAbstract, IAddressMain
    {
        public AddressMain()
        {
            //default is true for all address types
            //AddressComplex = new AddressComplex();

            AddressType = new AddressTypeComplex();


            GeoPosition = new GeoLocationComplex();
            //Verification = new Verification();
        }


        [MaxLength(200, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "House#")]
        public string HouseNo { get; set; }

        //public string PersonId { get; set; }
        //public Person Person { get; set; }


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




        public ICollection<Cashier> CashiersDefaultAddresses { get; set; }
        
        //public VerificaionStatusENUM VerificationStatusEnum { get; set; }

        [NotMapped]
        public SelectList VerificaionStatusEnumSelectList { get { return EnumExtention.ToSelectListSorted<VerificaionStatusENUM>(VerificaionStatusENUM.Unknown); } }



        public Verification Verification
        {
            get
            {
                var latestTrx = AddressVerificationTrxs;
                //.OrderBy(x => x.MetaData.Created.Date_NotNull_Min)
                //.FirstOrDefault();

                if (latestTrx.IsNullOrEmpty())
                {
                    Verification verificationDummy = new Verification();
                    verificationDummy.VerificaionStatusEnum = VerificaionStatusENUM.NotVerified;
                    return verificationDummy;
                }

                AddressVerificationTrx addressVerificationTrx = AddressVerificationTrxs.OrderByDescending(x => x.MetaData.Created.Date_NotNull_Min).FirstOrDefault();
                return addressVerificationTrx.Verification;

            }
        }

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

            AddressMain addy = ic as AddressMain;
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
            GeoPosition = addy.GeoPosition;
            AddressType = addy.AddressType;
            Attention = addy.Attention;
            PersonId = addy.PersonId;


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


        public static AddressMain UnboxFrom(ICommonWithId ic)
        {
            AddressMain addressWithId = ic as AddressMain;
            addressWithId.IsNullThrowException("unable to unbox AddressWithId");
            return addressWithId;

        }

    }
}