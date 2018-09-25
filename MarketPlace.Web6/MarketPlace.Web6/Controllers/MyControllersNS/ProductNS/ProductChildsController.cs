using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using ErrorHandlerLibrary;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductChildsController : EntityAbstractController<ProductChild>
    {

        ProductBiz _productBiz;
        UserBiz _userBiz;

        public ProductChildsController(ProductBiz biz, UserBiz userBiz, AbstractControllerParameters param)
            : base(biz.ProductChildBiz, param) 
        {
            _productBiz = biz;
            _userBiz = userBiz;
        }

        UserBiz UserBiz
        {
            get
            {
                return UserBiz;
            }
        }
        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.ProductSelectList = _productBiz.SelectList();
            ViewBag.UserSelectList = UserBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);

        }

        //public override void Event_LoadUserIntoEntity(ProductChild entity)
        //{
        //    if (entity.UserId.IsNullOrWhiteSpace())
        //    {
        //        ErrorsGlobal.Add("The Owner is required. The Id is missing.", MethodBase.GetCurrentMethod());
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }

        //    entity.User = _userBiz.Find(entity.UserId);

        //    if (entity.User.IsNull())
        //    {
        //        ErrorsGlobal.Add("The Owner was not found.", MethodBase.GetCurrentMethod());
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }

        //}
    }
}
