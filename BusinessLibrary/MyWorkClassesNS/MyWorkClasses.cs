using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UowLibrary.UploadFileNS;
using WebLibrary.Programs;

namespace UowLibrary.MyWorkClassesNS
{
    public class MyWorkClasses
    {
        IMemoryMain _memoryMain;
        IErrorSet _errorSet; 
        ConfigManagerHelper _configManager; 
        //BreadCrumbManager _breadCrumbManager;
        public MyWorkClasses(IMemoryMain memoryMain, IErrorSet errorSet, ConfigManagerHelper configManager)
        {
            _memoryMain = memoryMain;
            _errorSet = errorSet;
            _configManager = configManager;


        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public IMemoryMain MemoryMain
        {
            get
            {
                return _memoryMain;
            }
        }


        public IErrorSet ErrorSet
        {
            get
            {
                return _errorSet;
            }
        }

        public ConfigManagerHelper ConfigManager
        {
            get
            {
                return _configManager;
            }
        }

        //public BreadCrumbManager BreadCrumbManager
        //{
        //    get
        //    {
        //        return _breadCrumbManager;
        //    }
        //}

    }
}
