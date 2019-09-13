
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_RequestConfirmed_Sale : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_RequestConfirmed_Sale(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.RequestConfirmed, BuySellDocumentTypeENUM.Sale, customerPersonId, sellerPersonId)
        {

        }

        //public override string CurrentColorClass
        //{
        //    get
        //    {
        //        return ClassFor_RequestConfirmed;
        //    }
        //}

        //public override string CurrentColorClassPill
        //{
        //    get
        //    {
        //        return ClassFor_RequestConfirmed_Pill;
        //    }
        //}


        //public override bool OD_OrderedIsEnabled
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}



        //public override bool OD_ShippedIsVisible
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //public override bool OD_ShippedIsEnabled
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}


        #region Order Lines (OL) This is the orders list
        public override string OL_IconForEditView{get{ return IconEdit; } }

        public override bool OL_IsDeleteEnabled { get { return true; } }

        public override string OL_IconForDeleteView { get { return cancelIcon; } }

        public override string OL_DELETE_Button_ToolTip { get { return "The order will go back to unconfirmed. You will not be able to ship it anymore until the customer reconfirms the order."; } }
        //public override DeleteButtonENUM OL_DeleteButtonIs { get { return DeleteButtonENUM.CancelButton; } }

        public override bool OL_IsShowReject { get { return true; } }
        public override bool OL_IsRejectEnabled { get { return true; } }

        #endregion


        #region Header  (HD)  This is Header of the order

        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Show_RequireInsuranace { get { return true; } }
        public override bool HD_Enable_RequireInsuranace { get { return true; } }
        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        //public override bool HD_Enable_Freight_Request_Info { get { return true; } }
        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_Button_DelyButton { get { return true; } }
        public override bool HD_Enable_Button_DelyButton { get { return true; } }
        public override string HD_Text_Button_DelyButton { get { return "Confirm Sale"; } }
        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.OnlyNameCityCountry; } }
        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }
        //public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_Vehical_Type_Requested { get { return true; } }


















        public override bool HD_Show_Signature
        {
            get
            {
                return true;
            }
        }
        public override bool HD_Enable_Delivery_Info
        {
            get
            {
                return true;
            }
        }

        public override bool HD_Enable_Vehical_Type_Requested
        {
            get
            {
                return true;
            }
        }

        public override bool HD_Enable_Insurance
        {
            get
            {
                return true;
            }
        }

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
        public override string OD_LabelOrderOrShipped
        {
            get
            {
                return "Will Ship";
            }
        }

        public override bool OD_OrderedIsEnabled
        {
            get
            {
                return true;
            }
        }


        #endregion

        #region Offers (HD_OFF)

        #endregion
    }
}
