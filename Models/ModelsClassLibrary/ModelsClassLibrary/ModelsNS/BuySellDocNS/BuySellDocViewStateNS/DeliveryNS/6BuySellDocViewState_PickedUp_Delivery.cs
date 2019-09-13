using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_PickedUp_Delivery : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_PickedUp_Delivery(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.PickedUp, BuySellDocumentTypeENUM.Delivery, customerPersonId, sellerPersonId)
        {

        }


        #region Order Lines (OL) This is the orders list


        #endregion


        #region Header  (HD)  This is Header of the order

        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion
    }
}
