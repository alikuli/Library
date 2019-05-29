using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.FeatureNS.MenuFeatureNS;
//using UowLibrary.FeaturesNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
namespace MarketPlace.Web6.Controllers
{
    public class MenuPath1sController : EntityAbstractController<MenuPath1>
    {

        MenuPath1Biz _menupath1Biz;
        MenuFeatureBiz _menuFeatureBiz;

        public MenuPath1sController(MenuPath1Biz biz, AbstractControllerParameters param, MenuFeatureBiz menuFeatureBiz)
            : base(biz, param)
        {
            _menupath1Biz = biz;
            _menuFeatureBiz = menuFeatureBiz;
        }


        MenuPath1Biz MenuPath1Biz
        {
            get
            {
                return _menupath1Biz;
            }
        }

        MenuFeatureBiz MenuFeatureBiz
        {
            get
            {
                _menuFeatureBiz.UserId = UserId;
                _menuFeatureBiz.UserName = UserName;
                return _menuFeatureBiz;
            }
        }
        public async Task<ActionResult> DeleteUploadedFile(string menupathId, string uploadedFileId)
        {
            //delete from the productCategory1
            await _menupath1Biz.DeleteUploadedFile(menupathId, uploadedFileId);
            return RedirectToAction("Edit", new { id = menupathId });
        }





        public ActionResult AddFeature(string id, string parentName, string returnUrl)
        {
            id.IsNullOrWhiteSpaceThrowArgumentException("Id");
            parentName.IsNullOrWhiteSpaceThrowArgumentException("parentName");
            returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");

            MenuFeatureModel menuFeatureModel = new MenuFeatureModel();
            menuFeatureModel.ParentId = id;
            menuFeatureModel.ParentName = parentName;
            menuFeatureModel.SelectListFeature = MenuFeatureBiz.SelectList();
            menuFeatureModel.ReturnUrl = returnUrl;
            return View(menuFeatureModel);
        }

        [HttpPost]
        public ActionResult AddFeature(MenuFeatureModel menuFeatureModel)
        {
            try
            {
                MenuPath1Biz.AddFeature(menuFeatureModel);
            }
            catch (System.Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
            }
            if (menuFeatureModel.ReturnUrl.IsNullOrWhiteSpace())
                return View("Index");

            return Redirect(menuFeatureModel.ReturnUrl);
        }

        public ActionResult DeleteFeature(string menuPathid, string menuFeatureId, string returnUrl)
        {
            try
            {
                menuFeatureId.IsNullOrWhiteSpaceThrowArgumentException("menuFeatreId");
                returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");
                menuPathid.IsNullOrWhiteSpaceThrowArgumentException("menuPath1id");

                MenuFeatureDeleteModel menuFeatureDeleteModel = new MenuFeatureDeleteModel(menuPathid, menuFeatureId, returnUrl);

                menuFeatureDeleteModel.MenuFeature = MenuFeatureBiz.Find(menuFeatureId);
                menuFeatureDeleteModel.MenuPath = MenuPath1Biz.Find(menuPathid) as IMenuPath;

                menuFeatureDeleteModel.SelfCheck();

                return View(menuFeatureDeleteModel);

            }
            catch (System.Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
                if (returnUrl.IsNullOrWhiteSpace())
                    return View("Index");

                return Redirect(returnUrl);

            }

        }

        [HttpPost]
        public ActionResult DeleteFeature(MenuFeatureDeleteModel menuFeatureDeleteModel)
        {
            try
            {
                menuFeatureDeleteModel.IsNullThrowException("menuFeatureDeleteModel");
                MenuPath1Biz.DeleteFeature(menuFeatureDeleteModel);
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
            }

            if (menuFeatureDeleteModel.ReturnUrl.IsNullOrWhiteSpace())
                return View("Index");

            return Redirect(menuFeatureDeleteModel.ReturnUrl);
        }


        public ActionResult CreateNewFeature(string menuPathid, string returnUrl)
        {
            menuPathid.IsNullOrWhiteSpaceThrowArgumentException("menuPathid");
            returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");

            CreateNewFeatureModel createNewFeatureModel = new CreateNewFeatureModel();
            createNewFeatureModel.ParentId = menuPathid;
            createNewFeatureModel.ReturnUrl = returnUrl;

            return View(createNewFeatureModel);

        }
        [HttpPost]
        public ActionResult CreateNewFeature(CreateNewFeatureModel createNewFeatureModel)
        {

            try
            {
                createNewFeatureModel.IsNullThrowException("createNewFeatureModel");
                MenuPath1Biz.CreateNewFeature(createNewFeatureModel);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
            }
            if (createNewFeatureModel.ReturnUrl.IsNullOrWhiteSpace())
                return View("Index");

            return Redirect(createNewFeatureModel.ReturnUrl);

        }


        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            parm.Entity.MenuManager.ReturnUrl = Url.Action("Index", "MenuPath1s", new { menuEnum = MenuENUM.IndexMenuPath1 });
            return base.Event_CreateViewAndSetupSelectList(parm);
        }

        public override void Event_BeforeSaveInCreateAndEdit(ControllerCreateEditParameter parm)
        {
            base.Event_BeforeSaveInCreateAndEdit(parm);
            parm.ReturnUrl = Url.Action("Edit", "MenuPath1s", new { id = parm.Entity.Id });
        }

    }
}
