
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Sale : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Sale(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, BuySellDocumentTypeENUM.Sale, customerPersonId, sellerPersonId)
        {

        }

        #region Order Lines (OL) This is the orders list

        public override string OL_IconForEditView { get { return IconView; } }
        public override string OL_ToolTip_Edit { get { return "View"; } }

        public override string OL_Cancel_Button_ToolTip { get { return "Cancel. This will return you to 'Ready for Pickup mode"; } }
        public override string OL_IconForCancel { get { return IconCancel; } }
        public override bool OL_IsCanceledEnabled { get { return true; } }

        public override bool OL_IsShowCancelButton { get { return true; } }

        public override bool HD_Show_ExpectedDeliveryDate { get { return true; } }
//        public override bool HD_Enable_ExpectedDeliveryDate { get { return true; } }


        #endregion


        #region Header  (HD)  This is Header of the order
        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }

        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.Complete; } }


        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Show_RequireInsuranace { get { return true; } }
        //public override bool HD_Enable_RequireInsuranace { get { return true; } }
        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        //public override bool HD_Enable_Delivery_Info { get { return true; } }

        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_ShowOffers { get { return true; } }

        //public override bool HD_Enable_Vehical_Type_Requested { get { return true; } }
        public override bool HD_Show_Vehical_Type_Requested { get { return true; } }

        public override bool HD_Show_Customer_Comment { get { return true; } }
        public override bool HD_Show_OwnersSalesman { get { return true; } }



        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List
        public override string HD_OL_IconForEditView
        {
            get
            {
                return IconView;
            }
        }


        #endregion


        #region Order Detail (OD) This is the detail of the Order Item
        //public override bool OD_ShippedIsVisible
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}


        public override string OD_LabelOrderOrShipped
        {
            get
            {
                return "Will Ship";
            }
        }

        //public override bool OD_OrderedIsEnabled
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}


        #endregion

        #region Header Offers (HD_OFF)
        public override bool HD_OFF_Enable_Accept_Button { get { return true; } }
        public override bool HD_OFF_Show_Accept_By_Cust_And_Seller_Button { get { return true; } }
        //public override bool HD_Show_Make_Offers_Section { get { return true; } }
        public override string HD_OFF_Icon_Cancel_Courier_Button { get { return IconCancel; } }


        #endregion
    }
}
