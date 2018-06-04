using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.Logs.VisitorsLogNS;
using UowLibrary.VisitorLogNS;
using UowLibrary;

namespace MarketPlace.Web6.Controllers
{
    public class VisitorLogsController : EntityAbstractController<VisitorLog>
    {

        VisitorLogBiz _VisitorLogBiz;
        #region Constructo and initializers

        public VisitorLogsController(VisitorLogBiz VisitorLogBiz, IErrorSet errorSet, UserBiz userbiz)
            : base(VisitorLogBiz, errorSet,  userbiz)
        {
            _VisitorLogBiz = VisitorLogBiz;
        }

        #endregion



    }
}