using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System.Linq;
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

        }


        public override void Event_ModifyIndexItem(IndexListVM indexListVM, IndexItemVM indexItem, ICommonWithId icommonWithId)
        {
            base.Event_ModifyIndexItem(indexListVM, indexItem, icommonWithId);
            ProductChild productChild = icommonWithId as ProductChild;
            productChild.IsNullThrowException("unable to unbox productChild");

            indexItem.MenuManager = new MenuManager(null, null,productChild, MenuENUM.IndexMenuPath1, BreadCrumbManager, null, UserId);
            getPictureList(indexItem, productChild);
            indexItem.Price = productChild.Sell.SellPrice;

        }


    }
}
