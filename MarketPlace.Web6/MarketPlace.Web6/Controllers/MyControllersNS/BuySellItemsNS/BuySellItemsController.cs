using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellDocNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.BuySellItemNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.AddressNS;
using UowLibrary.BuySellDocNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.CustomerNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.PlayersNS.PersonNS;
using System.Linq;
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

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {

            if (!User.IsNull()) //must be logged in.
            {

                //is the current user the owner of this product?
                BuySellItem buySellItem = parm.Entity as BuySellItem;
                buySellItem.IsNullThrowException();

                buySellItem.BuySellDocumentTypeEnum = parm.BuySellDocumentTypeEnum;


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

            return base.Event_CreateViewAndSetupSelectList(parm);
        }

    }
}