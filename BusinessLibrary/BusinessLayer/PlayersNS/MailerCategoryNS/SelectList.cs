using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.MailerNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PlayersNS.MailerNS;

namespace UowLibrary.PlayersNS.MailerCategoryNS
{
    public partial class MailerCategoryBiz : BusinessLayer<MailerCategory>
    {



        /// <summary>
        /// This loads the Language Data into the select list
        /// </summary>
        /// <returns></returns>

        public override string SelectListCacheKey
        {
            get { return "MailerCategorySelectListData"; }
        }



    }
}
