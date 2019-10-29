using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Linq;
using System.Web.Mvc;
using UowLibrary.BuySellDocNS;
using UowLibrary.ParametersNS;
using UserModels;

namespace MarketPlace.Web6.Controllers
{
    [Authorize]
    public class BuySellItemsController : EntityAbstractController<BuySellItem>
    {

        BuySellItemBiz _buySellItemBiz;
        //AddressBiz _addressBiz;

        public BuySellItemsController(BuySellItemBiz buySellItemsBiz, AbstractControllerParameters param)
            : base(buySellItemsBiz, param)
        {
            _buySellItemBiz = buySellItemsBiz;
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {

            setupTheBuySellItem(parm);

            BuySellItem buySellItem = parm.Entity as BuySellItem;
            buySellItem.IsNullThrowException();

            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {

            setupTheBuySellItem(parm);

             BuySellItem buySellItem = parm.Entity as BuySellItem;
            buySellItem.IsNullThrowException();
            //transfer the information to the orderStr
            Hide_Save_Button(buySellItem);
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }

        private void setupTheBuySellItem(ControllerIndexParams parm)
        {
            BuySellItem buySellItem = parm.Entity as BuySellItem;
            buySellItem.IsNullThrowException();

            buySellItem.Quantity.OrderStr = string.Format("{0:N2}", buySellItem.Quantity.Order);
            buySellItem.SalePriceStr = string.Format("{0:N2}", buySellItem.SalePrice);
            buySellItem.BuySellDoc.IsNullThrowException();

            //by doing this we get the buySellDoc.ViewState
            buySellItem.BuySellDoc.BuySellDocumentTypeEnum = parm.BuySellDocumentTypeEnum;
            buySellItem.BuySellDoc.BuySellDocViewState.IsNullThrowException();
            
            if (!User.IsNull()) //must be logged in.
            {


                //is the current user the owner of this product?

                buySellItem.BuySellDocumentTypeEnum = parm.BuySellDocumentTypeEnum;
                buySellItem.BuySellDocStateEnum = parm.BuySellDocStateEnum;
                

                //we come here during the get as well and if we are getting a new product then we
                //will not have a product
                if (!buySellItem.ProductChild.IsNull())
                {
                    ApplicationUser productOwnerUser = buySellItem.ProductChild.Owner.Person.Users.FirstOrDefault();
                    productOwnerUser.IsNullThrowException();

                    bool isUserTheOwnerOfTheProduct = UserId == productOwnerUser.Id;
                    buySellItem.IsUserOwned = isUserTheOwnerOfTheProduct;
                }
            }
        }

        public override void Event_Edit_Update_The_entity(BuySellItem dbBuySellItem, BuySellItem inComingBuySellIten)
        {
            dbBuySellItem.BuySellDocStateEnum = inComingBuySellIten.BuySellDocStateEnum;
            dbBuySellItem.BuySellDocumentTypeEnum = inComingBuySellIten.BuySellDocumentTypeEnum;
        }

        private void Hide_Save_Button(BuySellItem buySellItem)
        {
            buySellItem.BuySellDocViewState.IsNullThrowException();
            if (buySellItem.BuySellDocViewState.OD_Hide_System_Save_Button)
            {
                ViewBag.ShowEditControls = false.ToString();

            }
        }

    }
}