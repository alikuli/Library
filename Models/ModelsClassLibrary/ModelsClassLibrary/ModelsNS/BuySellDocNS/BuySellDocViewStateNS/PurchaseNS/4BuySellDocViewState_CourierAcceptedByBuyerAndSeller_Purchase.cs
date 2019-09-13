
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Purchase : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Purchase(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, BuySellDocumentTypeENUM.Purchase, customerPersonId, sellerPersonId)
        {

        }


        //public override string CurrentColorClass
        //{
        //    get
        //    {
        //        return ClassFor_ConfirmedByCourier;
        //    }
        //}

        //public override string CurrentColorClassPill
        //{
        //    get
        //    {
        //        return ClassFor_ConfirmedByCourier;
        //    }
        //}

        #region Order Lines (OL) This is the orders list


        #endregion


        #region Header  (HD)  This is Header of the order
        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.Complete; } }
        public override bool HD_ShowOffers { get { return true; } }

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }



        public override bool HD_Show_Customer_Comment { get { return true; } }

        public override bool HD_Show_RequireInsuranace { get { return true; } }
        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        public override bool HD_Show_CustomersSalesman { get { return true; } }


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

        #region Open Offers
        public override string HD_OFF_Icon_Cancel_Courier_Button { get { return IconCancel; } }
        //public override string HD_OFF_Tooltip_Cancel_Courier_Button { get { return "You may cancel the courier using this button"; } }
        public override bool HD_OFF_Show_Courier_Cancel_Button { get { return true; } }
        public override bool HD_OFF_Enable_Courier_Cancel_Button { get { return true; } }

        #endregion



    }
}
