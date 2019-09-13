
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{





    /// <summary>
    /// this class sets the state of the buyselldoc
    ///     HD = Header
    ///     HD_OL = Header - Order Line Item
    ///     OL = Order Line Item
    /// </summary>
    public abstract class BuySellDocViewState_Abstract : IBuySellDocViewState
    {

        /// <summary>
        /// Global - You will need toset the iconEdit, iconView and iconDelete before use.
        /// </summary>
        /// 

        public BuySellDocViewState_Abstract(BuySellDocStateENUM buySellDocStateEnum, BuySellDocumentTypeENUM buySellDocumentTypeEnum, string customerPersonId, string sellerPersonId)
        {
            BuySellDocStateEnum = buySellDocStateEnum;
            BuySellDocumentTypeEnum = buySellDocumentTypeEnum;
            CustomerPersonId = customerPersonId;
            SellerPersonId = sellerPersonId;
        }


        public BuySellDocStateENUM BuySellDocStateEnum { get; set; }
        public BuySellDocumentTypeENUM BuySellDocumentTypeEnum { get; set; }
        public string CustomerPersonId { get; set; }
        public string SellerPersonId { get; set; }
        /// <summary>
        /// If this is true, then we are dealing with a deliveryman
        /// </summary>
        public virtual bool IsDeliveryman { get { return false; } }

        #region Classes for various stages
        /// <summary>
        /// These are the various templates or AddressComplex
        /// </summary>
        /// 


        public string ClassFor_RequestUnconfirmed { get { return " badge-requestunconfirmed "; } }
        public string ClassFor_RequestConfirmed { get { return " badge-requestconfirmed"; } }
        public string ClassFor_BeingPreparedForShipmentBySeller { get { return " badge-confirmedbyseller "; } }
        public string ClassFor_ReadyForShipment { get { return " badge-readyforshipment "; } }
        public string ClassFor_CourierAccepted { get { return " badge-confirmedbycourier "; } }
        public string ClassFor_ConfirmedByCourier { get { return " badge-confirmedbycourier "; } }
        public string ClassFor_PickedUp { get { return " badge-pickedup "; } }
        public string ClassFor_Enroute { get { return " badge-pickedup "; } }
        public string ClassFor_Delivered { get { return " badge-delivered "; } }
        public string ClassFor_Rejected { get { return " badge-rejected "; } }
        public string ClassFor_Canceled_Pill { get { return " badge-canceled "; } }
        public string ClassFor_Problem { get { return " badge-problem "; } }





        public string ClassFor_RequestUnconfirmed_Pill { get { return " badge badge-pill badge-requestunconfirmed "; } }
        public string ClassFor_RequestConfirmed_Pill { get { return " badge  badge-pill badge-requestconfirmed"; } }
        public string ClassFor_BeingPreparedForShipmentBySeller_Pill { get { return " badge badge-pill badge-confirmedbyseller "; } }
        public string ClassFor_ReadyForShipment_Pill { get { return " badge badge-pill badge-readyforshipment "; } }
        public string ClassFor_CourierAccepted_Pill { get { return " badge badge-pill badge-confirmedbycourier "; } }
        public string ClassFor_ConfirmedByCourier_Pill { get { return " badge badge-pill badge-confirmedbycourier "; } }
        public string ClassFor_PickedUp_Pill { get { return " badge badge-pill badge-pickedup "; } }
        public string ClassFor_Enroute_Pill { get { return " badge badge-pill badge-pickedup "; } }
        public string ClassFor_Delivered_Pill { get { return " badge badge-pill badge-delivered "; } }
        public string ClassFor_Rejected_Pill { get { return " badge badge-pill badge-rejected "; } }
        public string ClassFor_Problem_Pill { get { return " badge badge-pill badge-problem "; } }

        public string ClassForState { get { return ClassFor_RequestUnconfirmed_Pill; } }

        //===================== Header Fields =========================================
        //HD = Header

        public virtual string CurrentColorClass
        {
            get
            {
                switch (BuySellDocStateEnum)
                {
                    case BuySellDocStateENUM.RequestUnconfirmed:
                        return ClassFor_RequestUnconfirmed;
                    case BuySellDocStateENUM.RequestConfirmed:
                        return ClassFor_RequestConfirmed;
                    case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        return ClassFor_BeingPreparedForShipmentBySeller;
                    case BuySellDocStateENUM.ReadyForPickup:
                        return ClassFor_ReadyForShipment;
                    case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        return ClassFor_CourierAccepted;
                    case BuySellDocStateENUM.CourierComingToPickUp:
                        return ClassFor_ConfirmedByCourier;
                    case BuySellDocStateENUM.PickedUp:
                        return ClassFor_PickedUp;
                    case BuySellDocStateENUM.Enroute:
                        return ClassFor_Enroute;
                    case BuySellDocStateENUM.Delivered:
                        return ClassFor_Delivered;
                    case BuySellDocStateENUM.Rejected:
                        return ClassFor_Rejected;
                    case BuySellDocStateENUM.Unknown:
                    case BuySellDocStateENUM.Problem:
                    default:
                        return ClassFor_Problem;
                }

            }
        }
        public virtual string CurrentColorClassPill
        {
            get
            {
                switch (BuySellDocStateEnum)
                {
                    case BuySellDocStateENUM.RequestUnconfirmed:
                        return ClassFor_RequestUnconfirmed_Pill;
                    case BuySellDocStateENUM.RequestConfirmed:
                        return ClassFor_RequestConfirmed_Pill;
                    case BuySellDocStateENUM.BeingPreparedForShipmentBySeller:
                        return ClassFor_BeingPreparedForShipmentBySeller_Pill;
                    case BuySellDocStateENUM.ReadyForPickup:
                        return ClassFor_ReadyForShipment_Pill;
                    case BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller:
                        return ClassFor_CourierAccepted_Pill;
                    case BuySellDocStateENUM.CourierComingToPickUp:
                        return ClassFor_ConfirmedByCourier_Pill;
                    case BuySellDocStateENUM.PickedUp:
                        return ClassFor_PickedUp_Pill;

                    case BuySellDocStateENUM.Enroute:
                        return ClassFor_Enroute_Pill;

                    case BuySellDocStateENUM.Delivered:
                        return ClassFor_Delivered_Pill;
                    case BuySellDocStateENUM.Rejected:
                        return ClassFor_Rejected_Pill;
                    case BuySellDocStateENUM.Unknown:
                    case BuySellDocStateENUM.Problem:
                    default:
                        return ClassFor_Problem_Pill;
                }

            }
        }

        #endregion



        #region Icons

        public string IconEdit { get { return editIcon; } }
        public string IconView { get { return viewIcon; } }
        public string IconDelete { get { return deleteIcon; } }
        public string IconTruck { get { return truckIcon; } }
        public string IconCancel { get { return cancelIcon; } }
        public string IconReject { get { return reject2Icon; } }

        /// <summary>
        /// These are the icons being used.
        /// </summary>
        public string editIcon = @"/ContentMine/Icons/Edit.png";
        public string viewIcon = @"/ContentMine/Icons/View.png";
        public string deleteIcon = @"/ContentMine/Icons/GarbageCan.png";
        public string cancelIcon = @"/ContentMine/Icons/Cancel.png";
        public string truckIcon = @"/ContentMine/Icons/Truck.png";
        public string rejectIcon = @"/ContentMine/Icons/Reject.png";
        public string reject2Icon = @"/ContentMine/Icons/Reject2.png";

        #endregion


        #region Tooltips
        protected string tooltip_Delete_Button_For_Delete = "Select this to delete the order.";
        protected string tooltip_Delete_Button_For_Reject = "This item is rejected. Order will stop.";

        protected string HD_ToolTip_Button_Save_ShipTo_Text = "This will save the address in your list of saved addresses.";
        protected string HD_ToolTip_Button_Save_BillTo_Text = "This will save the address in your list of saved addresses.";

        protected string HD_ToolTip_Button_Copy_ShipTo_Text = "This will copy the address into bill to.";
        protected string HD_ToolTip_Button_Copy_BillTo_Text = "This will copy the address into ship to.";


        #endregion






        //=========================== Delivery Fields ========================================




        #region Order Lines (OL) This is the orders list
        //===================== Order Detail Fields =========================================
        //OD = Order Detail

        public string OL_ToolTip_Edit_Text = "Select this to edit the order.";
        public virtual string OL_ToolTip_Edit { get { return OL_ToolTip_Edit_Text; } }

        public virtual bool OL_IsEditEnabled { get { return true; } }
        public virtual bool OL_IsShowEditButton { get { return true; } }




        public virtual string OL_IconForEditView { get { return IconView; } }



        public virtual string OL_IconForDeleteView { get { return IconDelete; } }
        public virtual string OL_DELETE_Button_ToolTip { get { return tooltip_Delete_Button_For_Delete; } }
        public virtual bool OL_IsShowDeleteButton { get { return false; } }

        /// <summary>
        /// This is the delete button in the order line. This will delete the entire order.
        /// </summary>
        public virtual bool OL_IsDeleteEnabled { get { return false; } }
        //public virtual DeleteButtonENUM OL_DeleteButtonIs { get { return DeleteButtonENUM.DeleteButton; } }


        public virtual string OL_BadgeColor { get { return ""; } }

        public virtual bool OL_HideCompletedCol { get { return true; } }
        public virtual bool OL_HideOrderedCol { get { return false; } }


        /// <summary>
        /// This is mainly for list orders
        /// </summary>
        public virtual string OL_IconForReject { get { return IconReject; } }
        public virtual bool OL_IsRejectEnabled { get { return false; } }
        public virtual bool OL_IsShowReject { get { return false; } }
        public virtual string OL_Reject_Button_ToolTip { get { return "REJECT. Note, once rejected, the customer cannot redo the order. If you want to allow the customer to amend the order, then DO NOT REJECT, but CANCEL the order"; } }


        public virtual bool OL_IsCanceledEnabled { get { return false; } }
        public virtual bool OL_IsShowCancelButton { get { return false; } }
        public virtual string OL_IconForCancel { get { return cancelIcon; } }

        public virtual string OL_Cancel_Button_ToolTip { get { return "CANCEL. This will return the order back to UNCONFIRMED state. You will be able to edit it once again."; } }



        #endregion


        #region Header  (HD)  This is Header of the order

        //public const string HD_TemplateAllAddressesEnabled = "AddressComplex" ;
        //public const string HD_TemplateAllAddressesDisabled = "AddressComplexDisabled";
        //public const string HD_TemplateCityCountryAddressesDisabled = "AddressComplexDisabled_City_Country_Only";

        //public virtual string HD_OrderList_Template { get { return HD_OL_TemplateCityCountryAddressesDisabled; } }

        public virtual bool HD_Show_DeliverymanSalesman { get { return false; } }
        public virtual bool HD_Show_OwnersSalesman { get { return false; } }
        public virtual bool HD_Show_CustomersSalesman { get { return false; } }
        public virtual bool HD_Show_Salesman { get { return true; } }


        public virtual bool HD_Show_DropDown_ShipToAddress { get { return false; } }

        //public virtual bool HD_Show_ShippingAddress_ShipTo { get { return false; } }
        //public virtual bool HD_Show_ShippingAddress_BillTo { get { return false; } }
        public virtual bool HD_Show_DropDown_BillToAddress { get { return false; } }


        public virtual bool HD_Show_Complex_Address_Bill_To { get { return false; } }
        public virtual bool HD_Show_Complex_Address_Ship_To { get { return false; } }
        public virtual string HD_AddressTemplate_For_Shipping_And_BillTo_Address { get { return ConstantsLibrary.BuySellConstants.COMPLEX_ADDRESS_DISABLED; } }

        //public bool Is_HD_AddressTemplate_For_Shipping_And_BillTo_Address_DISABLED
        //{
        //    get
        //    {
        //        switch (HD_AddressTemplate_For_Shipping_And_BillTo_Address)
        //        {
        //            case HD_TemplateAllAddressesEnabled:
        //                return false;
        //            case HD_TemplateAllAddressesDisabled:
        //            case HD_TemplateCityCountryAddressesDisabled:
        //            default:
        //                return true;
        //        }
        //    }
        //}


        public virtual bool HD_Enable_Freight_Request_Info { get { return false; } }
        public virtual bool HD_Show_Freight_Request_Info { get { return false; } }


        public virtual bool HD_Show_Button_SaveAndCopy_ShipTo { get { return false; } }
        public virtual bool HD_Show_Button_SaveAndCopy_BillTo { get { return false; } }

        public virtual string HD_ToolTip_Button_Save_ShipTo { get { return ""; } }
        public virtual string HD_ToolTip_Button_Copy_ShipTo { get { return ""; } }
        public virtual bool HD_ToolTip_Button_SaveAndCopy_BillTo { get { return false; } }

        public virtual bool HD_Show_Get_PhoneNumber_Btn { get { return false; } }



        public virtual bool HD_Show_Signature { get { return false; } }
        public virtual bool HD_Enable_Delivery_Info { get { return false; } }

        public virtual bool HD_Enable_Customer_Comment { get { return false; } }
        public virtual bool HD_Show_Customer_Comment { get { return false; } }

        //to makethis work, give a value to ViewBag.ShowEditControls 
        public virtual bool HD_Hide_Save_Button_In_Edit { get { return false; } }
        public virtual bool HD_ShowOffers { get { return false; } }
        //public virtual bool HD_Enable_VehicalType { get { return false; } }

        public virtual bool HD_EnableDeliveryMan { get { return false; } }
        public virtual bool HD_EnableDeliveryDatesRequested { get { return false; } }



        /// <summary>
        /// This changes the format to the delivery format.
        /// </summary>
        public virtual bool HD_Show_Delivery_Format { get { return false; } }


        /// <summary>
        /// This enables the delivery accept buttons in the current shipment section
        /// </summary>
        /// <param name="deliveryPersonId"></param>
        /// <returns></returns>
        public virtual bool HD_Enable_DeliveryAcceptBtn(string deliveryPersonId) { return false; }

        public virtual bool HD_Show_AgreedFreightAndDate { get { return false; } }

        /// <summary>
        /// This enables the agreedFreight and Date fields
        /// </summary>
        public virtual bool HD_Enable_AgreedFreightAndDate { get { return false; } }

        /// <summary>
        /// If this is true, it will show the make offers section
        /// </summary>
        public virtual bool HD_Show_Make_Offers_Section { get { return false; } }
        public virtual bool HD_Disable_Make_Offers_Section { get { return false; } }

        public virtual AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.OnlyAddressNoPhoneOrEmail; } }
        public virtual bool HD_Show_Address_Pick_From { get { return false; } }
        public virtual bool HD_Show_Address_Ship_To { get { return false; } }

        public virtual bool HD_Enable_Insurance { get { return false; } }



        public virtual bool HD_Show_DeliveryCode_Customer { get { return false; } }
        public virtual bool HD_Show_PickupCode_Deliveryman { get { return false; } }
        public virtual bool HD_is_Show_Entry_DeliveryCode_To_DeliveryMan { get { return false; } }
        public virtual bool HD_Show_Enter_PickupCode_Seller { get { return false; } }
        public virtual bool HD_Show_CombinedCode { get { return false; } }
        public virtual bool HD_Show_CombinedCode_With_As_Entered { get { return false; } }
        public virtual bool HD_Show_PickupCode_To_Seller_As_info { get { return false; } }


        public virtual bool HD_Show_RequireInsuranace { get { return false; } }
        public virtual bool HD_Enable_RequireInsuranace { get { return false; } }
        public virtual bool HD_Show_RequireInsuranace_MakeOffersScreen { get { return false; } }


        public virtual bool HD_Show_Vehical_Type_Requested { get { return false; } }
        public virtual bool HD_Enable_Vehical_Type_Requested { get { return false; } }



        public virtual bool HD_Show_Vehical_Type_Offered { get { return false; } }
        public virtual bool HD_Enable_Vehical_Type_Offered { get { return false; } }




        //public virtual bool HD_Show_RequestedDate { get { return false; } }
        //public virtual bool HD_Enable_RequestedDate { get { return false; } }

        public virtual bool HD_Show_DocumentNo { get { return false; } }
        public virtual bool HD_Show_DocumentState { get { return false; } }

        public virtual bool HD_Show_Button_DelyButton { get { return false; } }
        public virtual bool HD_Enable_Button_DelyButton { get { return false; } }
        public virtual string HD_Text_Button_DelyButton { get { return "Yes"; } }



        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        protected string HD_OL_ToolTip_Delete_Text = "";
        //        protected string HD_OL_ToolTip_EditView_Text = "";
        protected string HD_OL_EditToolTip_Text = "View the item";
        public virtual string HD_OL_EditToolTip { get { return HD_OL_EditToolTip_Text; } }

        //===================== Header Order List Fields =========================================
        //HD = Header
        //OL = Order Line Item

        /// <summary>
        /// This controls the edit icon in the header order line
        /// </summary>
        public virtual string HD_OL_IconForEditView { get { return IconView; } }

        /// <summary>
        /// This controls the delete icon in the header order line
        /// </summary>
        public virtual string HD_OL_IconForDelete { get { return IconDelete; } }

        /// <summary>
        /// This enables the delete button for the Line order in the header.
        /// </summary>
        public virtual bool HD_OL_Button_Is_Delete_Enabled { get { return false; } }



        #endregion


        #region Order Detail (OD) This is the detail of the Order Item
        public virtual string OD_LabelOrderOrShipped { get { return "Ordered"; } }
        /// <summary>
        /// This enables the entire order.
        /// </summary>
        public virtual bool OD_OrderedIsEnabled { get { return false; } }
        public virtual bool OD_SalePriceIsEnabled { get { return false; } }
        public virtual bool OD_Hide_Save_Button { get { return false; } }
        //public virtual bool OD_ShippedIsEnabled { get { return false; } }
        //public virtual bool OD_ShippedIsVisible { get { return true; } }
        public virtual bool OD_Show_Button_DelyButton { get { return true; } }
        public virtual bool OD_Enable_Button_DelyButton { get { return false; } }


        #endregion


        #region Header Offers (HD_OFF)
        public virtual bool HD_OFF_Enable_Accept_Button { get { return false; } }
        public virtual bool HD_OFF_Show_Accept_By_Cust_And_Seller_Button { get { return false; } }
        public virtual bool HD_OFF_Show_Accept_By_Courier_Button { get { return false; } }
        public virtual bool HD_OFF_Show_Offer_Delete_Button { get { return false; } }
        public virtual bool HD_OFF_Enable_Offer_Delete_Button { get { return false; } }
        public virtual string HD_OFF_Enable_Offer_Delete_Button_Tooltip { get { return "You can permanantly delete your offer from here."; } }
        public virtual string HD_OFF_Enable_Offer_Delete_Button_Icon { get { return IconDelete; } }


        public virtual string HD_OFF_Icon_Cancel_Courier_Button { get { return IconDelete; } }
        public virtual string HD_OFF_Tooltip_Cancel_Courier_Button { get { return "You may cancel the courier using this button"; } }
        public virtual bool HD_OFF_Show_Courier_Cancel_Button { get { return false; } }
        public virtual bool HD_OFF_Enable_Courier_Cancel_Button { get { return false; } }
        public virtual string HD_OFF_Icon_Deliveryman_Accept_To_Deliver_Product_Button { get { return IconTruck; } }


        #endregion


        #region MyRegion
        public virtual decimal MISC_PenaltyAmount { get { return 0; } }
        public virtual string MISC_PenaltyText { get { return "Not Set"; } }

        //public decimal GetPenaltyPercentageForPurchaserQuitting()
        //{
        //    string pctAmountString = ConfigurationManager.AppSettings["Ourchase.PenaltyForQuitting.Percent"];
        //    decimal pctAmount = pctAmountString.ToDecimal();

        //    return pctAmount;
        //}


        #endregion




    }
}
