using AliKuli.Extentions;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace AliKuli.ToolsNS
{
    public class Emails
    {
        public Emails(string smtpServer, int port, string userName, string password, bool isEnableSsl = true)
        {
            SmtpServer = smtpServer;
            Port = port;
            UserName = userName;
            Password = password;
            IsEnableSsl = isEnableSsl;
            Client = CreateClient();
        }
        
        string SmtpServer{get;set;}
        int Port {get;set;} 
        string UserName{get;set;} 
        string Password {get;set;}
        bool IsEnableSsl { get; set; }
        
        SmtpClient Client { get; set; }

        

        public void SendEmailMsg(string from, string subject, string body, List<string> sendToList, List<string> bccList)
        {
            
            MailMessage mailMessage = CreateMailMessage(from, subject, body, sendToList, bccList);
            Client.Send(mailMessage);

        }

        private SmtpClient CreateClient()
        {
            SmtpClient client = new SmtpClient(SmtpServer, Port);
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            if (IsEnableSsl)
                client.EnableSsl = true;

            client.Credentials = new NetworkCredential(UserName, Password);
            return client;
        }



        MailMessage CreateMailMessage(string from, string subject, string body, List<string> sendToList, List<string> bccList)
        {
            from.IsNullOrWhiteSpaceThrowArgumentException("from");
            sendToList.IsNullOrEmptyThrowException("Send To List");
            MailMessage mailMessage = new MailMessage();

            MailAddress fromMailAddress = new MailAddress(from);
            mailMessage.From = fromMailAddress;

            //Make Send to List
            sendToList.IsNullOrEmptyThrowException("Send to addresses is empty");

            //add send to addresses
            foreach (var sendTo in sendToList)
            {
                MailAddress sendToMailAddress = new MailAddress(sendTo);
                mailMessage.To.Add(sendToMailAddress);
            }

            if (!bccList.IsNullOrEmpty())
            {
                foreach (var bcc in bccList)
                {
                    MailAddress sendToMailAddress = new MailAddress(bcc);
                    mailMessage.Bcc.Add(sendToMailAddress);

                }
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;

            return mailMessage;
        }

    }
}
