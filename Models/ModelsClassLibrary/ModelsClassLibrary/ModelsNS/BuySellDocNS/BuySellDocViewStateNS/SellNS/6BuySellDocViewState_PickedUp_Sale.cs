using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_PickedUp_Sale : BuySellDocViewState_Abstract
    {

        public BuySellDocViewState_PickedUp_Sale(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.PickedUp, BuySellDocumentTypeENUM.Sale, customerPersonId, sellerPersonId)
        {

        }

        //public override string CurrentColorClass
        //{
        //    get
        //    {
        //        return ClassFor_PickedUp;
        //    }
        //}

        //public override string CurrentColorClassPill
        //{
        //    get
        //    {
        //        return ClassFor_PickedUp;
        //    }
        //}


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
