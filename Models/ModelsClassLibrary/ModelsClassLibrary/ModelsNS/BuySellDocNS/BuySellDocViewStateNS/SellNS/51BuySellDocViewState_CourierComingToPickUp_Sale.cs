
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_CourierComingToPickUp_Sale : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_CourierComingToPickUp_Sale(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.CourierComingToPickUp, BuySellDocumentTypeENUM.Sale, customerPersonId, sellerPersonId)
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
        public override string OL_IconForEditView { get { return IconTruck; } }
        public override string OL_ToolTip_Edit { get { return "Please get pickup code from deliveryman and enter it here."; } }

        public override bool OL_IsCanceledEnabled { get { return true; } }
        public override bool OL_IsShowCancelButton { get { return true; } }

        public override string OL_Cancel_Button_ToolTip { get { return "This will cancel the deliveryman. You may be charged a cancelation fee."; } }


        #endregion

        #region Header  (HD)  This is Header of the order

        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.Complete; } }
        public override bool HD_Show_AgreedFreightAndDate { get { return true; } }
        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_Enter_PickupCode_Seller { get { return true; } }
        public override bool HD_Show_OwnersSalesman { get { return true; } }

        public override bool HD_Show_ExpectedDeliveryDate { get { return true; } }
//        public override bool HD_Enable_ExpectedDeliveryDate { get { return true; } }


        #endregion



        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion
    }
}
