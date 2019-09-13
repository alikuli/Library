using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_ReadyForPickup_Delivery : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_ReadyForPickup_Delivery(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.ReadyForPickup, BuySellDocumentTypeENUM.Delivery, customerPersonId, sellerPersonId)
        {

        }



        #region Order Lines (OL) This is the orders list

        public override string OL_IconForEditView
        {
            get { return truckIcon; }
        }
        public override string OL_ToolTip_Edit { get { return "To Enter a bid, press this button."; } }


        #endregion


        #region Header  (HD)  This is Header of the order
        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.OnlyNameCityCountry; } }
        public override bool HD_ShowOffers { get { return true; } }

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }



        public override bool HD_Show_Customer_Comment { get { return true; } }
        public override bool HD_Show_RequireInsuranace_MakeOffersScreen { get { return true; } }
        public override bool HD_Show_Make_Offers_Section { get { return true; } }

        //public override bool HD_Enable_Vehical_Type_Requested { get { return true; } }
        //public override bool HD_Show_Vehical_Type { get { return true; } }
        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }




        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        public override bool HD_Enable_Delivery_Info { get { return true; } }
        public override bool HD_Show_Vehical_Type_Requested { get { return true; } }



        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion

        #region Make Offer / Pickup Offer

        public override bool HD_OFF_Enable_Offer_Delete_Button
        {
            get
            {
                return true;
            }
        }

        public override string HD_OFF_Enable_Offer_Delete_Button_Tooltip
        {
            get
            {
                return "You Can delete your bid.";
            }
        }


        #endregion
    }
}
