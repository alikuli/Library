using AliKuli.Extentions;
using AliKuli.Tools;
using AliKuli.UtilitiesNS.RandomNumberGeneratorNS;
using DalLibrary.Interfaces;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.PlacesNS.EmailAddressNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.PersonNS;

namespace UowLibrary.EmailAddressNS
{
    public partial class EmailAddressBiz : ContactAbstractBiz<EmailAddress>
    {
        //PersonBiz _personBiz;
        public  EmailAddressBiz(IRepositry<EmailAddress> entityDal, BizParameters bizParameters, PersonBiz personBiz, CountryBiz countryBiz)
            : base(entityDal, bizParameters, personBiz, countryBiz)
        {
            //_personBiz = personBiz;

        }


        //public override string SelectListCacheKey
        //{
        //    get { return "EmailAddressesSelectListData"; }

        //}


        /// <summary>
        /// This sends the user an email verification code to their email address to verify their email
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailAddressId"></param>
        public void SendEmailConfirmation(string emailAddressId)
        {
            UserId.IsNullThrowException("You are not logged in");
            emailAddressId.IsNullOrWhiteSpaceThrowArgumentException("emailAddressId");

            //get the email address.
            EmailAddress emailAddress = Find(emailAddressId);
            emailAddress.IsNullThrowException("Email Not found");

            RandomNoGenerator randomGen = new RandomNoGenerator(5);
            string verificationNumber = randomGen.GetRandomNumber(1000);
            verificationNumber.IsNullOrWhiteSpaceThrowArgumentException("Random number not generated");

            emailAddress.VerificationNumber = verificationNumber.ToString();
            emailAddress.VerificationDateComplex.SetToTodaysDate(UserName);
            //time allowed for verification is 60 min.



            string body = string.Format("Your verification Id is: {0}", verificationNumber);
            string subject = string.Format("Verification for email: {0}", emailAddress.Name);
            string smtpServer = ConfigManagerHelper.SmtpServer;

            string portStr = ConfigManagerHelper.SmtpPort;
            portStr.IsNullOrWhiteSpaceThrowArgumentException("No port has been listed.");
            int portInt;
            bool success = int.TryParse(portStr, out portInt);
            if (!success)
            {
                throw new Exception("The port number is not a number.");
            }

            
            string userName = ConfigManagerHelper.SmtpUser;
            string from = ConfigManagerHelper.SmtpFromEmailAddress;
            string password = ConfigManagerHelper.SmtpPassword;

            List<string> sendToList = new List<string>();
            sendToList.Add(emailAddress.Name);
            Emails emails = new Emails();

            try
            {
                emails.SendEmailMsg(
                    smtpServer,
                    portInt,
                    userName,
                    password,
                    from,
                    subject,
                    body,
                    sendToList,
                    null);
                emailAddress.VerificationStatusEnum = VerificaionStatusENUM.Mailed;
                
                UpdateAndSave(emailAddress);

                ErrorsGlobal.AddMessage(string.Format("Verification Email Sent to: '{0}'",emailAddress.Name));

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Unable to send Email to: '{0}'",emailAddress.Name), MethodBase.GetCurrentMethod(), e);
            }




        }

        public bool CheckEmailVerificationCode(string emailAddressId, string verificationCode)
        {
            UserId.IsNullThrowException("You are not logged in");
            emailAddressId.IsNullOrWhiteSpaceThrowArgumentException("emailAddressId");
            verificationCode.IsNullOrWhiteSpaceThrowArgumentException("verificationCode");

            EmailAddress emailAddress = Find(emailAddressId);
            emailAddress.IsNullThrowException("Email not found");
            if(emailAddress.VerificationNumber == verificationCode)
            {
                //verification code matches... verifiy the email address
                //emailAddress.IsVerified = true;
                emailAddress.VerificationDateComplex.SetToTodaysDate(UserName);
                emailAddress.VerificationStatusEnum = VerificaionStatusENUM.Verified;
                emailAddress.VerificationDateComplex.SetToTodaysDate(UserName);

                UpdateAndSave(emailAddress);
                ErrorsGlobal.AddMessage(string.Format("Email: '{0} has been VERIFIED'", emailAddress.Name));
                return true;
            }

            ErrorsGlobal.AddMessage(string.Format("Email: '{0}' -VERIFICATION FAILED!", emailAddress.Name));
            return false;
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


        //private static void gmailSettings(out string smtpServer, out int portInt, out string emailAddress, out string userName, out string from, out string password)
        //{
        //    //gmail
        //    password = "Xray786!";
        //    smtpServer = "smtp.gmail.com";

        //    emailAddress = "alikuli62@gmail.com";
        //    userName = "alikuli62";

        //    string port = "587";
        //    bool success = int.TryParse(port, out portInt);

        //    if (!success)
        //    {
        //        portInt = 587;
        //    }

        //    //this does not work.
        //    from = "alikuli@dhoondo-nokar.com";
        //}


        public void UpdateAndSaveDefaultAddress(string addressId)
        {
            UserId.IsNullOrWhiteSpaceThrowArgumentException("not logged in");
            addressId.IsNullOrWhiteSpaceThrowArgumentException("AddressId is null");

            EmailAddress emailAddress = Find(addressId);
            emailAddress.IsNullThrowException("Address");


            Person person = UserBiz.GetPersonFor(UserId);
            person.IsNullThrowException("Person not found");

            person.DefaultEmailAddressId = addressId;
            PersonBiz.UpdateAndSave(person);
        }
    }
}
