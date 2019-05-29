using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MarketPlace.Web6.App_Start
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            ConfigManagerHelper configManagerHelper = new ConfigManagerHelper();

            // Plug in your email service here to send an email.
            //string mailPassword = "Nid@786!";
            //string emailAccount = "alikuli62@gmail.com";
            //client.Port = 587;
            //client.Host = "smtp.gmail.com";
            //client.Timeout = 10000;
            //win12.hosterpk.com 

            string mailPassword = configManagerHelper.SmtpPassword;
            string emailAccount = configManagerHelper.SmtpFromEmailAddress;
            SmtpClient client = new SmtpClient();

            client.Port = configManagerHelper.SmtpPort.ToInt();
            client.Host = configManagerHelper.SmtpServer;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(emailAccount, mailPassword);
            

            return client.SendMailAsync(emailAccount, message.Destination, message.Subject, message.Body);
            //return Task.FromResult(0);
        }
    }
}