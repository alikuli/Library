using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_Rejected_Salesman : BuySellDocViewState_Abstract
    {

        public BuySellDocViewState_Rejected_Salesman(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.Rejected, BuySellDocumentTypeENUM.Salesman, customerPersonId, sellerPersonId)
        { }





        #region Order Lines (OL) This is the orders list
        //public override string OL_IconForEditView { get {
        //        return IconEdit;
        //    }
        //}

        //public override bool OL_IsDeleteEnabled { get { return true; } }



        #endregion


        #region Header  (HD)  This is Header of the order
        public override string HD_OL_IconForEditView { get { return viewIcon; } }

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }

        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.OnlyNameCityCountry; } }

        //public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_Vehical_Type_Requested { get { return true; } }
        public override bool HD_Show_Button_DelyButton { get { return true; } }



        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }

        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Show_RequireInsuranace { get { return true; } }



        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        //public override bool HD_Enable_Freight_Request_Info { get { return true; } }


        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List
        //public override bool HD_OL_Button_Is_Delete_Enabled
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}


        #endregion


        #region Order Detail (OD) This is the detail of the Order Item
        //public override bool OD_SalePriceIsEnabled
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        public override bool OD_Hide_System_Save_Button { get { return true; } }


        #endregion

    }
}
