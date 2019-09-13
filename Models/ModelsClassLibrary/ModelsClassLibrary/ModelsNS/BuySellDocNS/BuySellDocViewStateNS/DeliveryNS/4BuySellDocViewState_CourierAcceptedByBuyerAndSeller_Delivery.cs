
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Delivery : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_CourierAcceptedByBuyerAndSeller_Delivery(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.CourierAcceptedByBuyerAndSeller, BuySellDocumentTypeENUM.Delivery, customerPersonId, sellerPersonId)
        {



        }


        #region Order Lines (OL) This is the orders list

        public override string OL_IconForEditView { get { return IconTruck; } }

        #endregion

        #region HD
        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override AddressDetailToShowENUM HD_AddressDetailToShow { get { return AddressDetailToShowENUM.Complete; } }
        public override bool HD_ShowOffers { get { return true; } }

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }



        public override bool HD_Show_Customer_Comment { get { return true; } }

        public override bool HD_Show_RequireInsuranace { get { return true; } }
        public override bool HD_Show_Freight_Request_Info { get { return true; } }


        #endregion

        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion

        #region Header Offers (HD_OFF)
        //public override bool HD_OFF_Enable_Accept_Button
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}
        public override bool HD_OFF_Show_Accept_By_Courier_Button
        {
            get
            {
                return true;
            }
        }
        #endregion
    }
}
