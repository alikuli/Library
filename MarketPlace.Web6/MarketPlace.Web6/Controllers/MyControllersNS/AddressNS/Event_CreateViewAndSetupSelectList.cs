using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DeliveryMethodNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary.PaymentTermNS;
using UowLibrary;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ErrorHandlerLibrary;
using UowLibrary.PageViewNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using UowLibrary.AddressNS;

namespace MarketPlace.Web6.Controllers
{
    public partial class AddressesController
    {

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ViewBag.CountrySelectList = AddressBiz.CountrySelectList;
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            ViewBag.CountrySelectList = AddressBiz.CountrySelectList;
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }
    }
}