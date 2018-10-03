using AliKuli.UtilitiesNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary;
using ModelsClassLibrary.ModelsNS.DashBoardNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using System;
using System.Linq;
using WebLibrary.Programs;

namespace UowLibrary.PageViewNS
{
    public partial class PageViewBiz : BusinessLayer<PageView>
    {
        public PageViewBiz(IRepositry<PageView> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ConfigManagerHelper configManagerHelper, BreadCrumbManager breadCrumbManager)
            : base(entityDal, null, memoryMain, null, errorSet, configManagerHelper, breadCrumbManager)
        {

        }

        internal int GetClickCount()
        {
            return FindAll().Count();
        }







    }
}
