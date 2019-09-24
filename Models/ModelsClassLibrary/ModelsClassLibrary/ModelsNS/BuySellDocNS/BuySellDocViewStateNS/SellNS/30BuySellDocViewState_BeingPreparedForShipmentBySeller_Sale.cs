
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_BeingPreparedForShipmentBySeller_Sale : BuySellDocViewState_Abstract
    {

        public BuySellDocViewState_BeingPreparedForShipmentBySeller_Sale(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.BeingPreparedForShipmentBySeller, BuySellDocumentTypeENUM.Sale, customerPersonId, sellerPersonId)
        {

        }




        #region Order Lines (OL) This is the orders list

        public override string OL_IconForEditView { get { return editIcon; } }
        public override string OL_ToolTip_Edit { get { return "You must sign off this document to go to next stage of shipping."; } }

        public override bool OL_IsShowCancelButton { get { return true; } }
        public override bool OL_IsCanceledEnabled { get { return true; } }

        public override string OL_Cancel_Button_ToolTip { get { return "You can cancel this order without penalty."; } }


        public override bool HD_Enable_ExpectedDeliveryDate { get { return true; } }
        


        #endregion


        #region Header  (HD)  This is Header of the order


        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }
        //public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_Vehical_Type_Requested { get { return true; } }



        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_Button_DelyButton { get { return true; } }
        public override bool HD_Enable_Button_DelyButton { get { return true; } }
        public override string HD_Text_Button_DelyButton { get { return "Ready For Pickup"; } }






        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Show_RequireInsuranace { get { return true; } }
        public override bool HD_Enable_RequireInsuranace { get { return true; } }
        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        public override bool HD_Enable_Vehical_Type_Requested { get { return true; } }


        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.Complete; } }
        public override bool HD_Show_Signature { get { return true; } }


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

        public override bool HD_OL_Button_Is_Delete_Enabled
        {
            get
            {
                return false;
            }
        }

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item


        #endregion
    }
}
