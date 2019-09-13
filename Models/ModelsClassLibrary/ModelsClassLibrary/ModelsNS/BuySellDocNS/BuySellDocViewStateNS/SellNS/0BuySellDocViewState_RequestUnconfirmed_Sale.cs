
using EnumLibrary.EnumNS;
namespace ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS.BuySellDocViewStateNS
{
    public class BuySellDocViewState_RequestUnconfirmed_Sale : BuySellDocViewState_Abstract
    {
        public BuySellDocViewState_RequestUnconfirmed_Sale(string customerPersonId, string sellerPersonId)
            : base(BuySellDocStateENUM.RequestConfirmed, BuySellDocumentTypeENUM.Sale, customerPersonId, sellerPersonId)
        {

        }
        //public override string CurrentColorClass
        //{
        //    get
        //    {
        //        return ClassFor_RequestUnconfirmed;
        //    }
        //}

        //public override string CurrentColorClassPill
        //{
        //    get
        //    {
        //        return ClassFor_RequestUnconfirmed_Pill;
        //    }
        //}


        //public override string OL_IconForEditView
        //{
        //    get
        //    {
        //        return viewIcon;
        //    }
        //}

        #region Order Lines (OL) This is the orders list


        public override string OL_IconForEditView
        {
            get
            {
                return IconView;
            }
        }


        #endregion


        #region Header  (HD)  This is Header of the order
        public override bool HD_Show_Delivery_Format { get { return true; } }
        public override bool HD_Show_RequireInsuranace { get { return true; } }
        //public override bool HD_Enable_RequireInsuranace { get { return true; } }
        public override bool HD_Show_Freight_Request_Info { get { return true; } }


        public override AddressDetailToShowENUM HD_AddressDetailToShow
        {
            get
            {
                return AddressDetailToShowENUM.OnlyNameCityCountry;
            }
        }

        public override bool HD_Show_Address_Ship_To { get { return true; } }
        public override bool HD_Show_Address_Pick_From { get { return true; } }
        public override bool HD_Hide_Save_Button_In_Edit { get { return true; } }

        #endregion


        #region Header Order Lines (HD_OL) This is the detail of the order i.e. Order Item List

        #endregion


        #region Order Detail (OD) This is the detail of the Order Item

        #endregion
    }
}
