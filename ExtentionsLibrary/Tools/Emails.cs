using AliKuli.Extentions;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace AliKuli.Tools
{
    public class Emails
    {
        public void SendEmailMsg(string smtpServer, int port, string userName, string password, string from, string subject, string body, List<string> sendToList, List<string> bccList)
        {
            SmtpClient client = new SmtpClient(smtpServer, port);
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(userName, password);
            MailMessage mailMessage = CreateMailMessage(from, subject, body, sendToList, bccList);
            client.Send(mailMessage);

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

        SmtpClient Client { get; set; }
    }
}
