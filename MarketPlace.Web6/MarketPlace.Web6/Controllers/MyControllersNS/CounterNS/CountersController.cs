using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary.CounterNS;
using ModelsClassLibrary.ModelsNS.SharedNS.CounterNS;
using UowLibrary;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.PlayersNS;
using UowLibrary.MyWorkClassesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ErrorHandlerLibrary;
using UowLibrary.PageViewNS;

namespace MarketPlace.Web6.Controllers
{
    public class CountersController : EntityAbstractController<Counter>
    {

        public CountersController(CounterBiz biz, BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(biz, bcm, err, pageViewBiz) { }


    }
}