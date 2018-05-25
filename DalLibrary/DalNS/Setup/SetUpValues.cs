using System;
using System.Configuration;
using System.Reflection;
using AliKuli.ConstantsNS;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.DAL.Setup;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using UserModelsLibrary.ModelsNS;


namespace DalLibrary.DalNS
{
    /// <summary>
    /// This gets all the global values
    /// Remember to set UserName each time you use this
    /// </summary>
    public class SetUpValues : ISetUpValues
    {

        readonly SetupDAL _setUpDAL;
        readonly ErrorSet _err;


        public SetUpValues(ApplicationDbContext db, IErrorSet errorsGloabal)
        {

            _err = new ErrorSet();
            _err.SetLibAndClass(Assembly.GetCallingAssembly().GetName().Name, this.GetType().Name);

            if (db.IsNull())
                _err.Add("Ctx is null", MethodBase.GetCurrentMethod());


            if (_err.HasErrors)
                throw new Exception(_err.ToString());

            _setUpDAL = new SetupDAL(db, errorsGloabal);
        }

        string _userName;
        public String UserName
        {
            private get { return _userName; }
            set
            {
                _userName = value;
                _err.UserName = _userName;
            }
        }

        string _bccEmailAddress;
        public string BccEmailAddress
        {
            get
            {
                return _bccEmailAddress ??
                    (_bccEmailAddress = GetFromSetUp(SetupEnum.BccEmailAddress)) ??
                    (_bccEmailAddress = new ConfigManagerHelper().AdminEmailAddress);
            }
        }



        string _defaultCountry;
        public string DefaultCountry
        {
            get
            {
                return _defaultCountry ??
                (_defaultCountry = GetFromSetUp(SetupEnum.DefaultCountry)) ??
                (_defaultCountry = new ConfigManagerHelper().CompanyName);

            }
        }

        //string _defaultCountryIdNoLen;
        //public string DefaultCountryIdNoLen
        //{
        //    get
        //    {
        //        return _defaultCountryIdNoLen ??
        //            (_defaultCountryIdNoLen = GetFromSetUp(SetupEnum.DefaultCountryIdNoLen)) ??
        //            (_defaultCountryIdNoLen = new ConfigManagerHelper().CountryIdCardNumberLength);


        //    }
        //}

        private string _companyName;

        public string CompanyName
        {
            get
            {
                return _companyName ??
                    (_companyName = GetFromSetUp(SetupEnum.CompanyName)) ??
                    (_companyName = new ConfigManagerHelper().CompanyName);
            }
        }

        string _adminCountryAbbreviation;
        public string AdminCountryAbbreviation
        {
            get
            {
                return _adminCountryAbbreviation ??
                (_adminCountryAbbreviation = GetFromSetUp(SetupEnum.CompanyName)) ??
                (_adminCountryAbbreviation = new ConfigManagerHelper().AdminCountryAbbreviation);

            }
        }



        string _defaultPageSize;
        public string DefaultPageSize
        {
            get
            {

                return _defaultPageSize ??
                    (_defaultPageSize = GetFromSetUp(SetupEnum.DefaultPageSize)) ??
                    (_defaultPageSize = new ConfigManagerHelper().DefaultPageSize);
            }
        }




        string _emailTestingDirectory;
        /// <summary>
        /// This is the testing directory for emails.
        /// </summary>
        public string EmailTestingDirectory
        {

            get
            {

                return _emailTestingDirectory ??
                    (_emailTestingDirectory = GetFromSetUp(SetupEnum.EmailTestingDirectory)) ??
                    (_emailTestingDirectory = new ConfigManagerHelper().EmailTestingDirectory);
            }
        }



        string _fromEmailAddress;
        /// <summary>
        /// This is the from Email address that shows up in all the emails.
        /// </summary>
        public string FromEmailAddress
        {

            get
            {
                return _fromEmailAddress ??
                    (_fromEmailAddress = GetFromSetUp(SetupEnum.FromEmailAddress)) ??
                    (_fromEmailAddress = new ConfigManagerHelper().AdminEmailAddress);
            }
        }




        string _ftpServerIp;
        /// <summary>
        /// This is the FTP server IP
        /// </summary>
        public string FtpServerIp
        {

            get
            {

                return _ftpServerIp ??
                    (_ftpServerIp = GetFromSetUp(SetupEnum.FtpServerIp));
            }
        }



