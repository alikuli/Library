namespace DalLibrary.DAL.Setup
{
    public interface ISetUpValues
    {
        string AdminPassword { get; }
        string AdminUser { get; }
        string BccEmailAddress { get; }
        string CompanyName { get; }
        string DefaultCountry { get; }
        string AdminCountryAbbreviation { get; }
        //string DefaultCountryIdNoLen { get; }
        string DefaultPageSize { get; }
        string EmailTestingDirectory { get; }
        string FromEmailAddress { get; }
        string FtpPassword { get; }
        string FtpServerIp { get; }
        string FtpUserName { get; }
        bool IsSendBcc { get; }
        bool IsSmtpSelectedService { get; }
        bool IsVerbose { get; }
        string ScratchCardMaxBatchSize { get; }
        string SendGridPassword { get; }
        string SendGridUserName { get; }
        string ShowStartUpScreenOnStartup { get; }
        string SmsTestingDirectory { get; }
        string SmtpDomain { get; }
        string SmtpPassword { get; }
        string SmtpPort { get; }
        string SmtpServer { get; }
        string SmtpUser { get; }
        string UserName { set; }
        string WebsiteAnchorLink { get; }
        string WebsiteUrl { get; }
    }
}
