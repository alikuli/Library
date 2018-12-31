using System.Web.Mvc;

namespace UowLibrary.PlayersNS.SalesmanNS
{
    public partial class SalesmanBiz
    {

        public override string SelectListCacheKey
        {
            get { return "SalesmanSelectList"; }
        }



        public SelectList SelectListSalesmanCategory
        {
            get { return SalesmanCategoryBiz.SelectList(); }
        }



        //public SelectList SelectListUser
        //{
        //    get { return UserBiz.SelectList(); }
        //}



        //public SelectList SelectListBillAddressesFor(string userId)
        //{
        //    return UserBiz.AddressBiz.SelectListBillAddressFor(userId);
        //}


        //public SelectList SelectListShipAddressesFor(string userId)
        //{
        //    return UserBiz.AddressBiz.SelectListShipAddressFor(userId);
        //}
    }
}
