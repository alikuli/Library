
using System.Linq;
using System.Web.Mvc;
namespace UowLibrary.CashEncashmentTrxNS
{
    public partial class CashEncashmentTrxBiz
    {
        public override string SelectListCacheKey
        {
            get { return "CashEncashmentTrxsSelectListData"; }
        }


        public SelectList SelectList_Only_Unpaid_Trx()
        {
            var allItems = this.FindAll().Where(x =>
                x.SecretNumberEntered.Value == null ||
                x.SecretNumberEntered.Value.Trim() == "" ||
                x.SecretNumberEntered.Value.Trim() != x.SecretNumber.Trim());
            return SelectList_Engine(allItems);

        }
    }
}
