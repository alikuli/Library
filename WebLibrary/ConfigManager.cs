
//using System;
//using System.Configuration;

//namespace AliKuli.UtilitiesNS
//{

//    public static class ConfigManager
//    {



//        #region Country section

//        private static string _defaultCountryName;
//        /// <summary>
//        /// Gets the default country name. Throws AliKuli.Exceptions.PlacesNS.CountryException
//        /// </summary>
//        /// <returns></returns>
//        public static string DefaultCountryName
//        {
//            get
//            {
//                string countryName = _defaultCountryName ?? (_defaultCountryName = ConfigurationManager.AppSettings[MyConstants.DEFAULT_COUNTRY].ToString().ToTitleCase());

//                if (countryName.IsNullOrWhiteSpace())
//                    throw new Exception("Default country name not setup in config file. Please have administrator set up country name.");
                
//                return _defaultCountryName ?? 
//                    (_defaultCountryName = ConfigurationManager.AppSettings[MyConstants.DEFAULT_COUNTRY].ToString().ToTitleCase());

//            }
//        }


//        private static string _defaultCountryAbbreviation;

//        /// <summary>
//        /// Gets the default countrtry's abbreviation fromn the config file. Throws AliKuli.Exceptions.PlacesNS.CountryException
//        /// </summary>
//        /// <returns></returns>
//        public static string DefaultCountryAbbreviation
//        {
//            get
//            {

//                string countryAbbreviation = _defaultCountryAbbreviation ?? 
//                    (_defaultCountryAbbreviation = ConfigurationManager.AppSettings[MyConstants.DEFAULT_COUNTRY_ABBREVIATION].ToString().ToUpper());

//                if (countryAbbreviation.IsNullOrWhiteSpace())
//                    throw new Exception("Default country abbreviation not setup in config file. Please have administrator set up country abbreviation.");

//                return _defaultCountryAbbreviation ??
//                    (_defaultCountryAbbreviation = ConfigurationManager.AppSettings[MyConstants.DEFAULT_COUNTRY_ABBREVIATION].ToString().ToTitleCase());
//            }
//        }



//        public static string _defaultCountryIdentificationNoLength;
//        /// <summary>
//        /// Gets the default countries Identification card number length from the config file. Throws AliKuli.Exceptions.PlacesNS.CountryException
//        /// </summary>
//        /// <param name="lenIdNumber"></param>
//        /// <returns></returns>
//        public static int DefaultCountryIdentificationNoLength
//        {
//            get
//            {

//                string defaultNoLength = _defaultCountryIdentificationNoLength ??
//                    (_defaultCountryIdentificationNoLength = ConfigurationManager.AppSettings[MyConstants.LENGTH_OF_COUNTRY_ID_NUMBER].ToString());

//                if (defaultNoLength.IsNullOrWhiteSpace())
//                    throw new Exception("Default number length not setup in config file. Please have administrator set up default number length.");

//                int lenIdNumber = 0;
//                bool idNoSuccess = int.TryParse(defaultNoLength, out lenIdNumber);

//                if (!idNoSuccess)
//                {
//                    string errorString = string.Format("The length of the Country CNIC number is not an int: '{0}'. Tell your admin to set it up properly please.", lenIdNumber);
//                    throw new Exception(errorString);
//                }

//                return lenIdNumber;

//            }
//        }

//        #endregion

//        #region Encryption

//        private static string _encryptAsString;


//        const string APP_IS_ENCRYPT = "Encrypt";
//        public static string EncryptAsString
//        {
//            get
//            {
//                string encrypt =
//                    _encryptAsString ??
//                    (_encryptAsString = ConfigurationManager.AppSettings[APP_IS_ENCRYPT].ToString());

//                if (encrypt.IsNullOrEmpty())
//                    throw new Exception("The Encrypt has not been set up in the Config file. Value is True of False. Tell your admin to set it up please.");

//                if (encrypt.ToLower() == "true" || encrypt.ToLower() == "false")
//                    return encrypt;

//                throw new Exception(string.Format("The Encrypt has been incorrectly set as '{0}' the Config file. Value is only True of False. Tell your admin to set it up correctly please.",
//                    encrypt));
//            }

//        }

//        public static bool EncryptAsBool()
//        {
//            return Convert.ToBoolean(EncryptAsString);
//        }

//        #endregion

//        #region Sales variables


//        const string APP_DEFAULT_NO_OF_DAYS_TO_SHIP_ORDER = "DefaultOrderDeliveryTimeInNoOfDays";
//        public static double DefaultNoOfDaysToShip()
//        {

//            string noOfDaysToShipString = ConfigurationManager.AppSettings[APP_DEFAULT_NO_OF_DAYS_TO_SHIP_ORDER].ToString();

//            if (noOfDaysToShipString.IsNullOrEmpty())
//            {
//                throw new Exception("The default number of days to ship has not been set up in the Config file. Tell your admin to set it up please.");
//            }

//            double noOfDaysToShipDbl = 0;
//            bool idNoSuccess = double.TryParse(noOfDaysToShipString, out noOfDaysToShipDbl);