        string _ftpUserName;
        /// <summary>
        /// This is the FTP username.
        /// </summary>
        public string FtpUserName
        {

            get
            {

                return _ftpUserName ??
                    (_ftpUserName = GetFromSetUp(SetupEnum.FtpUserName));


            }
        }



        string _ftpPassword;
        /// <summary>
        /// This is the FTP password
        /// </summary>
        public string FtpPassword
        {

            get
            {
                if (_ftpPassword.IsNullOrWhiteSpace())
                    _ftpPassword = GetFromSetUp(SetupEnum.FtpPassword);

                return _ftpPassword ??
                    (_ftpPassword = GetFromSetUp(SetupEnum.FtpPassword));


            }
        }


        string _isSendBccStr;
        /// <summary>
        /// If true, then this sends BCC messages to the admin.
        /// </summary>
        public bool IsSendBcc
        {

            get
            {
                _isSendBccStr = _isSendBccStr ??
                    (_isSendBccStr = GetFromSetUp(SetupEnum.IsSendBcc)) ??
                    (_isSendBccStr = new ConfigManagerHelper().IsSendBcc);

                if(_isSendBccStr.IsNullOrEmpty())
                    _err.Add("The IsSendBCC is empty in the Web.Config file", MethodBase.GetCurrentMethod());


                if (!_isSendBccStr.IsValidBoolean())
                    _err.Add(string.Format("The IsSendBCC has an invalid value in the Web.Config file. It can only have a value of true or false. It's current value is: '{0}'.", _isSendBccStr), MethodBase.GetCurrentMethod());
                return bool.Parse(_isSendBccStr);
            }
        }


        string _sendGridUserName;
        /// <summary>
        /// This is the user name for SendGrid
        /// </summary>
        public string SendGridUserName
        {

            get
            {
                return _sendGridUserName ??
                    (_sendGridUserName = GetFromSetUp(SetupEnum.SendGridUserName));
            }
        }


        string _sendGridPassword;
        /// <summary>
        /// This is the password for SendGrid,.
        /// </summary>
        public string SendGridPassword
        {

            get
            {

                return _sendGridPassword ??
                    (_sendGridPassword = GetFromSetUp(SetupEnum.SendGridPassword));


            }
        }



        string _showStartUpScreenOnStartup;
        /// <summary>
        /// If true, start up screen will show
        /// </summary>
        public string ShowStartUpScreenOnStartup
        {

            get
            {

                return _showStartUpScreenOnStartup ??
                    (_showStartUpScreenOnStartup = GetFromSetUp(SetupEnum.ShowStartUpScreenOnStartup));


            }
        }



        string _scratchCardMaxBatchSize;
        /// <summary>
        /// This is the default batch size for a scratch card batch
        /// </summary>
        public string ScratchCardMaxBatchSize
        {

            get
            {

                return _scratchCardMaxBatchSize ??
                    (_scratchCardMaxBatchSize = GetFromSetUp(SetupEnum.ScratchCardMaxBatchSize)) ??
                    (_scratchCardMaxBatchSize = new ConfigManagerHelper().ScratchCardMaxBatchSize);


            }
        }



        string _smtpDomain;

        /// <summary>
        /// This is the SMTP domain.
        /// </summary>
        public string SmtpDomain
        {

            get
            {
                return _smtpDomain ??
                    (_smtpDomain = GetFromSetUp(SetupEnum.SmtpDomain)) ??
                    (_smtpDomain = new ConfigManagerHelper().SmtpDomain);
            }
        }




        string _smtpPassword;
        /// <summary>
        /// This is the SMTP password of the user.
        /// </summary>
        public string SmtpPassword
        {

            get
            {
                return _smtpPassword ??
                    (_smtpPassword = GetFromSetUp(SetupEnum.SmtpPassword)) ??
                    (_smtpPassword = new ConfigManagerHelper().SmtpPassword);
            }
        }



        string _smtpPort;
        /// <summary>
        /// This is the SMTP port of the user.
        /// </summary>
        public string SmtpPort
        {

            get
            {
                return _smtpPort ??
                    (_smtpPort = GetFromSetUp(SetupEnum.SmtpPort)) ??
                    (_smtpPort = new ConfigManagerHelper().SmtpPort);
            }
        }



