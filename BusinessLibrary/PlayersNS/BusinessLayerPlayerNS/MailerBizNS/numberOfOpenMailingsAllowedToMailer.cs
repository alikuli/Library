using ConfigManagerLibrary;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {

        /// <summary>
        /// These are the number of incomplete mailings allowed at any given time.
        /// </summary>
        private int numberOfOpenMailingsAllowedToMailer
        {
            get
            {
                return VerificationConfig.Number_Of_Open_Mailings_Allowed;
            }
        }





    }
}
