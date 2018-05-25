using AliKuli.Extentions;
using InterfacesLibrary.AddressNS;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModelsClassLibrary.ModelsNS.AddressNS
{
    public class AddressStringWithNames : IAddressString
    {

        /// <summary>
        /// We need this here to complete the address. This will have to be dealt with later.
        /// </summary>
        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Extra Info")]
        public string Address2 { get; set; }

        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "House#")]
        public string HouseNo { get; set; }


        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public string Road { get; set; }


        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Web Site")]
        public string WebAddress { get; set; }


        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        [Display(Name = "Zip/Postal#")]
        public string Zip { get; set; }


        [Display(Name = "Phone")]
        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
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

        [Display(Name = "Country")]
        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        public string CountryName { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (!Name.IsNullOrWhiteSpace())
            {
                sb.AppendLine(Name.ToUpper());
            }

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

            if (!Phone.IsNullOrWhiteSpace())
            {
                sb.AppendLine("Ph: " + Phone);
            }
            if (!Attention.IsNullOrWhiteSpace())
            {
                sb.AppendLine("ATTENTION: " + Attention.ToTitleCase());
            }
            return sb.ToString();
        }
        public string ToStringTwoLine()
        {
            StringBuilder sb = new StringBuilder();
            if (!Name.IsNullOrWhiteSpace())
            {
                sb.Append(Name.ToUpper() + ", ");
            }

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
    }
}