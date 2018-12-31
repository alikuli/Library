using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.PersonCategoryNS
{
    public partial class PersonCategoryBiz : BusinessLayer<PersonCategory>
    {



        /// <summary>
        /// This loads the Language Data into the select list
        /// </summary>
        /// <returns></returns>

        public override string SelectListCacheKey
        {
            get { return "PersonCategorySelectListData"; }
        }



    }
}
