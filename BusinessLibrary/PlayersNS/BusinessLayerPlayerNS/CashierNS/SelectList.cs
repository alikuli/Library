using System.Web.Mvc;

namespace UowLibrary.PlayersNS.CashierNS
{
    public partial class CashierBiz
    {

        public override string SelectListCacheKey
        {
            get { return "CashierSelectList"; }
        }



        public SelectList SelectListCashierCategory
        {
            get { return CashierCategoryBiz.SelectList(); }
        }






    }
}
