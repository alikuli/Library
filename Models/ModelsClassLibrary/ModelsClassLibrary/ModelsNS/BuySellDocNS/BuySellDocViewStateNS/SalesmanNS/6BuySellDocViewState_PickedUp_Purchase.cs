using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_PickedUp_Purchase : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_PickedUp_Purchase(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.PickedUp, BuySellDocumentTypeENUM.Purchase, customerPersonId, sellerPersonId)
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
        //        return ClassFor_PickedUp_Pill;
        //    }
        //}

        //public override bool OD_ShippedIsVisible
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}
        ////public override bool HD_ShowOffers { get { return true; } }




        #region Order Lines (OL) This is the orders list
        public override bool HD_Show_Delivery_Format
        {
            get
            {
                return true;
            }
        }


        #endregion


        #region Header  (HD)  This is Header of the order

        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item
        //public override bool OD_ShippedIsVisible
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        #endregion
    }
}
