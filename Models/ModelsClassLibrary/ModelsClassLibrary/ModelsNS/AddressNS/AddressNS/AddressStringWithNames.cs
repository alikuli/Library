using AliKuli.Extentions;
using InterfacesLibrary.AddressNS;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    /// <summary>
    /// This is basically used to make addresses to show.
    /// </summary>
    public class AddressStringWithNames : IAddressStringWithNames
    {


        public AddressStringWithNames()
        {

        }

        public AddressStringWithNames(
            string houseNo,
            string road,
            string address2,
            string townName,
            string cityName,
            string stateOrProvince,
            string country,
            string webAddress,
            string zip,
            string phone,
            string attention,
            string email)
        {
            Initialize(houseNo, road, address2, townName, cityName, stateOrProvince, country, webAddress, zip, phone, attention, email);
        }

        public void Initialize(
            string houseNo,
            string road,
            string address2,
            string townName,
            string cityName,
            string stateOrProvince,
            string country,
            string webAddress,
            string zip,
            string phone,
            string attention,
            string email)
        {
            HouseNo = houseNo;
            Road = road;
            Address2 = address2;
            TownName = townName;
            CityName = cityName;
            StateName = stateOrProvince;
            CountryName = country;
            WebAddress = webAddress;
            Zip = zip;
            Phone = phone;
            Attention = attention;
            Email = email;
        }

        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "House#")]
        public string HouseNo { get; set; }



        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public string Road { get; set; }

        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Extra Info")]
        public string Address2 { get; set; }



        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Web Site")]
        public string WebAddress { get; set; }



        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Zip/Postal#")]
        public string Zip { get; set; }



        [Display(Name = "Phone")]
        [MaxLength(100, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public string Phone { get; set; }



        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Attention")]
        public string Attention { get; set; }



        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Town")]
        public string TownName { get; set; }


        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "City")]
        public string CityName { get; set; }


        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "State")]
        public string StateName { get; set; }


        /// <summary>
        /// This is just a dummy field that will be used off and on to make string addresses
        /// No, we are going to save it.
        /// </summary>
        [Display(Name = "Country")]
        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public string CountryName { get; set; }



        [Display(Name = "Email")]
        [MaxLength(100, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public string Email { get; set; }

        public override string ToString()
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
                sb.AppendLine(Address2);

            if (!TownName.IsNullOrWhiteSpace())
                sb.AppendLine(TownName);

            if (!CityName.IsNullOrWhiteSpace())
            {
                sb.Append(CityName);

                if (!StateName.IsNullOrEmpty())
                    sb.Append(", " + StateName);
            }
            else
            {
                if (!StateName.IsNullOrEmpty())
                    sb.Append(StateName);

            }

            sb.AppendLine();

            if (!CountryName.IsNullOrWhiteSpace())
            {
                sb.Append(CountryName.ToUpper());

                if (!Zip.IsNullOrWhiteSpace())
                {
                    sb.Append(" " + Zip);
                }
                sb.AppendLine();
            }
            else
            {
                if (!Zip.IsNullOrWhiteSpace())
                {
                    sb.Append("Postal Code: " + Zip);
                    sb.AppendLine();
                }

            }

            if (!Attention.IsNullOrWhiteSpace())
            {
                sb.AppendLine(" ATTENTION: " + Attention.ToTitleCase());
            }
            if (!Phone.IsNullOrWhiteSpace())
            {
                sb.AppendLine(" Ph: " + Phone);
            }

            if (!Email.IsNullOrWhiteSpace())
            {
                sb.AppendLine(" email: " + Email);

            }
            return sb.ToString();
        }

        public string ToStringOnlyNameAndCityAndCountry
        {
            get
            {
                StringBuilder sb = new StringBuilder();



                if (!TownName.IsNullOrWhiteSpace())
                    sb.AppendLine(TownName);

                if (!CityName.IsNullOrWhiteSpace())
                {
                    sb.Append(CityName);

                    if (!StateName.IsNullOrEmpty())
                        sb.Append(", " + StateName);
                }
                else
                {
                    if (!StateName.IsNullOrEmpty())
                        sb.Append(StateName);

                }

                sb.AppendLine();

                if (!CountryName.IsNullOrWhiteSpace())
                {
                    sb.Append(CountryName.ToUpper());

                    if (!Zip.IsNullOrWhiteSpace())
                    {
                        sb.Append(" " + Zip);
                    }
                    sb.AppendLine();
                }
                else
                {
                    if (!Zip.IsNullOrWhiteSpace())
                    {
                        sb.Append("Postal Code: " + Zip);
                        sb.AppendLine();
                    }

                }

                if (!Attention.IsNullOrWhiteSpace())
                {
                    sb.AppendLine(" ATTENTION: " + Attention.ToTitleCase());
                }

                return sb.ToString();
            }
        }
        public string ToStringHTML()
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
                sb.AppendLine(Address2);

            if (!TownName.IsNullOrWhiteSpace())
                sb.AppendLine(TownName);

            if (!CityName.IsNullOrWhiteSpace())
            {
                sb.Append(CityName);

                if (!StateName.IsNullOrEmpty())
                    sb.Append(", " + StateName);
            }
            else
            {
                if (!StateName.IsNullOrEmpty())
                    sb.Append(StateName);

            }

            sb.AppendLine();

            if (!CountryName.IsNullOrWhiteSpace())
            {
                sb.Append(CountryName.ToUpper());

                if (!Zip.IsNullOrWhiteSpace())
                {
                    sb.Append(" " + Zip);
                }
                sb.AppendLine();
            }
            else
            {
                if (!Zip.IsNullOrWhiteSpace())
                {
                    sb.Append(" Postal Code: " + Zip);
                    sb.AppendLine();
                }

            }

            if (!Attention.IsNullOrWhiteSpace())
            {
                sb.AppendLine("<br />ATTENTION: " + Attention.ToTitleCase());
            }
            if (!Phone.IsNullOrWhiteSpace())
            {
                sb.AppendLine("<br />Ph: " + Phone);
            }

            if (!Email.IsNullOrWhiteSpace())
            {
                sb.AppendLine("<br />email: " + Email);

            }
            return sb.ToString();
        }


        public string ToStringTwoLine
        {
            get
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
                    sb.Append(" Ph: " + Phone.ToUpper());
                }
                if (!WebAddress.IsNullOrWhiteSpace())
                {
                    sb.Append(" Web: " + WebAddress);
                }
                if (!Email.IsNullOrWhiteSpace())
                {
                    sb.Append(" EMAIL: " + Email);
                }

                if (!sb.ToString().IsNullOrWhiteSpace())
                    sb.Append(".");

                if (sb.ToString().IsNullOrWhiteSpace())
                {
                    return "No Address Provided.";
                }

                return sb.ToString();
            }
        }

        public string ToString_Phone_And_Email
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (!Phone.IsNullOrWhiteSpace())
                {
                    sb.Append("Ph: " + Phone.ToUpper());
                }
                else
                {
                    sb.Append("NO PHONE");

                }
                if (!Email.IsNullOrWhiteSpace())
                {
                    sb.Append("/EMAIL: " + Email);
                }
                else
                {
                    sb.Append("/NO EMAIL");

                }

                if (!sb.ToString().IsNullOrWhiteSpace())
                    sb.Append(".");

                if (sb.ToString().IsNullOrWhiteSpace())
                {
                    return "No Address Provided.";
                }

                return sb.ToString();
            }
        }


        public string ToAddressWithoutContact
        {
            get
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

                //sb.AppendLine();

                //if (!Phone.IsNullOrWhiteSpace())
                //{
                //    sb.Append(" Ph: " + Phone.ToUpper());
                //}
                //if (!WebAddress.IsNullOrWhiteSpace())
                //{
                //    sb.Append(" Web: " + WebAddress);
                //}
                //if (!Email.IsNullOrWhiteSpace())
                //{
                //    sb.Append(" EMAIL: " + Email);
                //}

                if (!sb.ToString().IsNullOrWhiteSpace())
                    sb.Append(".");

                if (sb.ToString().IsNullOrWhiteSpace())
                {
                    return "No Address Provided.";
                }

                return sb.ToString();
            }

        }
        public string ToTownCityStateCountyOnly_SingleLine
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (TownName.IsNullOrWhiteSpace())
                {

                }
                else
                {
                    sb.Append(TownName);
                }


                string currStringVal = "";
                if (CityName.IsNullOrWhiteSpace())
                {

                }
                else
                {
                    if (TownName.IsNullOrWhiteSpace())
                    {
                        sb.Append(CityName);

                    }
                    else
                    {
                        sb.Append(", ");
                        sb.Append(CityName);
                    }
                }

                currStringVal = sb.ToString();

                if (currStringVal.IsNullOrEmpty())
                {
                    if (StateName.IsNullOrWhiteSpace())
                    {

                    }
                    else
                    {
                        sb.Append(StateName);
                    }
                }
                else
                {
                    if (StateName.IsNullOrWhiteSpace())
                    {

                    }
                    else
                    {
                        sb.Append(", ");
                        sb.Append(StateName);
                    }
                }

                currStringVal = sb.ToString();

                if (currStringVal.IsNullOrEmpty())
                {
                    if (CountryName.IsNullOrWhiteSpace())
                    {

                    }
                    else
                    {
                        sb.Append(CountryName);
                    }
                }
                else
                {
                    if (CountryName.IsNullOrWhiteSpace())
                    {

                    }
                    else
                    {
                        sb.Append(", ");
                        sb.Append(CountryName);
                    }
                }

                return sb.ToString();
            }
        }
        private string createErrorString(string errorStr, string fullErrorStr)
        {
            if (fullErrorStr.IsNullOrWhiteSpace())
            {
                return string.Format("{0} ", errorStr);
            }
            else
            {
                fullErrorStr += errorStr;
                return string.Format("{0} ", fullErrorStr);

            }
        }

        [NotMapped]
        public string Error { get; set; }

        public bool ErrorCheck()
        {
            bool isExceptionThrown = false;

            if (HouseNo.IsNullOrWhiteSpace())
            {
                isExceptionThrown = true;
                Error = createErrorString("House Number is not filled. ", Error);

            }
            if (Road.IsNullOrWhiteSpace())
            {
                isExceptionThrown = true;
                Error = createErrorString("Road name is not filled. ", Error);

            }
            if (TownName.IsNullOrWhiteSpace())
            {
                isExceptionThrown = true;
                Error = createErrorString("Town name is not filled. ", Error);

            }
            if (CityName.IsNullOrWhiteSpace())
            {
                isExceptionThrown = true;
                Error = createErrorString("City name is not filled. ", Error);

            }
            if (StateName.IsNullOrWhiteSpace())
            {
                isExceptionThrown = true;
                Error = createErrorString("Province name is not filled. ", Error);

            }
            if (CountryName.IsNullOrWhiteSpace())
            {
                isExceptionThrown = true;
                Error = createErrorString("Country name is not filled. ", Error);

            }
            //if (Email.IsNullOrWhiteSpace())
            //{
            //    isExceptionThrown = true;
            //    Error = createErrorString("Email is not filled. ", Error);

            //}
            if (Phone.IsNullOrWhiteSpace())
            {
                isExceptionThrown = true;
                Error = createErrorString("Phone is not filled. ", Error);

            }


            if (HouseNo.ToLower() == "undefined")
            {
                isExceptionThrown = true;
                Error = createErrorString("House Number is not filled. ", Error);

            }
            if (Road.ToLower() == "undefined")
            {
                isExceptionThrown = true;
                Error = createErrorString("Road name is not filled. ", Error);

            }
            if (TownName.ToLower() == "undefined")
            {
                isExceptionThrown = true;
                Error = createErrorString("Town name is not filled. ", Error);

            }
            if (CityName.ToLower() == "undefined")
            {
                isExceptionThrown = true;
                Error = createErrorString("City name is not filled. ", Error);

            }
            if (StateName.ToLower() == "undefined")
            {
                isExceptionThrown = true;
                Error = createErrorString("Province name is not filled. ", Error);

            }
            if (CountryName.ToLower() == "undefined")
            {
                isExceptionThrown = true;
                Error = createErrorString("Country name is not filled. ", Error);

            }
            if (Phone.ToLower() == "undefined")
            {
                isExceptionThrown = true;
                Error = createErrorString("Country name is not filled. ", Error);

            }

            //if (Email.ToLower() == "undefined")
            //{
            //    isExceptionThrown = true;
            //    Error = createErrorString("Country name is not filled. ", Error);

            //}

            return isExceptionThrown;
        }

        public override bool Equals(object obj)
        {
            AddressStringWithNames addy = obj as AddressStringWithNames;

            if (addy.IsNull())
                return false;

            string HouseNo_NotNull = HouseNo ?? "";
            string Road_NotNull = Road ?? "";
            string Address2_NotNull = Address2 ?? "";
            string TownName_NotNull = TownName ?? "";
            string CityName_NotNull = CityName ?? "";
            string StateName_NotNull = StateName ?? "";
            string CountryName_NotNull = CountryName ?? "";
            string Attention_NotNull = Attention ?? "";
            string Phone_NotNull = Phone ?? "";
            string Email_NotNull = Email ?? "";

            string HouseNo_NotNull_Incoming = addy.HouseNo ?? "";
            string Road_NotNull_Incoming = addy.Road ?? "";
            string Address2_NotNull_Incoming = addy.Address2 ?? "";
            string TownName_NotNull_Incoming = addy.TownName ?? "";
            string CityName_NotNull_Incoming = addy.CityName ?? "";
            string StateName_NotNull_Incoming = addy.StateName ?? "";
            string CountryName_NotNull_Incoming = addy.CountryName ?? "";
            string Attention_NotNull_Incoming = addy.Attention ?? "";
            string Phone_NotNull_Incoming = Phone ?? "";
            string Email_NotNull_Incoming = Email ?? "";

            if (HouseNo_NotNull.ToLower() == HouseNo_NotNull_Incoming.ToLower() &&
                Road_NotNull.ToLower() == Road_NotNull_Incoming.ToLower() &&
                Address2_NotNull.ToLower() == Address2_NotNull_Incoming.ToLower() &&
                TownName_NotNull.ToLower() == TownName_NotNull_Incoming.ToLower() &&
                CityName_NotNull.ToLower() == CityName_NotNull_Incoming.ToLower() &&
                StateName_NotNull.ToLower() == StateName_NotNull_Incoming.ToLower() &&
                CountryName_NotNull.ToLower() == CountryName_NotNull_Incoming.ToLower() &&
                Attention_NotNull.ToLower() == Attention_NotNull_Incoming.ToLower() &&
                Email_NotNull.ToLower() == Email_NotNull_Incoming.ToLower() &&
                Phone_NotNull.ToLower() == Phone_NotNull_Incoming.ToLower()

                )

                return true;
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 269;

            if (!HouseNo.IsNullOrWhiteSpace())
                hash = (hash * 47) + HouseNo.ToLower().GetHashCode();

            if (!Address2.IsNullOrWhiteSpace())
                hash = (hash * 47) + Address2.ToLower().GetHashCode();

            if (!TownName.IsNullOrWhiteSpace())
                hash = (hash * 47) + TownName.ToLower().GetHashCode();

            if (!CityName.IsNullOrWhiteSpace())
                hash = (hash * 47) + CityName.ToLower().GetHashCode();

            if (!StateName.IsNullOrWhiteSpace())
                hash = (hash * 47) + StateName.ToLower().GetHashCode();

            if (!CountryName.IsNullOrWhiteSpace())
                hash = (hash * 47) + CountryName.ToLower().GetHashCode();

            if (!Attention.IsNullOrWhiteSpace())
                hash = (hash * 47) + Attention.ToLower().GetHashCode();

            return hash;
        }

        public bool IsWhiteSpaceOrNull()
        {
            return
                (HouseNo.IsNullOrWhiteSpace() || HouseNo.ToLower() == "undefined") &&
                (Road.IsNullOrWhiteSpace() || Road.ToLower() == "undefined") &&
                (Address2.IsNullOrWhiteSpace() || Address2.ToLower() == "undefined") &&
                (TownName.IsNullOrWhiteSpace() || TownName.ToLower() == "undefined") &&
                (CityName.IsNullOrWhiteSpace() || CityName.ToLower() == "undefined") &&
                (StateName.IsNullOrWhiteSpace() || StateName.ToLower() == "undefined") &&
                (CountryName.IsNullOrWhiteSpace() || CountryName.ToLower() == "undefined") &&
                (Phone.IsNullOrWhiteSpace() || Phone.ToLower() == "undefined") &&
                (Email.IsNullOrWhiteSpace() || Email.ToLower() == "undefined") &&
                (WebAddress.IsNullOrWhiteSpace() || WebAddress.ToLower() == "undefined");
        }

        public static AddressStringWithNames SystemAddress()
        {
            AddressStringWithNames ac = new AddressStringWithNames();
            ac.Road = "Main Harbanspura Road";
            ac.Address2 = "Gulkali";
            ac.Attention = "Manager";
            ac.CityName = "Lahore";
            ac.CountryName = "Pakistan";
            ac.Email = "alikuli62@gmail.com";
            ac.HouseNo = "1";
            ac.Phone = "92-331-4474120";
            ac.StateName = "Punjab";
            ac.TownName = "Lahore";
            ac.WebAddress = "";
            ac.Zip = "0000";

            return ac;
        }


    }
}