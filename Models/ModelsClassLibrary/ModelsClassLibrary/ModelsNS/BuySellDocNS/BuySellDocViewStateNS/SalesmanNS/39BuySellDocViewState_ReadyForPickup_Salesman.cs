using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_ReadyForPickup_Salesman : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_ReadyForPickup_Salesman(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.ReadyForPickup, BuySellDocumentTypeENUM.Salesman, customerPersonId, sellerPersonId)
        {


        }



        #region Order List
        //Delete Button
        public override string OL_DELETE_Button_ToolTip
        {
            get
            {
                return "You will go back to unconfirmed order and you may be charged because the seller has already packed the product.";
            }
        }

        ////public override DeleteButtonENUM OL_DeleteButtonIs
        //{
        //    get
        //    {
        //        return DeleteButtonENUM.CancelButton;
        //    }
        //}

        public override string OL_IconForDeleteView
        {
            get
            {
                return cancelIcon;
            }
        }

        public override bool OL_IsDeleteEnabled
        {
            get
            {
                return true;
            }
        }



        public override string OL_IconForEditView
        {
            get
            {
                return IconTruck;
            }
        }

        public override string OL_ToolTip_Edit
        {
            get
            {
                return "Select your courier";
            }
        }
        #endregion


        #region Header  (HD)  This is Header of the order


        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.Complete; } }
        public override bool HD_ShowOffers { get { return true; } }

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }

        public override bool HD_Show_Vehical_Type_Requested { get { return true; } }


        public override bool HD_Show_Customer_Comment { get { return true; } }
        public override bool HD_Show_RequireInsuranace { get { return true; } }

        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        public override bool HD_Enable_Freight_Request_Info { get { return true; } }

        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_CustomersSalesman { get { return true; } }
        public override bool HD_Show_OwnersSalesman { get { return true; } }
        public override bool HD_Show_DeliverymanSalesman { get { return true; } }




        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion

        #region Header Offers (HD_OFF)
        public override bool HD_OFF_Enable_Accept_Button
        {
            get
            {
                return true;
            }
        }
        public override bool HD_OFF_Show_Accept_By_Cust_And_Seller_Button
        {
            get
            {
                return true;
            }
        }
        #endregion


    }
}
