using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.CashierCategoryNS
{
    public partial class CashierCategoryBiz : BusinessLayer<CashierCategory>
    {



        /// <summary>
        /// This loads the Language Data into the select list
        /// </summary>
        /// <returns></returns>

        public override string SelectListCacheKey
        {
            get { return "CashierCategorySelectListData"; }
        }



    }
}
