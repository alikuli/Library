using System.Web.Mvc;

namespace UowLibrary.PlayersNS.OwnerNS
{
    public partial class OwnerBiz
    {

        public override string SelectListCacheKey
        {
            get { return "OwnerSelectList"; }
        }



        public SelectList SelectListOwnerCategory
        {
            get { return OwnerCategoryBiz.SelectList(); }
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
