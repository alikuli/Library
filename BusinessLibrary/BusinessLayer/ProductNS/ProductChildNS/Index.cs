using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using UowLibrary.MenuNS.MenuStateNS;

namespace UowLibrary.ProductChildNS
{
    public partial class ProductChildBiz
    {

        public override void Event_ModifyIndexList(IndexListVM indexListVM, ControllerIndexParams parameters)
        {
            base.Event_ModifyIndexList(indexListVM, parameters);
            indexListVM.Show.EditDeleteAndCreate = true;
            indexListVM.IsImageTiled = true;
            indexListVM.MenuManager.ReturnUrl = parameters.ReturnUrl;

            indexListVM.Heading.Main = "Seller Product";
            if (IsShowHidden)
                indexListVM.Heading.Main = "Seller Product (Hidden)";


        }


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            ProductChild productChild = ProductChild.Unbox(icommonWithId);
            indexItem.IsHidden = productChild.Hide;
            indexItem.IsTokenPaymentAccepted = productChild.IsNonRefundablePaymentAccepted;
            indexItem.MenuManager = new MenuManager(null, null, productChild, MenuENUM.IndexMenuPath1, BreadCrumbManager, null, UserId, indexListVM.MenuManager.ReturnUrl, UserName);
            indexItem.MenuManager.PictureAddresses = GetCurrItemsPictureList(productChild);
            indexItem.Price = productChild.Sell.SellPrice;

        }


    }
}
