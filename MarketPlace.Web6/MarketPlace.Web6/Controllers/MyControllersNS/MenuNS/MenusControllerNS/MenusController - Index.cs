using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers
{
    public partial class MenusController : EntityAbstractController<MenuPathMain>
    {



        public override async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, MenuENUM menuEnum = MenuENUM.IndexMenuPath1, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, bool isMenu = true)
        {

            return await base.Index(id, searchFor, isandForSearch, selectedId, returnUrl, menuEnum, sortBy, print, true);
        }





    }





}