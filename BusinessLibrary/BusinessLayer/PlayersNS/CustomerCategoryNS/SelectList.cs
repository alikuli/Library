using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.CustomerCategoryNS
{
    public partial class CustomerCategoryBiz : BusinessLayer<CustomerCategory>
    {

        /// <summary>
        /// This loads the Language Data into the select list
        /// </summary>
        /// <returns></returns>

        public override string SelectListCacheKey
        {
            get { return "CustomerCategorySelectListData"; }
        }



    }
}
