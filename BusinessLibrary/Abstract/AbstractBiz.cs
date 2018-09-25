using ErrorHandlerLibrary.ExceptionsNS;
using System;
using UowLibrary.Interface;
using WebLibrary.Programs;
using AliKuli.Extentions;
using System.Reflection;
using Microsoft.AspNet.Identity;
using UowLibrary.ParametersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UowLibrary.PageViewNS;
using ErrorHandlerLibrary;
using AliKuli.UtilitiesNS;

namespace UowLibrary.Abstract
{
    /// <summary>
    /// Note, for this to work properly you need to send the UserIdString into this from the Controller or any other program that is on top
    /// To see how a controller sends it see the OnActionExecuting(ActionExecutingContext filterContext) method in the Abstract Controller.
    /// and then the properties Uow and UowManagment/UowAccounts in ManagementController and AccountController 
    /// </summary>
    public abstract partial class AbstractBiz : IBiz
    {

        UploadedFileBiz _uploadedFileBiz;
        IMemoryMain _memoryMain;
        PageViewBiz _pageViewBiz;
        IErrorSet _errorSet;
        ConfigManagerHelper _configManagerHelper;
        BreadCrumbManager _breadCrumbManager;
        public AbstractBiz(BizParameters param)
            : this(param.UploadedFileBiz, param.MemoryMain, param.PageViewBiz, param.ErrorSet, param.ConfigManagerHelper, param.BreadCrumbManager)
        {


        }





        public AbstractBiz(UploadedFileBiz uploadedFileBiz, IMemoryMain memoryMain, PageViewBiz pageViewBiz, IErrorSet errorSet, ConfigManagerHelper configManagerHelper, BreadCrumbManager breadCrumbManager)
        {
            _uploadedFileBiz = uploadedFileBiz;

            _memoryMain = memoryMain;
            _pageViewBiz = pageViewBiz;
            _errorSet = errorSet;
            _configManagerHelper = configManagerHelper;
            _breadCrumbManager = breadCrumbManager;

            errorSet.SetLibAndClass("Uow Library", "UOW_Abstract");

        }
        //public AbstractBiz(AbstractControllerParameters param)
        //{
        //    //Get parameters

        //    _memoryMain = param.MemoryMain;
        //    _pageViewBiz = param.PageViewBiz;
        //    _errorSet = param.ErrorSet;
        //    _configManagerHelper = param.ConfigManagerHelper;
        //    _breadCrumbManager = param.BreadCrumbManager;

        //    param.ErrorSet.SetLibAndClass("Uow Library", "UOW_Abstract");


        //}
        string _userId;
        string _userName;
        public string UserId
        {
            get{return _userId;}
            set{_userId = value;}
        }
        public string UserName
        {
            get{return _userName;}
            set{_userName = value;}
        }

        public UploadedFileBiz UploadedFileBiz
        {
            get { return _uploadedFileBiz; }
        }


        public MemoryMain MemoryMain
        {
            get
            {
                return _memoryMain as MemoryMain;
            }
        }


        public PageViewBiz PageViewBiz
        {
            get 
            {
                return _pageViewBiz; 
            }
        }

        public ErrorSet ErrorsGlobal
        {
            get
            {
                return _errorSet as ErrorSet;
            }
        }

        public ConfigManagerHelper ConfigManagerHelper
        {
            get
            {
                return _configManagerHelper;
            }
        }

        public BreadCrumbManager BreadCrumbManager
        {
            get
            {
                return _breadCrumbManager;
            }
        }

        //public CountryBiz CountryBiz
        //{
        //    get
        //    {
        //        return RightBiz.UserBiz.CountryBiz;
        //    }
        //}
        //public BreadCrumbManager BreadCrumbManager
        //{
        //    get{return _bizParam.BreadCrumbManager;}
        //}

        public virtual void EncryptDecrypt()
        {
            throw new NotImplementedException();
        }



    }


}



