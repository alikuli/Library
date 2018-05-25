//using System.Linq;
//using EnumLibrary.EnumNS;

//namespace AliKuli.CacheLibrary
//{
//    /// <summary>
//    /// This gets all the global values
//    /// </summary>
//    public class GlobalValuesVM
//    {
//        public GlobalValuesVM()
//        {

//        }

//        public GlobalValuesVM(ApplicationDbContext dbIn)
//        {
//            var s = db.SetUps;


//            try
//            {
//                DefaultCountry = s.FirstOrDefault(x => x.NameEncryptDecrypt == SetupEnum.DefaultCountry.ToString() && x.Deleted == false).Value;

//            }
//            catch
//            {
//                DefaultCountry = string.Empty;

//            }

//            try
//            {
//                ScratchCardMaxBatchSize = s.FirstOrDefault(x => x.NameEncryptDecrypt == SetupEnum.ScratchCardMaxBatchSize.ToString() && x.Deleted == false).Value;

//            }
//            catch
//            {
//                ScratchCardMaxBatchSize = string.Empty;

//            }


//            try
//            {
//                FtpPassword = s.FirstOrDefault(x => x.NameEncryptDecrypt == SetupEnum.FtpPassword.ToString() && x.Deleted == false).Value;

//            }
//            catch
//            {
//                FtpPassword = string.Empty;

//            }

//            try
//            {
//                string theName = SetupEnum.FtpServerIp.ToString().ToLower();
//                FtpServerIp = s.FirstOrDefault(x => x.NameEncryptDecrypt.ToLower() == theName && x.Deleted == false).Value;

//            }
//            catch
//            {
//                FtpServerIp = string.Empty;

//            }


//            try
//            {
//                FtpUserName = s.FirstOrDefault(x => x.NameEncryptDecrypt == SetupEnum.FtpUserName.ToString() && x.Deleted == false).Value;

//            }
//            catch
//            {
//                FtpUserName = string.Empty;

//            }


//            try
//            {
//                CompanyName = s.FirstOrDefault(x => x.NameEncryptDecrypt == SetupEnum.CompanyName.ToString() && x.Deleted == false).Value;

//            }
//            catch
//            {
//                CompanyName = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                SendGridPassword = s.FirstOrDefault(x => x.NameEncryptDecrypt == "SendGridUserName" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                SendGridUserName = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                SendGridPassword = s.FirstOrDefault(x => x.NameEncryptDecrypt == "SendGridPassword" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                SendGridPassword = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                ShowStartUpScreenOnStartup = s.FirstOrDefault(x => x.NameEncryptDecrypt == "ShowStartUpScreenOnStartup" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                ShowStartUpScreenOnStartup = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                DefaultPageSize = s.FirstOrDefault(x => x.NameEncryptDecrypt == "DefaultPageSize" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                DefaultPageSize = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                SmtpServer = s.FirstOrDefault(x => x.NameEncryptDecrypt == "SmtpServer" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                SmtpServer = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                SmtpUser = s.FirstOrDefault(x => x.NameEncryptDecrypt == "SmtpUser" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                SmtpUser = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                SmtpPassword = s.FirstOrDefault(x => x.NameEncryptDecrypt == "SmtpPassword" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                SmtpPassword = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                BccEmailAddress = s.FirstOrDefault(x => x.NameEncryptDecrypt == "BccEmailAddress" && x.Deleted == false).Value;
//            }
//            catch
//            {
//                BccEmailAddress = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                FromEmailAddress = s.FirstOrDefault(x => x.NameEncryptDecrypt == "FromEmailAddress" && x.Deleted == false).Value;
//            }
//            catch
//            {
//                FromEmailAddress = string.Empty;
//            }

//            //---------------------------------------------------------------------------------
//            try
//            {
//                UseSendgridOrSmtp = s.FirstOrDefault(x => x.NameEncryptDecrypt == "UseSendgridOrSmtp" && x.Deleted == false).Value;
//            }
//            catch
//            {
//                UseSendgridOrSmtp = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                WebsiteUrl = s.FirstOrDefault(x => x.NameEncryptDecrypt == "WebsiteUrl" && x.Deleted == false).Value;
//            }
//            catch
//            {
//                WebsiteUrl = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                SmtpPort = s.FirstOrDefault(x => x.NameEncryptDecrypt == "SmtpPort" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                SmtpPort = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                IsSendBcc = s.FirstOrDefault(x => x.NameEncryptDecrypt == "IsSendBcc" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                IsSendBcc = string.Empty;

//            }
//            //---------------------------------------------------------------------------------
//            try
//            {
//                SmtpDomain = s.FirstOrDefault(x => x.NameEncryptDecrypt == "SmtpDomain" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                SmtpDomain = string.Empty;

//            }


//            try
//            {
//                EmailTestingDirectory = s.FirstOrDefault(x => x.NameEncryptDecrypt == "EmailTestingDirectory" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                EmailTestingDirectory = string.Empty;



//            }
//            try
//            {
//                SmsTestingDirectory = s.FirstOrDefault(x => x.NameEncryptDecrypt == "SmsTestingDirectory" && x.Deleted == false).Value;

//            }
//            catch
//            {
//                SmsTestingDirectory = string.Empty;



//            }

//        }

//        public string BccEmailAddress { get; set; }
//        public string CompanyName { get; set; }
//        public string DefaultCountry { get; set; }
//        public string DefaultPageSize { get; set; }
//        public string EmailTestingDirectory { get; set; }


//        public string FromEmailAddress { get; set; }
//        public string FtpServerIp { get; set; }
//        public string FtpUserName { get; set; }
//        public string FtpPassword { get; set; }


//        public string IsSendBcc { get; set; }
//        public string SendGridUserName { get; set; }
//        public string SendGridPassword { get; set; }
//        public string ShowStartUpScreenOnStartup { get; set; }

//        public string ScratchCardMaxBatchSize { get; set; }

//        public string SmtpDomain { get; set; }
//        public string SmtpPassword { get; set; }
//        public string SmtpPort { get; set; }
//        public string SmtpServer { get; set; }
//        public string SmtpUser { get; set; }
//        public string SmsTestingDirectory { get; set; }

//        public string UseSendgridOrSmtp { get; set; }

//        public string WebsiteUrl { get; set; }
//        public string WebsiteAnchorLink
//        {
//            get
//            {
//                return "<a href=\"" + this.WebsiteUrl + "\">" + this.CompanyName + " Website</a>";
//            }
//        }
//    }
//}