using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.BankCategoryNS
{
    public partial class BankCategoryBiz : BusinessLayer<BankCategory>
    {



        /// <summary>
        /// This loads the Language Data into the select list
        /// </summary>
        /// <returns></returns>

        public override string SelectListCacheKey
        {
            get { return "BankCategorySelectListData"; }
        }



    }
}
