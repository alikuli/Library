﻿using EnumLibrary.EnumNS;

namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_Enroute_Sale : BuySellDocViewState_Abstract
    {

        public BuySellDocViewState_Enroute_Sale(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.Enroute, BuySellDocumentTypeENUM.Sale, customerPersonId, sellerPersonId)
        {

        }

        //public override string CurrentColorClass
        //{
        //    get
        //    {
        //        return ClassFor_PickedUp;
        //    }
        //}

        //public override string CurrentColorClassPill
        //{
        //    get
        //    {
        //        return ClassFor_PickedUp;
        //    }
        //}


        #region Order Lines (OL) This is the orders list
        public override bool OL_IsCanceledEnabled { get { return true; } }
        public override bool OL_IsShowCancelButton { get { return true; } }

        public override string OL_Cancel_Button_ToolTip { get { return "This will cancel the deliveryman. The order will be converted into a problem. All cash, guarantees and insurances will be frozen until resolved. "; } }


        #endregion


        #region Header  (HD)  This is Header of the order

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }

        public override bool HD_Show_PickupCode_To_Seller_As_info { get { return true; } }

        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.Complete; } }
        public override bool HD_Show_AgreedFreightAndDate { get { return true; } }
        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }
        public override bool HD_Show_PickupCode_Deliveryman { get { return true; } }
        public override bool HD_Show_OwnersSalesman { get { return true; } }



        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion
    }
}
