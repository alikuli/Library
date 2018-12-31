using System.Web.Mvc;

namespace UowLibrary.PlayersNS.DeliverymanNS
{
    public partial class DeliverymanBiz
    {

        public override string SelectListCacheKey
        {
            get { return "DeliverymanSelectList"; }
        }

        public SelectList SelectListDeliverymanCategory
        {
            get { return deliverymanCategoryBiz.SelectList(); }
        }

        //public SelectList SelectListUser
        //{
        //    get { return UserBiz.SelectList(); }
        //}

    }
}
