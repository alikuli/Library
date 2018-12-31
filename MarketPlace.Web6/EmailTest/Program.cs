using AliKuli.Tools;
using System;
using System.Collections.Generic;

namespace EmailTest
{
    class Program
    {
        static void Main(string[] args)
        {

            string smtpServer;
            int portInt;
            string emailAddress;
            string userName;
            string from;
            string password;
            gmailSettings(out smtpServer, out portInt, out emailAddress, out userName, out from, out password);
            //dhoondoSettings(out smtpServer, out portInt, out emailAddress, out userName, out from, out password);


            string verificationNumber = "99999";
            string body = string.Format("Your verification Id is: {0}", verificationNumber);
            string subject = string.Format("Verification for email 3: {0}", emailAddress);
            List<string> sendToList = new List<string>();
            sendToList.Add(emailAddress);


            //string smtpServer = "mail.dhoondo-nokar.com";
            //string port = "465";
            //int portInt;
            //string emailAddress = "alikuli@dhoondo-nokar.com";
            //string verificationNumber = "99999";
            //string userName = "alikuli";
            //string password = "@liKuli786!";
            //string body = string.Format("Your verification Id is: {0}", verificationNumber);
            //string subject = string.Format("Verification for email: {0}", emailAddress);
            //string from = "alikuli@dhoondo-nokar.com";
            //List<string> sendToList = new List<string>();
            //sendToList.Add(emailAddress);
            //bool success = int.TryParse(port, out portInt);
            //if (!success)
            //{
            //    portInt = 25;
            //}


            Emails emails = new Emails(smtpServer, portInt, userName, password,true);

            Console.WriteLine("Sending email...");
            emails.SendEmailMsg(from, subject, body, sendToList, null);
            Console.WriteLine("Email sent!");
            Console.ReadLine();

        }

        private static void gmailSettings(out string smtpServer, out int portInt, out string emailAddress, out string userName, out string from, out string password)
        {
            //gmail
            password = "Xray786!";
            smtpServer = "smtp.gmail.com";
            string port = "587";

            emailAddress = "alikuli62@gmail.com";
            userName = "alikuli62";
            bool success = int.TryParse(port, out portInt);
            if (!success)
            {
                portInt = 25;
            }
            from = "alikuli@dhoondo-nokar.com";
        }
        //private static void dhoondoSettings(out string smtpServer, out int portInt, out string emailAddress, out string userName, out string from, out string password)
        //{
        //    //gmail
        //    password = "zK8ipd7O38";
        //    //password = "@liKuli786!";
        //    smtpServer = "mail.dhoondo-nokar.com";
        //    string port = "25";

        //    emailAddress = "alikuli@dhoondo-nokar.com";
        //    userName = "dhoondo";
        //    bool success = int.TryParse(port, out portInt);
        //    if (!success)
        //    {
        //        portInt = 25;
        //    }
        //    from = "alikuli@dhoondo-nokar.com";
        //}
    }
}
