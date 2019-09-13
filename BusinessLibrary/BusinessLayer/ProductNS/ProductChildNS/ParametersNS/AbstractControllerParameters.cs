using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.PageViewNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UowLibrary.PageViewNS;
using UowLibrary.SuperLayerNS.AccountsNS;
using UowLibrary.UploadFileNS;
using WebLibrary.Programs;

namespace UowLibrary.ParametersNS
{
    public class AbstractControllerParameters
    {
        IMemoryMain _memoryMain;
        IErrorSet _errorSet; 
        ConfigManagerHelper _configManager; 
        BreadCrumbManager _breadCrumbManager;
        PageViewBiz _pageViewBiz;
        //GlobalObject _globalObject;
        public AbstractControllerParameters(IMemoryMain memoryMain, IErrorSet errorSet, ConfigManagerHelper configManager, BreadCrumbManager breadCrumbManager, PageViewBiz pageViewBiz /*, GlobalObject globalObject */)
        {
            _memoryMain = memoryMain;
            _errorSet = errorSet;
            _configManager = configManager;
            _breadCrumbManager = breadCrumbManager;
            _pageViewBiz = pageViewBiz;
            //_globalObject = globalObject;

        }


        //public GlobalObject GlobalObject
        //{
        //    get
        //    {
        //        return GlobalObject;
        //    }
        //}
        public string UserId { get; set; }
        public string UserName { get; set; }
        public MemoryMain MemoryMain
        {
            get
            {
                return ((MemoryMain)_memoryMain);
            }
        }


        public PageViewBiz PageViewBiz
        {
            get { return _pageViewBiz; }
        }

        public IErrorSet ErrorSet
        {
            get
            {
                return _errorSet;
            }
        }

        public ConfigManagerHelper ConfigManagerHelper
        {
            get
            {
                return _configManager;
            }
        }

        public BreadCrumbManager BreadCrumbManager
        {
            get
            {
                return _breadCrumbManager;
            }
        }

    }
}
