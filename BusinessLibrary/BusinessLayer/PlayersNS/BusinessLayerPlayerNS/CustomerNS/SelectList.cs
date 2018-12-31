using System.Web.Mvc;

namespace UowLibrary.PlayersNS.CustomerNS
{
    public partial class CustomerBiz
    {

        public override string SelectListCacheKey
        {
            get { return "CustomerSelectList"; }
        }

        public SelectList SelectListCustomerCategory
        {
            get { return CustomerCategoryBiz.SelectList(); }
        }

        //public SelectList SelectListUser {
        //    get { return UserBiz.SelectList(); }
        //}

    }
}
