using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public interface IBuySellDocViewState
    {


        #region Classes for state
        string ClassFor_RequestUnconfirmed_Pill { get; }
        string ClassFor_RequestConfirmed_Pill { get; }
        string ClassFor_BeingPreparedForShipmentBySeller_Pill { get; }
        string ClassFor_ReadyForShipment_Pill { get; }
        string ClassFor_CourierAccepted_Pill { get; }
        string ClassFor_ConfirmedByCourier_Pill { get; }
        string ClassFor_PickedUp_Pill { get; }
        string ClassFor_Delivered_Pill { get; }
        string ClassFor_Canceled_Pill { get; }
        string ClassFor_Rejected_Pill { get; }
        string ClassFor_Problem_Pill { get; }
        string ClassFor_OptingOut_Pill { get; }

        string CurrentColorClass { get; }
        string CurrentColorClassPill { get; }

        #endregion

        #region Icons
        string IconDelete { get; }
        string IconEdit { get; }
        string IconView { get; }
        bool IsDeliveryman { get; }


        #endregion


        #region Order Lines (OL) This is the orders list
        string OL_BadgeColor { get; }


        string OL_IconForEditView { get; }
        string OL_ToolTip_Edit { get; }
        bool OL_IsEditEnabled { get; }
        bool OL_IsShowEditButton { get; }




        string OL_IconForDeleteView { get; }
        bool OL_IsDeleteEnabled { get; }
        string OL_DELETE_Button_ToolTip { get; }
        bool OL_IsShowDeleteButton { get; }


        //DeleteButtonENUM OL_DeleteButtonIs { get; }


        string OL_IconForReject { get; }
        bool OL_IsRejectEnabled { get; }
        bool OL_IsShowReject { get; }
        string OL_Reject_Button_ToolTip { get; }



        bool OL_IsCanceledEnabled { get; }
        bool OL_IsShowCancelButton { get; }
        string OL_IconForCancel { get; }
        string OL_Cancel_Button_ToolTip { get; }




        bool OL_HideCompletedCol { get; }
        bool OL_HideOrderedCol { get; }

        #endregion


        #region Header  (HD)  This is Header of the order
        bool HD_ShowOffers { get; }
        bool HD_Enable_DeliveryAcceptBtn(string deliveryPersonId);
        //IconForEditView 
        //string HD_OrderList_Template { get; }
        bool HD_Show_Button_SaveAndCopy_BillTo { get; }
        bool HD_Show_Button_SaveAndCopy_ShipTo { get; }
        string HD_ToolTip_Button_Copy_ShipTo { get; }
        string HD_ToolTip_Button_Save_ShipTo { get; }
        bool HD_ToolTip_Button_SaveAndCopy_BillTo { get; }
        bool HD_Enable_Delivery_Info { get; }

        bool HD_Show_Signature { get; }

        bool HD_Enable_Customer_Comment { get; }
        bool HD_Show_Customer_Comment { get; }

        bool HD_Show_Delivery_Format { get; }

        bool HD_Hide_Save_Button_In_Edit { get; }

        bool HD_Show_AgreedFreightAndDate { get; }
        bool HD_Enable_AgreedFreightAndDate { get; }
        //bool HD_Enable_VehicalType { get; }

        //bool HD_Show_RequestedDate { get; }
        //bool HD_Enable_RequestedDate { get; }
        bool HD_Show_ExpectedDeliveryDate { get; }
        bool HD_Enable_ExpectedDeliveryDate { get; }

        bool HD_Show_DeliverymanSalesman { get; }
        bool HD_Show_OwnersSalesman { get; }
        bool HD_Show_CustomersSalesman { get; }
        bool HD_Show_Salesman { get; }

        bool HD_Show_RequireInsuranace { get; }
        bool HD_Enable_RequireInsuranace { get; }
        bool HD_Show_RequireInsuranace_MakeOffersScreen { get; }

        bool HD_Show_Vehical_Type_Requested { get; }
        bool HD_Enable_Vehical_Type_Requested { get; }

        bool HD_Show_Vehical_Type_Offered { get; }
        bool HD_Enable_Vehical_Type_Offered { get; }

        bool HD_Show_Make_Offers_Section { get; }
        bool HD_Disable_Make_Offers_Section { get; }
        bool HD_Enable_Freight_Request_Info { get; }
        bool HD_Show_Freight_Request_Info { get; }
        bool HD_EnableDeliveryMan { get; }
        bool HD_EnableDeliveryDatesRequested { get; }
        AddressDetailToShowENUM HD_AddressDetailToShow { get; }
        string HD_AddressTemplate_For_Shipping_And_BillTo_Address { get; }
        //bool Is_HD_AddressTemplate_For_Shipping_And_BillTo_Address_DISABLED { get; }
        bool HD_Show_DropDown_BillToAddress { get; }
        bool HD_Show_DropDown_ShipToAddress { get; }
        bool HD_Show_Get_PhoneNumber_Btn { get; }
        //bool HD_Show_ShippingAddress_BillTo { get; }
        //bool HD_Show_ShippingAddress_ShipTo { get; }

        bool HD_Show_Address_Pick_From { get; }
        bool HD_Show_Address_Ship_To { get; }

        bool HD_Show_Complex_Address_Bill_To { get; }
        bool HD_Show_Complex_Address_Ship_To { get; }
        bool HD_Enable_Insurance { get; }


        bool HD_Show_DeliveryCode_Customer { get; }
        bool HD_Show_PickupCode_Deliveryman { get; }

        bool HD_is_Show_Entry_DeliveryCode_To_DeliveryMan { get; }
        bool HD_Show_PickupCode_To_Seller_As_info { get; }


        bool HD_Show_Enter_PickupCode_Seller { get; }

        bool HD_Show_CombinedCode { get; }
        bool HD_Show_CombinedCode_With_As_Entered { get; }
        bool HD_Show_DocumentNo { get; }
        bool HD_Show_DocumentState { get; }
        //bool HD_Show_RequestedDate { get;}
        //bool HD_Enable_RequestedDate { get;}


        bool HD_Show_Button_DelyButton { get; }
        bool HD_Enable_Button_DelyButton { get; }
        string HD_Text_Button_DelyButton { get; }

        /// <summary>
        /// This allows you to not use the system. You do everything directly yourself.
        /// </summary>
        bool HD_Show_Opt_Out_Of_System { get; }
        bool HD_Enable_Opt_Out_Of_System { get; }
        string HD_Text_Opt_Out_Of_System { get; }



        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List
        string HD_OL_IconForEditView { get; }
        string HD_OL_IconForDelete { get; }
        bool HD_OL_Button_Is_Delete_Enabled { get; }

        string HD_OL_EditToolTip { get; }



        #endregion


        #region Order Detail (OD) This is the detail of the Order Item
        bool OD_OrderedIsEnabled { get; }
        //bool OD_ShippedIsEnabled { get; }
        //bool OD_ShippedIsVisible { get; }
        bool OD_SalePriceIsEnabled { get; }
        string OD_LabelOrderOrShipped { get; }

        bool OD_Hide_System_Save_Button { get; }
        bool OD_Show_Button_SaveButton { get; }
        bool OD_Enable_Button_SaveButton { get; }


        #endregion

        #region Header Offers (HD_OFF)
        bool HD_OFF_Enable_Accept_Button { get; }
        bool HD_OFF_Show_Accept_By_Cust_And_Seller_Button { get; }
        bool HD_OFF_Show_Accept_By_Courier_Button { get; }

        bool HD_OFF_Show_Offer_Delete_Button { get; }
        bool HD_OFF_Enable_Offer_Delete_Button { get; }

        string HD_OFF_Enable_Offer_Delete_Button_Tooltip { get; }
        string HD_OFF_Enable_Offer_Delete_Button_Icon { get; }
        string HD_OFF_Icon_Cancel_Courier_Button { get; }
        string HD_OFF_Tooltip_Cancel_Courier_Button { get; }
        bool HD_OFF_Show_Courier_Cancel_Button { get; }
        bool HD_OFF_Enable_Courier_Cancel_Button { get; }

        string HD_OFF_Icon_Deliveryman_Accept_To_Deliver_Product_Button { get; }



        #endregion

        #region Penalties and other misc

        decimal MISC_PenaltyAmount { get; }
        string MISC_PenaltyText { get; }
        #endregion

    }
}
