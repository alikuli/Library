using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.MenuNS.MenuStateNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.DeliverymanNS;


namespace MarketPlace.Web6.Controllers
{
    [Authorize]

    public class DeliverymansController : EntityAbstractController<Deliveryman>
    {

        DeliverymanBiz _deliverymanBiz;

        public DeliverymansController(DeliverymanBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _deliverymanBiz = biz;
        }

        DeliverymanBiz DeliverymanBiz
        {
            get
            {
                return _deliverymanBiz;
            }
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            setup(parm);
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }


        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            setup(parm);
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }


        private void setup(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You must be logged in");

            Deliveryman deliveryman = parm.Entity as Deliveryman;
            deliveryman.IsNullThrowException("Unable to unbox deliveryman");

            deliveryman.SelectListPeople = _deliverymanBiz.PersonBiz.SelectList();
            deliveryman.SelectListDeliverymanCategory = _deliverymanBiz.SelectListDeliverymanCategory;
            deliveryman.SelectListAddressBillTo = _deliverymanBiz.AddressBiz.SelectListBillAddressCurrentUser();
        }

        public ActionResult DeliverymanInfo(string id, string returnUrl)
        {
            try
            {
                id.IsNullOrWhiteSpaceThrowArgumentException();
                Deliveryman delyman = DeliverymanBiz.Find(id);
                delyman.IsNullThrowException();

                if(delyman.MenuManager.IsNull())
                {
                    delyman.MenuManager = new MenuManager(null, null, null, MenuENUM.IndexDefault, BreadCrumbManager, null, UserId, returnUrl, UserName);
                }
                delyman.MenuManager.IsNullThrowException();
                delyman.MenuManager.ReturnUrl = returnUrl;
                return View(delyman);
            }
            catch (System.Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
                return Redirect(returnUrl);
            }
        }
        public ActionResult GetPickup()
        {
            return View();
        }

    }
}