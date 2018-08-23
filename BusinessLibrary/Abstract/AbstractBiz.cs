using ErrorHandlerLibrary.ExceptionsNS;
using System;
using UowLibrary.Interface;
using WebLibrary.Programs;
using AliKuli.Extentions;
using System.Reflection;
using Microsoft.AspNet.Identity;
using UowLibrary.MyWorkClassesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using BreadCrumbsLibraryNS.Programs;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;

namespace UowLibrary.Abstract
{
    /// <summary>
    /// Note, for this to work properly you need to send the UserIdString into this from the Controller or any other program that is on top
    /// To see how a controller sends it see the OnActionExecuting(ActionExecutingContext filterContext) method in the Abstract Controller.
    /// and then the properties Uow and UowManagment/UowAccounts in ManagementController and AccountController 
    /// </summary>
    public abstract partial class AbstractBiz : IBiz
    {
        private readonly IMemoryMain _imemoryMain;
        MyWorkClasses _myWorkClasses;

        public AbstractBiz(MyWorkClasses myWorkClasses)
        {
            //Get parameters
            _imemoryMain = myWorkClasses.MemoryMain;
            _ierrorsGlobal = myWorkClasses.ErrorSet;
            _myWorkClasses = myWorkClasses;
            _ierrorsGlobal.SetLibAndClass("Uow Library", "UOW_Abstract");


        }

        string _userId;
        string _userName;
        public string UserId
        {
            get
            {
                _userId.IsNullOrWhiteSpaceThrowException("Programming Error. UserId in Business Abstract Layer not set.");
                return _userId;
            }
            set
            {
                _userId = value;
            }
        }
        public string UserName
        {
            get
            {
                //_userName.IsNullOrWhiteSpaceThrowException("Programming Error. User Name in Business Abstract Layer not set.");
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        //public UploadedFileBiz UploadedFileBiz
        //{
        //    get
        //    {
        //        return _uploadedFileBiz;
        //    }
        //}

        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        return _rightBiz.UserBiz;
        //    }
        //}

        //public RightBiz RightBiz
        //{
        //    get
        //    {
        //        return _rightBiz;
        //    }
        //}

        public MyWorkClasses MyWorkClasses
        {
            get
            {
                return _myWorkClasses;
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
        //    get
        //    {
        //        return MyWorkClasses.BreadCrumbManager;
        //    }
        //}

        public virtual void EncryptDecrypt()
        {
            throw new NotImplementedException();
        }



    }


}



