using System.Web.Mvc;

namespace UowLibrary.PlayersNS.BankNS
{
    public partial class BankBiz
    {

        public override string SelectListCacheKey
        {
            get { return "BankSelectList"; }
        }



        public SelectList SelectListBankCategory
        {
            get { return BankCategoryBiz.SelectList(); }
        }






    }
}
