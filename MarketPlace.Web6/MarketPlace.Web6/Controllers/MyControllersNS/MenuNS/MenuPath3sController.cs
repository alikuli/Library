using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;
using AliKuli.Extentions;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using System.Reflection;
using System;
namespace MarketPlace.Web6.Controllers
{
    public class MenuPath3sController : EntityAbstractController<MenuPath3>
    {

        MenuPath3Biz _menupath3Biz;
        MenuFeatureBiz _menuFeatureBiz;

        public MenuPath3sController(MenuPath3Biz biz, AbstractControllerParameters param, MenuFeatureBiz menuFeatureBiz)
            : base(biz, param)
        {
            _menupath3Biz = biz;
            _menuFeatureBiz = menuFeatureBiz;
        }


        MenuPath3Biz MenuPath3Biz
        {
            get
            {
                return _menupath3Biz;
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
            //delete from the ProductCategoryMain
            await _menupath3Biz.DeleteUploadedFile(uploadedFileId);
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
                MenuPath3Biz.AddFeature(menuFeatureModel);
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
                menuPathid.IsNullOrWhiteSpaceThrowArgumentException("menuPath3id");

                MenuFeatureDeleteModel menuFeatureDeleteModel = new MenuFeatureDeleteModel(menuPathid, menuFeatreId, returnUrl);

                menuFeatureDeleteModel.MenuFeature = MenuFeatureBiz.Find(menuFeatreId);
                menuFeatureDeleteModel.MenuPath = MenuPath3Biz.Find(menuPathid) as IMenuPath;

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
                MenuPath3Biz.DeleteFeature(menuFeatureDeleteModel);
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
                MenuPath3Biz.CreateNewFeature(createNewFeatureModel);

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