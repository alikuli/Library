
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    /// <summary>
    /// After delivery, do not all deletion / change of the order. This is used to get the cash amount of the user. 
    /// The order header becomes a part of the cashTrx
    /// </summary>
    public class BuySellDocViewState_Delivered_Purchase : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_Delivered_Purchase(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.Delivered, BuySellDocumentTypeENUM.Purchase, customerPersonId, sellerPersonId)
        {

        }

        //public override string CurrentColorClass
        //{
        //    get
        //    {
        //        return ClassFor_Delivered;
        //    }
        //}

        //public override string CurrentColorClassPill
        //{
        //    get
        //    {
        //        return ClassFor_Delivered_Pill;
        //    }
        //}

        #region Order Lines (OL) This is the orders list
        public override bool OL_IsCanceledEnabled { get { return true; } }
        public override bool OL_IsShowCancelButton { get { return true; } }

        string message = string.Format("Cancel if you have a problem with the order. This will freeze everyone's money until the problem is resolved. This option is only available during the guarantee period. CUSTOMER IS ALWAYS RIGHT. This option is available until ");
        public override string OL_Cancel_Button_ToolTip { get { return message; } }


        #endregion


        #region Header  (HD)  This is Header of the order
        //public override bool HD_Show_CombinedCode { get { return true; } }
        public override bool HD_Show_CombinedCode_With_As_Entered { get { return true; } }

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

        #endregion
    }
}
