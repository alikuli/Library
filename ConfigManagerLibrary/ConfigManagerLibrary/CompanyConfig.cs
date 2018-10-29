using AliKuli.Extentions;
using ConstantsLibrary;
using System.Configuration;

namespace ConfigManagerLibrary
{
    public class CompanyConfig
    {

        /// <summary>
        /// This returns the number of mailers allowed to the mailers.
        /// </summary>
        public static string CompanyNameForWebPage
        {
            get
            {

                string str = ConfigurationManager.AppSettings[CompanyConstants.COMPANY_NAME_ON_WEBPAGE];
                str.IsNullOrWhiteSpaceThrowException("COMPANY NAME ON WEBPAGE not set in Web Config.");
                return str;


            }
        }

        public static string CompanyAddress
        {
            get
            {

                string str = ConfigurationManager.AppSettings[CompanyConstants.COMPANY_ADDRESS];
                str.IsNullOrWhiteSpaceThrowException("COMPANY ADDRESS not set in Web Config.");
                return str;


            }
        }









    }
}
