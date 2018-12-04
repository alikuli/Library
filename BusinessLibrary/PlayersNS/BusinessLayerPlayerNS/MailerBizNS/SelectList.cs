using AliKuli.Extentions;
using EnumLibrary.EnumNS.VerificationNS;
using System.Web.Mvc;

namespace UowLibrary.MailerNS
{
    public partial class MailerBiz
    {

        public SelectList SelectListTrustLevel
        {
            get
            {
                return EnumExtention.ToSelectListSorted<TrustLevelENUM>(TrustLevelENUM.Unknown);
            }
        }

        public override string SelectListCacheKey
        {
            get { return "MailerSelectList"; }
        }

        //public SelectList SelectListUsers
        //{
        //    get
        //    {
        //        return UserBiz.SelectList();
        //    }

        //}


    }
}