        string _smtpServer;
        /// <summary>
        /// This is the name of the SMTP server.
        /// </summary>
        public string SmtpServer
        {

            get
            {
                return _smtpServer ??
                    (_smtpServer = GetFromSetUp(SetupEnum.SmtpServer)) ??
                    (_smtpServer = new ConfigManagerHelper().SmtpServer);
            }
        }



        string _smtpUser;
        /// <summary>
        /// This is the login id of the user for the SMTP server.
        /// </summary>
        public string SmtpUser
        {

            get
            {
                return _smtpUser ??
                    (_smtpUser = GetFromSetUp(SetupEnum.SmtpUser)) ??
                    (_smtpUser = new ConfigManagerHelper().SmtpUser);

            }
        }



        string _smsTestingDirectory;
        /// <summary>
        /// This is for setting up the testing directory for SMS.
        /// </summary>
        public string SmsTestingDirectory
        {

            get
            {
                return _smsTestingDirectory ??
                    (_smsTestingDirectory = GetFromSetUp(SetupEnum.SmsTestingDirectory)) ??
                    (_smsTestingDirectory = new ConfigManagerHelper().SmsTestingDirectory);
            }
        }

        static string _isSmtpSelectedService;

        /// <summary>
        /// This allows us to use SendGrid of SMTP.
        /// </summary>
        public bool IsSmtpSelectedService
        {

            get
            {
                _isSmtpSelectedService = _isSmtpSelectedService ??
                    GetFromSetUp(SetupEnum.IsSmtpSelectedService) ??
                    new ConfigManagerHelper().IsSmtpSelectedService;

                return BooleanCheckHelp("IsSmtpSelectedService", _isSmtpSelectedService);
            }
        }

        private bool BooleanCheckHelp(string variableName, string variableValue)
        {
            if (variableValue.IsNullOrWhiteSpace())
                _err.Add(string.Format("The {0} is empty in the Web.Config file", variableName), MethodBase.GetCurrentMethod());


            if (!variableValue.IsValidBoolean())
                _err.Add(string.Format("The {0} has an invalid value in the Web.Config file. It can only have a value of true or false. It's current value is: '{1}'.", variableName, variableValue), MethodBase.GetCurrentMethod());

            return bool.Parse(variableValue);
        }



        string _websiteUrl;
        /// <summary>
        /// This is the website URL
        /// </summary>
        public string WebsiteUrl
        {

            get
            {
                return _websiteUrl ??
                    (_websiteUrl = GetFromSetUp(SetupEnum.WebsiteUrl)) ??
                    (_websiteUrl = new ConfigManagerHelper().WebsiteUrl);
            }
        }



        string _websiteAnchorLink;
        /// <summary>
        /// This is the anchor link for the website that will be used to access website from the link
        /// </summary>
        public string WebsiteAnchorLink
        {
            get
            {
                return _websiteAnchorLink ??
                    (_websiteAnchorLink = string.Format(@"<a href=\{0}\>{1}\Website</a>", this.WebsiteUrl, this.CompanyName));
            }
        }

        string _adminPassword;

        public string AdminPassword
        {
            get
            {

                return _adminPassword ?? (_adminPassword = new ConfigManagerHelper().AdminPassword); 
            }
        }


        string _adminUser;

        public string AdminUser
        {
            get
            {
                return _adminUser ??
                    (_adminUser = new ConfigManagerHelper().AdminUserPhone);

            }
        }

        public bool IsVerbose
        {
            get
            {
                return new ConfigManagerHelper().IsVerbose;
            }
        }




        /// <summary>
        /// This is the helper load property. It
        /// </summary>
        /// <param name="setupEnum"></param>
        /// <returns></returns>
        private string GetFromSetUp(SetupEnum setupEnum)
        {
            string setupEnumStr = setupEnum.ToString();

            if (setupEnumStr.IsNullOrWhiteSpace())
            {
                string errorStr = string.Format(
                    "Default {0} not setup.",
                    setupEnumStr.ToTitleSentance());

                _err.Add(
                    string.Format("Default {0} not setup.",
                    setupEnumStr.ToTitleSentance()),
                    MethodBase.GetCurrentMethod());
            }

            //throw new NotImplementedException(); //todo
            var dc = _setUpDAL.FindForName(setupEnumStr);

            if (dc.IsNull())
                return "";

            //Save it in the cache.

            return dc.Name.ToTitleSentance();

        }


    }

}