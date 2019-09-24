using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS.SalesmanNS;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.SalesmanNS;
using UowLibrary.SuperLayerNS;

namespace MarketPlace.Web6.Controllers
{

    [Authorize]
    public class SalesmansController : EntityAbstractController<Salesman>
    {
        SuperBiz _superBiz;
        SalesmanBiz _salesmanBiz;
        public SalesmansController(SalesmanBiz biz, AbstractControllerParameters param, SuperBiz superBiz)
            : base(biz, param)
        {
            _salesmanBiz = biz;
            _superBiz = superBiz;

        }

        SuperBiz SuperBiz
        {
            get
            {
                _superBiz.UserId = UserId;
                _superBiz.UserName = UserName;

                return _superBiz;
            }
        }

        SalesmanBiz SalesmanBiz
        {
            get
            {
                return _salesmanBiz;
            }
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            setupCode(parm);
            return base.Event_Create_ViewAndSetupSelectList_GET(parm);
        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            setupCode(parm);
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }

        
        private void setupCode(ControllerIndexParams parm)
        {
            UserId.IsNullOrWhiteSpaceThrowException("You are not logged in");
            Salesman salesman = parm.Entity as Salesman;
            salesman.IsNullThrowException("Unable to unbox Salesman");

            salesman.SelectListSalesmanCategory = SalesmanBiz.SalesmanCategoryBiz.SelectList();
            salesman.SelectListBillAddress = SalesmanBiz.SelectListBillAddressesFor(UserId);
            salesman.SelectListShipAddress = SalesmanBiz.SelectListShipAddressesFor(UserId);
            salesman.SelectListParentSalesmen = SalesmanBiz.SelectListSuperSalesmen();
            salesman.SelectListPeople = _salesmanBiz.PersonBiz.SelectList(); ;
        }


        public ActionResult GetListOfPeopleCurrentUserHasSoldTo()
        {

            try
            {
                UserId.IsNullOrWhiteSpaceThrowException("You must be logged in");
                PersonServedBySalesmanWithTotalTrxAmountAdded_Header personServedBySalesmanWithTotalTrxAmountAdded_Header = SuperBiz.CreateSalesmanReport_PeopleServedBySalesman(UserId);
                return View(personServedBySalesmanWithTotalTrxAmountAdded_Header);
            }
            catch (Exception ex)
            {
                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), ex);
            }

            return RedirectToAction("Index", "Menus");

        }

    }
}