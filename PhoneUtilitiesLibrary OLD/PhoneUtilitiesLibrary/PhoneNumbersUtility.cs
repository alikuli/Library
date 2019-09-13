using libphonenumber;

namespace AliKuli.UtilitiesNS
{
    /// <summary>
    /// Checks if number is valid for the country.
    /// Returns complete phonenumber string and complete phone number string formatted for country.
    /// </summary>
    public class PhoneNumbersUtility
    {

        private readonly string _phoneIn;
        private readonly string _countryAbbreviation;


        /// <summary>
        /// This returns the complete phone number string
        /// </summary>
        public  string CompletePhoneNumber { get; set; }

        /// <summary>
        /// This returns the complete country formated string
        /// </summary>
        public  string CompletePhoneNumberFormatted { get; set; }

        public  bool IsValid { get; set; }


        public PhoneNumbersUtility(string phoneIn, string countryAbbreviation)
        {
            _phoneIn = phoneIn;
            _countryAbbreviation = countryAbbreviation;
            IsValidPhoneNumber();
        }

        private bool IsValidPhoneNumber()
        {
            //Now check the number using the google phone library https://github.com/googlei18n/libphonenumber
            PhoneNumberUtil phoneUtil = PhoneNumberUtil.Instance;
            PhoneNumber pakPhoneNumberProto = phoneUtil.Parse(_phoneIn, _countryAbbreviation);

            //Now let us validate the number
            IsValid = phoneUtil.IsPossibleNumber(_phoneIn, _countryAbbreviation); // .IsValidNumber(pakPhoneNumberProto); //returns true if valid

            //At this point, pakPhoneNumberProto contains:
            //    {
            //      "country_code": 90,
            //      "national_number": 446681800 (?)
            //    }


            if (IsValid)
            {
                CompletePhoneNumber = pakPhoneNumberProto.CountryCode.ToString() + pakPhoneNumberProto.NationalNumber.ToString();
                //CompletePhoneNumberFormatted = phoneUtil. .FormatNationalNumberWithCarrierCode(pakPhoneNumberProto, CompletePhoneNumber);

            }

            return IsValid;

        }





    }
}