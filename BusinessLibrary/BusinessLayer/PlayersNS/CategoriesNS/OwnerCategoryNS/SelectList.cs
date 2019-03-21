using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.OwnerCategoryNS
{
    public partial class OwnerCategoryBiz : BusinessLayer<OwnerCategory>
    {



        /// <summary>
        /// This loads the Language Data into the select list
        /// </summary>
        /// <returns></returns>

        public override string SelectListCacheKey
        {
            get { return "OwnerCategorySelectListData"; }
        }



    }
}
