
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_BeingPreparedForShipmentBySeller_Salesman : BuySellDocViewState_Abstract
    {

        public BuySellDocViewState_BeingPreparedForShipmentBySeller_Salesman(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.BeingPreparedForShipmentBySeller, BuySellDocumentTypeENUM.Salesman, customerPersonId, sellerPersonId)
        {

        }


        //public override string CurrentColorClass
        //{
        //    get
        //    {
        //        return ClassFor_ReadyForShipment;
        //    }
        //}

        //public override string CurrentColorClassPill
        //{
        //    get
        //    {
        //        return ClassFor_ReadyForShipment_Pill;
        //    }
        //}


        #region Order List
        public override string OL_IconForEditView { get { return IconView; } }


        #endregion

        #region Header Order List

        #endregion

        #region Header

        public override string HD_OL_IconForEditView { get { return viewIcon; } }

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }

        public override AddressDetailToShowENUM HD_AddressDetailToShow
        {
            get
            {
                return AddressDetailToShowENUM.Complete;
            }
        }



        //public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_Vehical_Type_Requested { get { return true; } }




        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Show_RequireInsuranace { get { return true; } }



        public override bool HD_Show_Freight_Request_Info { get { return true; } }
        public override bool HD_Enable_Freight_Request_Info { get { return true; } }


        #endregion


        #region Order Detail
        //public override bool OD_ShippedIsVisible
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        #endregion


        #region Order Lines (OL) This is the orders list


        #endregion


        #region Header  (HD)  This is Header of the order
        public override bool HD_Show_CustomersSalesman { get { return true; } }
        public override bool HD_Show_OwnersSalesman { get { return true; } }
        public override bool HD_Show_DeliverymanSalesman { get { return true; } }

        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion
    }
}
