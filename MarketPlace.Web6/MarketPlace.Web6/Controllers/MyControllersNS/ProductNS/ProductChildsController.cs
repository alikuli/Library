using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductChildsController : EntityAbstractController<ProductChild>
    {

        ProductBiz _productBiz;
        UserBiz _userBiz;
        public ProductChildsController(ProductChildBiz productChildBiz, IErrorSet errorSet, ProductBiz productBiz, UserBiz userBiz)
            : base(productChildBiz, errorSet, userBiz)
        {
            _productBiz = productBiz;
            _userBiz = userBiz;
        }

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ViewBag.ProductSelectList = _productBiz.SelectList();
            ViewBag.UserSelectList = _userBiz.SelectList();

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
