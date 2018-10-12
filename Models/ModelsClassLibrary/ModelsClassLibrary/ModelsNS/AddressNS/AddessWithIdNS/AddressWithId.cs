using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.AddressNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.GeoLocationNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
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
    public class AddressWithId : CommonWithId, IAddressWithId
    {
        public AddressWithId()
        {
            //default is true for all address types
            //AddressComplex = new AddressComplex();
            AddressType = new AddressTypeComplex();


            GeoPosition = new GeoLocationComplex();

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
        public Country Country { get; set; }


        [Display(Name = "Geo Position")]
        public GeoLocationComplex GeoPosition { get; set; }





        /// <summary>
        /// If general, then none is selected.
        /// </summary>
        [Display(Name = "Type")]
        public AddressTypeComplex AddressType { get; set; }



        public virtual ApplicationUser User { get; set; }

        public virtual string UserId { get; set; }



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



    }
}