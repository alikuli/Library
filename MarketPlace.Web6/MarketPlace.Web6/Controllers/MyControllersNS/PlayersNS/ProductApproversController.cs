using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.ProductApproverNS;

namespace MarketPlace.Web6.Controllers
{

    [Authorize]
    public class ProductApproversController : EntityAbstractController<ProductApprover>
    {
        ProductApproverBiz _productApproverBiz;
        public ProductApproversController(ProductApproverBiz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _productApproverBiz = biz;
        }

        ProductApproverBiz ProductApproverBiz
        {
            get
            {
                return _productApproverBiz;
            }
        }

        UserBiz UserBiz
        {
            get
            {
                return ProductApproverBiz.CashTrxBiz.PersonBiz.UserBiz;
            }
        }
        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");

            if (!UserBiz.IsAdmin(UserId))
                throw new Exception("You are not an administrator");

            ProductApprover productApprover = parm.Entity as ProductApprover;
            productApprover.IsNullThrowException("Unable to unbox ProductApprover");

            productApprover.SelectListProductApproverCategory = ProductApproverBiz.ProductApproverCategoryBiz.SelectList();

            productApprover.SelectListPeople = ProductApproverBiz.PersonBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);
        }


        public override async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, EnumLibrary.EnumNS.MenuENUM menuEnum = MenuENUM.IndexDefault, EnumLibrary.EnumNS.SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, bool isMenu = false, string menuPathMainId = "", string viewName = "Index")
        {
            try
            {
                UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");

                if (!UserBiz.IsAdmin(UserId))
                    throw new Exception("You are not an administrator");

                return await base.Index(id, searchFor, isandForSearch, selectedId, returnUrl, menuEnum, sortBy, print, isMenu, menuPathMainId, viewName);
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Menus");
            }
        }
    }
}