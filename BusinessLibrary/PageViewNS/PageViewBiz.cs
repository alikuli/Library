using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InvoiceNS;
using MigraDocLibrary;
using MigraDocLibrary.InvoiceNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using System.Linq;
using ErrorHandlerLibrary;

namespace UowLibrary.PageViewNS
{
    public partial class PageViewBiz : BusinessLayer<PageView>
    {
        public PageViewBiz(IRepositry<PageView> entityDal,  IMemoryMain memoryMain, IErrorSet errorSet, ConfigManagerHelper configManagerHelper, BreadCrumbManager breadCrumbManager)
            : base(entityDal, null, memoryMain, null, errorSet, configManagerHelper, breadCrumbManager)
        {
            
        }

        internal int GetClickCount()
        {
            return FindAll().Count();
        }

    }
}
