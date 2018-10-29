using AliKuli.ConstantsNS;
using AliKuli.Extentions;
using ConfigManagerLibrary;
using System;
using System.Configuration;
using System.Reflection;

namespace AliKuli.UtilitiesNS
{
    public class ConfigManagerHelper
    {

        public VerificationConfig Verification { get; set; }
        private ErrorHandlerConfigManagerHelper _err;
        public ConfigManagerHelper()
        {
            _err = new ErrorHandlerConfigManagerHelper(IsVerbose);
        }

        public virtual string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }

        private void InitializeError()
        {
            //_err.SetLibAndClass(Assembly.GetCallingAssembly().GetName().Name, typeof(ConfigManagerHelper).Name);
        }

        string _adminCountryName;
        public virtual string AdminCountryName
        {
            get
            {

                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_adminCountryName = _adminCountryName ??
                    ConfigurationManager.AppSettings[MyConstants.ADMIN_COUNTRY]));



            }
        }


        string _adminName;
        public virtual string AdminName
        {
            get
            {

                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_adminName = _adminName ??
                    ConfigurationManager.AppSettings[MyConstants.ADMIN_NAME]));



            }
        }

        string _adminRole;
        public virtual string AdminRole
        {
            get
            {

                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_adminRole = _adminRole ??
                    ConfigurationManager.AppSettings[MyConstants.ADMIN_ROLE]));



            }
        }

        //virtual string _countryIdCardNumberLength;
        //public virtual string CountryIdCardNumberLength
        //{
        //    get
        //    {

        //        if (!CheckForNullStringAndThrowError_Helper(
        //            MethodBase.GetCurrentMethod(),
        //            (_countryIdCardNumberLength = _countryIdCardNumberLength ??
        //            ConfigurationManager.AppSettings[MyConstants.ADMIN_COUNTRY_ID_NUMBER_LENGTH]))
        //            .IsValidInteger())

        //            ThrowException_InvalidInteger(MethodBase.GetCurrentMethod(), _countryIdCardNumberLength, "Country ID Card Number Length");

        //        return _countryIdCardNumberLength;

        //    }
        //}


        private string _companyName;
        public virtual string CompanyName
        {
            get
            {

                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_companyName = _companyName ??
                    ConfigurationManager.AppSettings[MyConstants.COMPANY_NAME]));

            }
        }



        string _adminCountryAbbreviation;
        public virtual string AdminCountryAbbreviation
        {
            get
            {

                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_adminCountryAbbreviation = _adminCountryAbbreviation ??
                    ConfigurationManager.AppSettings[MyConstants.ADMIN_COUNTRY_ABBREVIATION]));

            }
        }



        string _defaultPageSize;
        public virtual string DefaultPageSize
        {
            get
            {
                if (!CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_defaultPageSize = _defaultPageSize ??
                    ConfigurationManager.AppSettings[MyConstants.DEFAULT_PAGE_SIZE]))
                    .IsValidInteger())

                    ThrowException_InvalidInteger(MethodBase.GetCurrentMethod(), _defaultPageSize, "Default Page Size");


                return _defaultPageSize;
            }
        }




        string _emailTestingDirectory;
        /// <summary>
        /// This is the testing directory for emails.
        /// </summary>
        public virtual string EmailTestingDirectory
        {

            get
            {
                if (!CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_emailTestingDirectory = _emailTestingDirectory ??
                    ConfigurationManager.AppSettings[MyConstants.EMAIL_TESTING_DIRECTORY]))
                    .IsValidEmail())

                    ThrowException_InvalidEmail(MethodBase.GetCurrentMethod(), _emailTestingDirectory);


                return _emailTestingDirectory;
            }
        }



        string _adminEmailAddress;
        /// <summary>
        /// This is the from Email address that shows up in all the emails.
        /// </summary>
        public virtual string AdminEmailAddress
        {

            get
            {
                if (!CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_adminEmailAddress = _adminEmailAddress ??
                    ConfigurationManager.AppSettings[MyConstants.ADMIN_EMAIL]))
                    .IsValidEmail())

                    ThrowException_InvalidEmail(MethodBase.GetCurrentMethod(), _adminEmailAddress);


                return _adminEmailAddress;
            }
        }





        //virtual string _ftpServerIp;
        ///// <summary>
        ///// This is the FTP server IP
        ///// </summary>
        //public virtual string FtpServerIp
        //{

        //    get
        //    {
        //        if (_ftpServerIp.IsNullOrWhiteSpace())
        //            _ftpServerIp = GetFromSetUp(SetupEnum.FtpServerIp);

        //        if (_ftpServerIp.IsNullOrWhiteSpace())
        //            _ftpServerIp = "";

        //        return _ftpServerIp;
        //    }
        //}



        //virtual string _ftpUserName;
        ///// <summary>
        ///// This is the FTP username.
        ///// </summary>
        //public virtual string FtpUserName
        //{

        //    get
        //    {
        //        if (_ftpUserName.IsNullOrWhiteSpace())
        //            _ftpUserName = GetFromSetUp(SetupEnum.FtpUserName);

        //        if (_ftpUserName.IsNullOrWhiteSpace())
        //            _ftpUserName = "";


        //        return _ftpUserName;


        //    }
        //}



        //virtual string _ftpPassword;
        ///// <summary>
        ///// This is the FTP password
        ///// </summary>
        //public virtual string FtpPassword
        //{

        //    get
        //    {
        //        if (_ftpPassword.IsNullOrWhiteSpace())
        //            _ftpPassword = GetFromSetUp(SetupEnum.FtpPassword);

        //        if (_ftpPassword.IsNullOrWhiteSpace())
        //            _ftpPassword = "";


        //        return _ftpPassword;


        //    }
        //}


        string _isSendBcc;
        /// <summary>
        /// If true, then this sends BCC messages to the admin.
        /// </summary>
        public virtual string IsSendBcc
        {

            get
            {
                if (!CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_isSendBcc = _isSendBcc ??
                    ConfigurationManager.AppSettings[MyConstants.IS_SEND_BCC]))
                    .IsValidBoolean())

                    ThrowException_InvalidBool(MethodBase.GetCurrentMethod(), _isSendBcc);

                return _isSendBcc;
            }
        }




        string _scratchCardMaxBatchSize;
        /// <summary>
        /// This is the default batch size for a scratch card batch
        /// </summary>
        public virtual string ScratchCardMaxBatchSize
        {

            get
            {
                if (!CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_scratchCardMaxBatchSize = _scratchCardMaxBatchSize ??
                    ConfigurationManager.AppSettings[MyConstants.DEFAULT_SCRATCHCARD_BATCH_SIZE]))
                    .IsValidInteger())

                    ThrowException_InvalidInteger(MethodBase.GetCurrentMethod(), _scratchCardMaxBatchSize, "Scratch Card Max Size");

                return _scratchCardMaxBatchSize;
            }
        }



        string _smtpDomain;

        /// <summary>
        /// This is the SMTP domain.
        /// </summary>
        public virtual string SmtpDomain
        {

            get
            {
                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_smtpDomain = _smtpDomain ??
                    ConfigurationManager.AppSettings[MyConstants.SMTP_DOMAIN]));
            }
        }




        string _smtpPassword;
        /// <summary>
        /// This is the SMTP password of the user.
        /// </summary>
        public virtual string SmtpPassword
        {

            get
            {
                return (_smtpPassword = _smtpPassword ??
                    (_smtpPassword = ConfigurationManager.AppSettings[MyConstants.SMTP_PASSWORD]));

            }
        }



        string _smtpPort;
        /// <summary>
        /// This is the SMTP port of the user.
        /// </summary>
        public virtual string SmtpPort
        {

            get
            {
                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_smtpPort = _smtpPort ??
                    ConfigurationManager.AppSettings[MyConstants.SMTP_PORT]));

            }
        }



        string _smtpServer;
        /// <summary>
        /// This is the name of the SMTP server.
        /// </summary>
        public virtual string SmtpServer
        {

            get
            {
                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_smtpUser = _smtpServer ??
                    (_smtpServer = ConfigurationManager.AppSettings[MyConstants.SMTP_SERVER])));
            }
        }



        string _smtpUser;
        /// <summary>
        /// This is the login id of the user for the SMTP server.
        /// </summary>
        public virtual string SmtpUser
        {

            get
            {
                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_smtpUser = _smtpUser ??
                    ConfigurationManager.AppSettings[MyConstants.SMTP_USER]));

            }
        }



        string _smsTestingDirectory;
        /// <summary>
        /// This is for setting up the testing directory for SMS.
        /// </summary>
        public virtual string SmsTestingDirectory
        {

            get
            {

                return CheckForNullStringAndThrowError_Helper(
                    MethodBase.GetCurrentMethod(),
                    (_smsTestingDirectory = _smsTestingDirectory ??
                    ConfigurationManager.AppSettings[MyConstants.SMS_TESTING_DIRECTORY]));
            }
        }


        string _isSmtpSelectedService;
        /// <summary>
        /// This allows us to use SendGrid of SMTP.
        /// </summary>
        public virtual string IsSmtpSelectedService
        {

            get
            {
                _isSmtpSelectedService = _isSmtpSelectedService ?? ConfigurationManager.AppSettings[MyConstants.IS_SMTP_SELECTED_SERVICE];


                if (!CheckForNullStringAndThrowError_Helper(MethodBase.GetCurrentMethod(), _isSmtpSelectedService)
                    .IsValidBoolean())
                    ThrowException_InvalidBool(MethodBase.GetCurrentMethod(), _isSmtpSelectedService);

                return _isSmtpSelectedService;

            }
        }



        string _websiteUrl;
        /// <summary>
        /// This is the website URL
        /// </summary>
        public virtual string WebsiteUrl
        {

            get
            {
                _websiteUrl = _websiteUrl ?? ConfigurationManager.AppSettings[MyConstants.IS_SMTP_SELECTED_SERVICE];


                if (!CheckForNullStringAndThrowError_Helper(MethodBase.GetCurrentMethod(), _websiteUrl)
                    .IsValidUrl())
                    ThrowException_InvalidUrl(MethodBase.GetCurrentMethod(), _websiteUrl);

                return _websiteUrl;

            }
        }



        string _websiteAnchorLink;
        /// <summary>
        /// This is the anchor link for the website that will be used to access website from the link
        /// </summary>
        public virtual string WebsiteAnchorLink
        {
            get
            {

                _websiteAnchorLink = _websiteAnchorLink ?? ConfigurationManager.AppSettings[MyConstants.WEBSITE_ANCHOR_LINK];

                if (!CheckForNullStringAndThrowError_Helper(MethodBase.GetCurrentMethod(), _websiteAnchorLink)
                    .IsValidUrl())
                    ThrowException_InvalidUrl(MethodBase.GetCurrentMethod(), _websiteAnchorLink);

                return _websiteAnchorLink;

            }
        }

        string _adminPassword;

        public virtual string AdminPassword
        {
            get
            {
                _adminPassword = _adminPassword ?? ConfigurationManager.AppSettings[MyConstants.ADMIN_PASSWORD];
                return CheckForNullStringAndThrowError_Helper(MethodBase.GetCurrentMethod(), _adminPassword);
            }
        }


        string _adminUserPhone;

        public virtual string AdminUserPhone
        {
            get
            {
                _adminUserPhone = _adminUserPhone ?? ConfigurationManager.AppSettings[MyConstants.ADMIN_USER_PHONE];
                return CheckForNullStringAndThrowError_Helper(MethodBase.GetCurrentMethod(), _adminUserPhone);


            }
        }



        string _verboseStr;
        public virtual bool IsVerbose
        {
            get
            {
                _verboseStr = _verboseStr ?? ConfigurationManager.AppSettings[MyConstants.IS_VERBOSE];
                return bool.Parse(CheckForNullStringAndThrowError_Helper(MethodBase.GetCurrentMethod(), _verboseStr));
            }
        }


        string _encryptedStr;
        public virtual bool IsEncrypted
        {
            get
            {
                _encryptedStr = _encryptedStr ?? ConfigurationManager.AppSettings[MyConstants.IS_ENCRYPTED];
                if (_encryptedStr.IsNullOrWhiteSpace())
                    return false;

                return bool.Parse(CheckForNullStringAndThrowError_Helper(MethodBase.GetCurrentMethod(), _encryptedStr));
            }
        }







        string _forceInitialization;
        //virtual bool _isVerboseBoo;
        public virtual bool IsForceInitialization
        {
            get
            {
                _forceInitialization = _forceInitialization ?? ConfigurationManager.AppSettings[MyConstants.IS_FORCE_INITIALIZATION];

                return bool.Parse(
                    CheckForNullStringAndThrowError_Helper(MethodBase.GetCurrentMethod(), _forceInitialization));
            }
        }

        #region Helpers
        private string CheckForNullStringAndThrowError_Helper(MethodBase methodBase, string variableValue)
        {


            if (variableValue.IsNullOrWhiteSpace())
            {
                InitializeError();
                _err.AddError("Empty string", MethodBase.GetCurrentMethod().Name);
                throw new Exception(_err.ToString());
            }

            return variableValue;

        }
        private void ThrowException_InvalidBool(MethodBase methodBase, string variableValue)
        {

            //_err.Add(string.Format("The bool {0} has an invalid value in the Web.Config file. It can only have a value of true or false. It's current value is: '{1}'.", variableName, variableValue), variableName);
            InitializeError();
            _err.AddError("Invalid Bool", MethodBase.GetCurrentMethod().Name);
            throw new Exception(_err.ToString());

        }
        private void ThrowException_InvalidInteger(MethodBase methodBase, string variableValue, string offendingName)
        {

            //_err.Add(string.Format("The {0} has an invalid integer value in the Web.Config file. It can only have a numeric value. It's current value is: '{1}'.", variableName, variableValue), variableName);
            InitializeError();
            _err.AddError("Invalid number", MethodBase.GetCurrentMethod().Name);
            throw new Exception(_err.ToString());

        }

        private void ThrowException_InvalidUrl(MethodBase methodBase, string variableValue)
        {

            //_err.Add(string.Format("The {0} has an invalid URL value in the Web.Config file.  It's current value is: '{1}'.", variableName, variableValue), variableName);
            InitializeError();
            _err.AddError("Invalid url", MethodBase.GetCurrentMethod().Name);

            throw new Exception(_err.ToString());

        }

        private void ThrowException_InvalidEmail(MethodBase methodBase, string variableValue)
        {

            //_err.Add(string.Format("The {0} has an invalid Email Address value in the Web.Config file.  It's current value is: '{1}'.", variableName, variableValue), variableName);
            InitializeError();
            _err.AddError("Invalid email", MethodBase.GetCurrentMethod().Name);
            throw new Exception(_err.ToString());

        }
        #endregion
    }
}
