using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_Enroute_Purchase : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_Enroute_Purchase(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.Enroute, BuySellDocumentTypeENUM.Purchase, customerPersonId, sellerPersonId)
        {

        }




        #region Order Lines (OL) This is the orders list
        public override string OL_IconForEditView { get { return IconTruck; } }
        public override string OL_ToolTip_Edit { get { return "Get your delivery code from here"; } }

        public override bool OL_IsCanceledEnabled { get { return true; } }
        public override bool OL_IsShowCancelButton { get { return true; } }

        public override string OL_Cancel_Button_ToolTip { get { return "This will cancel the deliveryman. The order will be converted into a problem. All cash, guarantees and insurances will be frozen until resolved. "; } }



        #endregion


        #region Header  (HD)  This is Header of the order
        //public override bool HD_Show_CombinedCode_With_As_Entered { get { return true; } }
        public override bool HD_Show_CombinedCode { get { return true; } }

        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }

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
    }
}
