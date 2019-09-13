using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_Enroute_Salesman : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_Enroute_Salesman(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.Enroute, BuySellDocumentTypeENUM.Salesman, customerPersonId, sellerPersonId)
        {

        }




        #region Order Lines (OL) This is the orders list
        public override string OL_IconForEditView { get { return IconTruck; } }
        public override string OL_ToolTip_Edit { get { return "Get your delivery code from here"; } }



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
        public override bool HD_Show_OwnersSalesman { get { return true; } }
        //public override bool HD_Show_DeliverymansSalesman { get { return true; } }
        public override bool HD_Show_DeliverymanSalesman { get { return true; } }



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
