using ModelsClassLibrary.ModelsNS.Logs.VisitorsLogNS;
using ModelsClassLibrary.ViewModels;

namespace UowLibrary.VisitorLogNS
{
    public partial class VisitorLogBiz : BusinessLayer<VisitorLog>
    {
        public override string SelectListCacheKey
        {
            get { return "VisitorLogSelectListKey"; }
        }



    }
}
