using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.PlayersNS.CashierCategoryNS
{
    public partial class CashierCategoryBiz : BusinessLayer<CashierCategory>
    {
        public CashierCategoryBiz(IRepositry<CashierCategory> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

        }



    }
}
