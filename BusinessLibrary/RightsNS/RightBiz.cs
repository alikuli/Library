using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using ModelsClassLibrary.ViewModels;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
namespace UowLibrary.PlayersNS
{
    public partial class RightBiz : BusinessLayer<Right>
    {


        public RightBiz(BizParameters bizParameters, IRepositry<Right> entityDal )
            : base(entityDal,bizParameters)
        {
        }






    }
}
