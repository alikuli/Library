using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using UowLibrary.CounterNS;
using ModelsClassLibrary.ModelsNS.SharedNS.CounterNS;

namespace MarketPlace.Web6.Controllers
{
    public class CountersController : EntityAbstractController<Counter>
    {

        public CountersController(CounterBiz countersBiz, IErrorSet errorSet)
            : base(countersBiz, errorSet) {}


    }
}