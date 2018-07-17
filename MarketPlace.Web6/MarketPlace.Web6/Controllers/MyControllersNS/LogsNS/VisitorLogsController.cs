using System.Threading.Tasks;
using System.Web.Mvc;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.Logs.VisitorsLogNS;
using UowLibrary.VisitorLogNS;
using UowLibrary;
using BreadCrumbsLibraryNS.Programs;

namespace MarketPlace.Web6.Controllers
{
    public class VisitorLogsController : EntityAbstractController<VisitorLog>
    {

        VisitorLogBiz _VisitorLogBiz;
        #region Constructo and initializers

        public VisitorLogsController(VisitorLogBiz VisitorLogBiz, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(VisitorLogBiz, errorSet, userbiz, breadCrumbManager)
        {
            _VisitorLogBiz = VisitorLogBiz;
        }

        #endregion



    }
}