using EnumLibrary.EnumNS;
using MarketPlace.Web4.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers
{
    public partial class ProductAutomobileVMsController : AbstractController
    {

   

        public virtual async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, string productId, string menuPathMainId, string productChildId, MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        {

            return RedirectToAction("Index", "Products", new { id = id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = selectedId, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuLevelEnum, sortBy = sortBy, print = print });

        }



    }
}
