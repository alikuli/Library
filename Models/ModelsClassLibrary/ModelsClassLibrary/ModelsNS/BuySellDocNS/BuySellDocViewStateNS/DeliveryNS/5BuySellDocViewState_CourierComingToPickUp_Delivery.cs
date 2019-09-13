
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_CourierComingToPickUp_Delivery : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_CourierComingToPickUp_Delivery(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.CourierComingToPickUp, BuySellDocumentTypeENUM.Delivery, customerPersonId, sellerPersonId)
        {

        }



        #region Order Lines (OL) This is the orders list
        public override string OL_IconForEditView { get { return IconTruck; } }
        public override string OL_ToolTip_Edit { get { return "To Enter to get your pickup code."; } }

        //public override string OL_DELETE_Button_ToolTip { get { return "To cancel your bid and pickup"; } }

        //public override bool OL_IsDeleteEnabled { get { return true; } }
        //public override string OL_IconForDeleteView { get { return cancelIcon; } }
        public override bool OL_IsCanceledEnabled { get { return true; } }
        public override bool OL_IsShowCancelButton { get { return true; } }

        public override string OL_Cancel_Button_ToolTip { get { return "This will cancel your bid and remove the buyer/seller acceptance."; } }

        #endregion


        #region Header  (HD)  This is Header of the order
        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_AgreedFreightAndDate { get { return true; } }
        public override bool HD_Show_PickupCode_Deliveryman { get { return true; } }


        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.Complete; } }
        //public override bool HD_ShowOffers { get { return true; } }

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }



        public override bool HD_Show_Customer_Comment { get { return true; } }

        public override bool HD_Show_RequireInsuranace { get { return true; } }
        public override bool HD_Show_Freight_Request_Info { get { return true; } }

        public override bool HD_Show_Make_Offers_Section { get { return true; } }
        public override bool HD_Disable_Make_Offers_Section { get { return true; } }
        public override bool HD_Show_DeliverymanSalesman { get { return true; } }
        #endregion



        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion
    }
}
