using System.Web.Mvc;

namespace UowLibrary.PlayersNS.ProductApproverNS
{
    public partial class ProductApproverBiz
    {

        public override string SelectListCacheKey
        {
            get { return "ProductApproverSelectList"; }
        }



        public SelectList SelectListProductApproverCategory
        {
            get { return ProductApproverCategoryBiz.SelectList(); }
        }

    }
}
