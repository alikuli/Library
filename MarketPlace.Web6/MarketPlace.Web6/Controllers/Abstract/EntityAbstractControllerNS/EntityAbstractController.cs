using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.Interface;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class EntityAbstractController<TEntity> : AbstractController where TEntity : class, ICommonWithId
    {

        protected IBusinessLayer<TEntity> _icrudBiz;
        protected Type _tEntityType;
        //private UserBiz _userBiz;
        //RightBiz _rightBiz;
        //MyWorkClasses _myWorkClasses;
        public EntityAbstractController(IBusinessLayer<TEntity> icrudUow, BreadCrumbManager bcm, IErrorSet err, PageViewBiz pageViewBiz)
            : base(bcm, err, pageViewBiz) 
        {
            _icrudBiz = icrudUow;
            _tEntityType = typeof(TEntity);

            //_rightBiz = rightBiz;
            //_myWorkClasses = myWorkClasses;
        }

        public BusinessLayer<TEntity> Biz
        {
            get
            {
                return (BusinessLayer<TEntity>)_icrudBiz;
            }
        }


    }
}