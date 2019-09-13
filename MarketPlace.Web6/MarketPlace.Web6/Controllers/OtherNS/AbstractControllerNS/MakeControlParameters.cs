using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;

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
            string menuPathMainId,
            string searchFor,
            string isandForSearch,
            string selectedId,
            ICommonWithId entity,
            ICommonWithId dudEntity,
            BreadCrumbManager breadCrumbManager,
            string userId,
            string userName,
            string productId,
            string returnUrl,
            bool isMenu,
            //string userPersonId,
            //string productChildPersonId,
            string button,
            MenuENUM menuEnum = MenuENUM.IndexDefault,
            SortOrderENUM sortBy = SortOrderENUM.Item1_Asc,
            bool print = false,
            ActionNameENUM actionNameEnum = ActionNameENUM.Unknown,
            BuySellDocumentTypeENUM buySellDocumentTypeEnum = BuySellDocumentTypeENUM.Unknown,
            BuySellDocStateENUM buySellDocStateEnum = BuySellDocStateENUM.Unknown)
        {
            //FactoryParameters fp = new FactoryParameters();

            //load parameters
            string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);



            //todo note... the company name is missing. We may need it.

            //the MenuController in the dudEntity entity needs to be set.

            ControllerIndexParams parms = new ControllerIndexParams(
                id,
                menuPathMainId,
                searchFor,
                isandForSearch,
                selectedId,
                menuEnum,
                sortBy,
                logoAddress,
                entity,
                dudEntity as ICommonWithId,
                userId,
                userName,
                false,
                isMenu,
                breadCrumbManager,
                actionNameEnum,
                null,
                productId,
                returnUrl,
                buySellDocumentTypeEnum,
                buySellDocStateEnum, button);

            //ViewBag.ReturnUrl = returnUrl;

            return parms;
        }



    }
}