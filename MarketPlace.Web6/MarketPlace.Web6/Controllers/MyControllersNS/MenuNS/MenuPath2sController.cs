using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using System;
using System.Reflection;
//using ModelsClassLibrary.ModelsNS.FeaturesNS;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.MenuNS;
//using UowLibrary.FeaturesNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath2sController : EntityAbstractController<MenuPath2>
    {
        MenuFeatureBiz _menuFeatureBiz;
        MenuPath2Biz _menupath2Biz;
        #region Constructo and initializers

        public MenuPath2sController(MenuPath2Biz biz, AbstractControllerParameters param, MenuFeatureBiz menuFeatureBiz)
            : base(biz, param)
        {
            _menupath2Biz = biz;
            _menuFeatureBiz = menuFeatureBiz;

        }

        #endregion

        MenuPath2Biz MenuPath2Biz
        {
            get
            {
                return _menupath2Biz;
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

        public async Task<ActionResult> DeleteUploadedFile(string menuPathId, string uploadedFileId)
        {
            //delete from the productCategory2
            await _menupath2Biz.DeleteUploadedFile(menuPathId, uploadedFileId);
            return RedirectToAction("Edit", new { id = menuPathId });
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
                MenuPath2Biz.AddFeature(menuFeatureModel);
            }
            catch (System.Exception)
            {

                throw;
            }
            return Redirect(menuFeatureModel.ReturnUrl);
        }

        public ActionResult DeleteFeature(string menuPathid, string menuFeatreId, string returnUrl)
        {
            try
            {
                menuFeatreId.IsNullOrWhiteSpaceThrowArgumentException("menuFeatreId");
                returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");
                menuPathid.IsNullOrWhiteSpaceThrowArgumentException("menuPath2id");

                MenuFeatureDeleteModel menuFeatureDeleteModel = new MenuFeatureDeleteModel(menuPathid, menuFeatreId, returnUrl);

                menuFeatureDeleteModel.MenuFeature = MenuFeatureBiz.Find(menuFeatreId);
                menuFeatureDeleteModel.MenuPath = MenuPath2Biz.Find(menuPathid) as IMenuPath;

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
                MenuPath2Biz.DeleteFeature(menuFeatureDeleteModel);
            }
            catch (System.Exception)
            {

                throw;
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
            createNewFeatureModel.MenuPathId = menuPathid;
            createNewFeatureModel.ReturnUrl = returnUrl;

            return View(createNewFeatureModel);

        }
        [HttpPost]
        public ActionResult CreateNewFeature(CreateNewFeatureModel createNewFeatureModel)
        {

            try
            {
                createNewFeatureModel.IsNullThrowException("createNewFeatureModel");
                MenuPath2Biz.CreateNewFeature(createNewFeatureModel);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
            }
            if (createNewFeatureModel.ReturnUrl.IsNullOrWhiteSpace())
                return View("Index");

            return Redirect(createNewFeatureModel.ReturnUrl);

        }

    }
}