using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.Interface;
using UowLibrary.ParametersNS;

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
        public EntityAbstractController(IBusinessLayer<TEntity> icrudUow, AbstractControllerParameters param)
            : base(param)
        {
            _icrudBiz = icrudUow;
            _tEntityType = typeof(TEntity);
        }

        public BusinessLayer<TEntity> Biz
        {
            get
            {
                BusinessLayer<TEntity> businessLayer = _icrudBiz as BusinessLayer<TEntity>;
                businessLayer.IsNullThrowException();
                businessLayer.UserId = UserId;
                businessLayer.UserName = UserName;
                return businessLayer;
            }
        }


        //[Inject]
        //public AccountsBiz AccountsBiz { get; set; }

        protected void Hide_Save_Button()
        {
            ViewBag.ShowEditControls = false.ToString();
        }

        protected void Show_Save_Button()
        {
            ViewBag.ShowEditControls = true.ToString();
        }

        protected ActionResult throwError(Exception e)
        {
            ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
            ErrorsGlobal.MemorySave();
            return RedirectToAction("Index", "Menus");
        }

    }
}