
using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_BeingPreparedForShipmentBySeller_Delivery : BuySellDocViewState_Abstract
    {

        public BuySellDocViewState_BeingPreparedForShipmentBySeller_Delivery(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.BeingPreparedForShipmentBySeller, BuySellDocumentTypeENUM.Delivery, customerPersonId, sellerPersonId)
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



        //public override bool Is_Show_Owner_Shipping_Address_Instead_Of_Customer_BillTo_Address
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}




        #region Order Lines (OL) This is the orders list


        public override bool OL_IsDeleteEnabled
        {
            get
            {
                return false;
            }
        }
        public override string OL_IconForEditView
        {
            get
            {
                return IconView;
            }
        }
        //public override bool OD_ShippedIsVisible
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        #endregion


        #region Header  (HD)  This is Header of the order
        public override bool HD_Show_Delivery_Format
        {
            get
            {
                return true;
            }
        }

        public override bool HD_OL_Button_Is_Delete_Enabled
        {
            get
            {
                return false;
            }
        }
        public override bool HD_Hide_Save_Button_In_Edit
        {
            get
            {
                return true;
            }
        }
        //public override bool HD_Enable_DeliveryAcceptBtn(string deliveryPersonId)
        //{
        //    return HD_Enable_DeliveryAcceptBtn_Logic(deliveryPersonId);
        //}
        public override bool HD_ShowOffers { get { return true; } }

        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List
        public override string HD_OL_IconForEditView
        {
            get
            {
                return viewIcon;
            }
        }

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion
    }

}