//            if (!idNoSuccess)
//            {
//                string errorString = string.Format("The value for the number of days to ship not a number: '{0}'. Tell your admin to set it up properly please in Config file for variable '{1}'.", noOfDaysToShipString, APP_DEFAULT_NO_OF_DAYS_TO_SHIP_ORDER);
//                throw new Exception(errorString);
//            }
//            return noOfDaysToShipDbl;

//        }

//        #endregion

//        #region Scratch Card Helpers
//        /// <summary>
//        /// This gets the number of units required to setup a serviceman. I am using this to get the correct value scratch cards automatically
//        /// from the users accounts
//        /// </summary>
//        /// <returns></returns>
//        public static int ScratchCardUnitsToSetupServiceman()
//        {
//            string numberOfUnitsRqrdForSettingUpServiceMan = ConfigurationManager.AppSettings["NumberOfUnitsRqrdForSettingUpServiceMan"].ToString();

//            if (numberOfUnitsRqrdForSettingUpServiceMan.IsNullOrEmpty())
//                throw new Exception ("The setup in config is empty.");

//            int noOfUnitsInt = 0;
//            bool success = int.TryParse(numberOfUnitsRqrdForSettingUpServiceMan, out noOfUnitsInt);

//            if (!success)
//                throw new Exception("The number setup is not a number.");

//            if (noOfUnitsInt == 0)
//                throw new Exception("The number is a zero! Not allowed. ");


//            return noOfUnitsInt;
//        }


//        /// <summary>
//        /// This gets the number of units required by a customer to get setup and access the website to find a servant. After this, they can purchase smaller denominations(?)
//        /// </summary>
//        /// <returns></returns>
//        public static string ScratchCardUnitsToSetupCustomer()
//        {
//            string numberOfUnitsRqrdForSettingUpCustomer = ConfigurationManager.AppSettings["NumberOfUnitsRqrdForSettingUpCustomer"].ToString();
//            if (numberOfUnitsRqrdForSettingUpCustomer.IsNullOrEmpty())
//                throw new Exception("The setup in config is empty.");

//            long noOfUnits = 0;
//            bool success = long.TryParse(numberOfUnitsRqrdForSettingUpCustomer, out noOfUnits);

//            if (!success)
//                throw new Exception("The number setup is not a number.");

//            if (noOfUnits == 0)
//                throw new Exception("The number is a zero! Not allowed. ");


//            return numberOfUnitsRqrdForSettingUpCustomer;
//        }

//        public static int ScratchCardUnitsToSetupCustomer_Int()
//        {
//            int numOfUnitsReqrdToSetup_Customers = 0;
//            bool success = int.TryParse(ScratchCardUnitsToSetupCustomer(), out numOfUnitsReqrdToSetup_Customers);
//            return numOfUnitsReqrdToSetup_Customers;

//        }
//        /// <summary>
//        /// This is the default length of the scratch card pin number.
//        /// </summary>
//        /// <returns></returns>
//        public static string ScratchCardNumberDefaultLength()
//        {

//            string defaultLengthOfScratchCardPinNumber = ConfigurationManager.AppSettings["DefaultLengthOfScratchCardPinNumber"].ToString();

//            if (defaultLengthOfScratchCardPinNumber.IsNullOrEmpty())
//                throw new Exception("The setup in config is empty.");

//            long noOfUnits = 0;
//            bool success = long.TryParse(defaultLengthOfScratchCardPinNumber, out noOfUnits);

//            if (!success)
//                throw new Exception("The number setup is not a number.");

//            if (noOfUnits == 0)
//                throw new Exception("The number is a zero! Not allowed. ");



//            if (noOfUnits > 16)
//            {
//                throw new Exception(string.Format("The maximum default length of the Scratch Card is 16. You have entered '{0}'. Please Tell your admin to set it up properly in config file.", noOfUnits));
//            }


//            return defaultLengthOfScratchCardPinNumber;
//        }
//        #endregion


//        #region User

//        private const string DEFAULT_ADMIN_USER = "AdminUserName";
//        private static string _adminUser;
//        public static string DefaultAdminUser
//        {
//            get
//            {
//                string adminUser =
//                    _adminUser ??
//                    (_adminUser = ConfigurationManager.AppSettings[DEFAULT_ADMIN_USER].ToString());

//                if (adminUser.IsNullOrEmpty())
//                {
//                    throw new Exception("The default admin user has not been set up in the Config file. Tell your admin to set it up please.");
//                }

//                return adminUser;

//            }
//        }



//        private const string DEFAULT_ADMIN_PASSWORD = "AdminPassword";
//        private static string _adminPassword;
//        public static string DefaultAdminPassword
//        {
//            get
//            {
//                string adminPassword =
//                    _adminPassword ??
//                    (_adminPassword = ConfigurationManager.AppSettings[DEFAULT_ADMIN_PASSWORD].ToString());

//                if (adminPassword.IsNullOrEmpty())
//                {
//                    throw new Exception("The default admin password has not been set up in the Config file. Tell your admin to set it up please.");
//                }

//                return adminPassword;

//            }
//        }


//        #endregion


//    }
//}