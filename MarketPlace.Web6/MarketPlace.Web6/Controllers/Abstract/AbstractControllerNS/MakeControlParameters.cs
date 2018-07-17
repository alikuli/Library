using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UserModels;

namespace MarketPlace.Web4.Controllers
{
    /// <summary>
    /// Note. The Error_Controller and the Error_Uow are the same
    /// After exectuing an action, the AbstractController automaticlly adds the errors.
    /// You must wire up tye Uow in the Controllers so that the Uow goes into Uow and then
    /// you can create one from that which is for that particular controller if you need it.
    /// </summary>
    public abstract partial class AbstractController : Controller
    {


        protected ControllerIndexParams MakeControlParameters(
            string id,
            string searchFor,
            string isandForSearch,
            string selectedId,
            ICommonWithId entity,
            ICommonWithId dudEntity,
            MenuENUM menuEnum = MenuENUM.unknown,
            SortOrderENUM sortBy = SortOrderENUM.Item1_Asc,
            bool print = false,
            string returnUrl = "",
            ActionNameENUM actionNameEnum = ActionNameENUM.Unknown)
        {
            //FactoryParameters fp = new FactoryParameters();

            //load parameters
            string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);

            ApplicationUser user = GetApplicationUser();
            bool isUserAdmin = IsUserAdmin(user);

            //todo note... the company name is missing. We may need it.

            //the MenuController in the dudEntity entity needs to be set.


            ControllerIndexParams parms = new ControllerIndexParams(
                id,
                searchFor,
                isandForSearch,
                selectedId,
                menuEnum,
                sortBy,
                logoAddress,
                entity,
                dudEntity as ICommonWithId,
                user,
                isUserAdmin,
                returnUrl,
                actionNameEnum);

            ViewBag.ReturnUrl = returnUrl;

            return parms;
        }



    }
}