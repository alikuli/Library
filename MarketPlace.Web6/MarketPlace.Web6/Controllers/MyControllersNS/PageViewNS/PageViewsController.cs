using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Web.Mvc;
using UowLibrary.PageViewNS;
using System.Linq;
namespace MarketPlace.Web6.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PageViewsController : EntityAbstractController<PageView>
    {

        PageViewBiz _PageViewsBiz;
        #region Constructo and initializers

        public PageViewsController(PageViewBiz biz, BreadCrumbManager bcm, IErrorSet err)
            : base(biz, bcm, err, biz)
        {
            _PageViewsBiz = biz;
        }


        public ActionResult GetCount()
        {
            CountModel cm = new CountModel();
            cm.Count = _PageViewsBiz.FindAll().Count();
            return View("_numberOfCounts", cm);
        }
        #endregion


    }
}