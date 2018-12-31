using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.Logs.VisitorsLogNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.VisitorLogNS
{
    public partial class VisitorLogBiz : BusinessLayer<VisitorLog>
    {
        public VisitorLogBiz(IRepositry<VisitorLog> entityDal, AbstractControllerParameters myWorkClasses, BizParameters bizParameters)
            : base(entityDal, bizParameters)

        {

        }


    }
}
