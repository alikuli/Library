
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_RequestConfirmed_Purchase : BuySellDocViewState_Abstract
    {

        public BuySellDocViewState_RequestConfirmed_Purchase(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.RequestConfirmed, BuySellDocumentTypeENUM.Purchase, customerPersonId, sellerPersonId)
        {

        }


        #region Order Lines (OL) This is the orders list

        public override string OL_IconForEditView { get { return IconView; } }




        //public override string OL_DELETE_Button_ToolTip { get { return "You will go back to Request Unconfirmed. Then, you can change or delete the order."; } }

        //public override string OL_IconForDeleteView { get { return deleteIcon; } }

        //public override DeleteButtonENUM OL_DeleteButtonIs { get { return DeleteButtonENUM.DeleteButton; } }

        //public override bool OL_IsDeleteEnabled { get { return true; } }



        public override bool OL_IsShowCancelButton { get { return true; } }
        public override bool OL_IsCanceledEnabled { get { return true; } }
        //public override bool OL_IsShowReject { get { return true; } }
        //public override bool OL_IsRejectEnabled { get { return true; } }



        #endregion


        #region Header  (HD)  This is Header of the order


        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Show_RequireInsuranace { get { return true; } }
        //public override bool HD_Enable_RequireInsuranace { get { return true; } }
        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        public override bool HD_Enable_Freight_Request_Info { get { return true; } }


        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.OnlyNameCityCountry; } }

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }
        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_Vehical_Type_Requested { get { return true; } }

        public override bool HD_Show_CustomersSalesman { get { return true; } }


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

        public override bool OD_Hide_System_Save_Button { get { return true; } }

        //public override bool OD_OrderedIsEnabled
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        #endregion

    }
}
