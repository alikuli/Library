using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.IndexNS.PlaceLocationNS;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers
{
    public partial class MenusController : EntityAbstractController<MenuPathMain>
    {



        public override async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, MainLocationSelectorClass MainLocationSelectorClass, MenuENUM menuEnum = MenuENUM.IndexMenuPath1, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, bool isMenu = true, string menuPathMainId = "", string productId ="", string viewName = "Index", FormCollection fc = null)
        {
            //returnUrl = Request.Url.PathAndQuery;
            return await base.Index(id, searchFor, isandForSearch, selectedId, returnUrl, MainLocationSelectorClass, menuEnum, sortBy, print, true, menuPathMainId, productId, "",fc);
        }





    }





}