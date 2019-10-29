
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_RequestUnconfirmed_Purchase : BuySellDocViewState_Abstract
    {

        public BuySellDocViewState_RequestUnconfirmed_Purchase(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.RequestUnconfirmed, BuySellDocumentTypeENUM.Purchase, customerPersonId, sellerPersonId)
        {

        }

        #region Order Lines (OL) This is the orders list

        public override string OL_IconForEditView
        {
            get
            {
                return IconEdit;
            }
        }

        //public override bool OL_IsDeleteEnabled { get { return true; } }


        //public override bool OL_IsShowDeleteButton { get { return true; } }


        #endregion



        #region Header  (HD)  This is Header of the order




        public override bool HD_Show_Button_DelyButton { get { return true; } }
        public override bool HD_Enable_Button_DelyButton { get { return true; } }
        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override string HD_Text_Button_DelyButton { get { return "Place Order"; } }


        public override string HD_AddressTemplate_For_Shipping_And_BillTo_Address { get { return ConstantsLibrary.BuySellConstants.COMPLEX_ADDRESS_ENABLED; } }
        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        public override bool HD_Enable_Freight_Request_Info { get { return true; } }
        public override bool HD_Enable_Customer_Comment { get { return true; } }
        public override bool HD_Show_Customer_Comment { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }
        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.OnlyNameCityCountry; } }
        public override bool HD_Show_RequireInsuranace { get { return true; } }
        public override bool HD_Show_Button_SaveAndCopy_BillTo { get { return true; } }
        public override bool HD_Show_Button_SaveAndCopy_ShipTo { get { return true; } }
        public override bool HD_Show_Complex_Address_Bill_To { get { return true; } }
        public override bool HD_Show_Complex_Address_Ship_To { get { return true; } }
        //public override bool HD_Show_Signature { get { return true; } }
        public override bool HD_Show_Vehical_Type_Requested { get { return true; } }
        public override bool HD_Enable_Vehical_Type_Requested { get { return true; } }

        public override bool HD_Enable_Opt_Out_Of_System { get { return true; } }




        #endregion
        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        public override string HD_OL_IconForEditView
        {
            get
            {
                return IconEdit;
            }
        }

        public override bool HD_OL_Button_Is_Delete_Enabled
        {
            get
            {
                return true;
            }
        }

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item
        public override bool OD_SalePriceIsEnabled { get { return true; } }
        public override bool OD_Enable_Button_SaveButton { get { return true; } }
        public override bool OD_Hide_System_Save_Button { get { return true; } }
        public override bool OD_OrderedIsEnabled { get { return true; } }

        #endregion
    }
}
