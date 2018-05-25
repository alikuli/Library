using System.IO;
using System.Web.Mvc;
namespace MarketPlace.Web6.Controllers
{
    public class TestController : Controller
    {

        public ActionResult Index()
        {
            //C:\Users\ALI\Documents\Visual Studio 2013\Projects\Libraries\MarketPlace.Web6\MarketPlace.Web6\Content\MyImages\Initialization\Chinese Food.jpg
            string sourceDirectory = AliKuli.ConstantsNS.MyConstants.SAVE_INITIALIZATION_DIRECTORY;
            string nameOfSourceFile = "Chinese Food.jpg";
            string targetPath = Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY,"test");

            string sourceDirectoryWebMapped = HttpContext.Server.MapPath(sourceDirectory);
            string targetDirectoryWebMapped = HttpContext.Server.MapPath(targetPath);

            AliKuli.ToolsNS.FileTools.CopyFileAndGiveNewName(sourceDirectoryWebMapped, targetDirectoryWebMapped, nameOfSourceFile);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string msg)
        {
            return View();
        }
    }
}