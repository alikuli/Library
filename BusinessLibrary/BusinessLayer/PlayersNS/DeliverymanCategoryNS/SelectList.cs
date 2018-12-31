using ModelsClassLibrary.ModelsNS.PlayersNS;

namespace UowLibrary.PlayersNS.DeliverymanCategoryNS
{
    public partial class DeliverymanCategoryBiz : BusinessLayer<DeliverymanCategory>
    {

        /// <summary>
        /// This loads the Language Data into the select list
        /// </summary>
        /// <returns></returns>

        public override string SelectListCacheKey
        {
            get { return "DeliverymanCategorySelectListData"; }
        }



    }
}
