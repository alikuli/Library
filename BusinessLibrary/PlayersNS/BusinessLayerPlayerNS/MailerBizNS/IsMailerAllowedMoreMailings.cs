using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {

        /// <summary>
        /// This decides if mailer is allowed any more mailings.
        /// </summary>
        private bool IsMailerAllowedMoreMailings(Mailer mailer)
        {
            bool allowed = numberOfOpenMailingsAllowedToMailer >= TotalOpenMailingsForMailer(mailer).TotalOutstanding;
            return allowed;
        }



    }
}
