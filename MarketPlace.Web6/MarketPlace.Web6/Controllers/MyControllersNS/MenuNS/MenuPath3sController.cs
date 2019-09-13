using AliKuli.Extentions;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.FeaturesNS.MenuFeatureNS;
using ModelsClassLibrary.ModelsNS.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;
namespace MarketPlace.Web6.Controllers
{
    public class MenuPath3sController : EntityAbstractController<MenuPath3>
    {

        MenuPath3Biz _menupath3Biz;
        MenuFeatureBiz _menuFeatureBiz;
        MenuPathMainBiz _menuPathMainBiz;

        public MenuPath3sController(MenuPath3Biz biz, AbstractControllerParameters param, MenuFeatureBiz menuFeatureBiz, MenuPathMainBiz menuPathMainBiz)
            : base(biz, param)
        {
            _menupath3Biz = biz;
            _menuFeatureBiz = menuFeatureBiz;
            _menuPathMainBiz = menuPathMainBiz;
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

        MenuPathMainBiz MenuPathMainBiz
        {
            get
            {
                _menuPathMainBiz.UserId = UserId;
                _menuPathMainBiz.UserName = UserName;
                return _menuPathMainBiz;

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

        public ActionResult DeleteFeature(string menuPathid, string menuFeatureId, string returnUrl)
        {
            try
            {
                menuFeatureId.IsNullOrWhiteSpaceThrowArgumentException("menuFeatureId");
                returnUrl.IsNullOrWhiteSpaceThrowArgumentException("returnUrl");
                menuPathid.IsNullOrWhiteSpaceThrowArgumentException("menuPath3id");

                MenuFeatureDeleteModel menuFeatureDeleteModel = new MenuFeatureDeleteModel(menuPathid, menuFeatureId, returnUrl);

                menuFeatureDeleteModel.MenuFeature = MenuFeatureBiz.Find(menuFeatureId);
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

        //public override void Event_BeforeSaveInCreateAndEdit(ControllerCreateEditParameter parm)
        //{
        //    base.Event_BeforeSaveInCreateAndEdit(parm);

        //    //this is the main screen from where the create started.
        //}


        public override void Event_AfterSaveInCreate(ControllerCreateEditParameter parm)
        {
            //create a menupath if you have all the data
            MenuPath3 mp3 = parm.Entity as MenuPath3;

            string menupath1Id = mp3.MenuPath1Id_Parent;
            string menupath2Id = mp3.MenuPath2Id_Parent;

            //if either id's is missing, abort
            if (menupath1Id.IsNullOrWhiteSpace() || menupath1Id.IsNullOrWhiteSpace())
            {
                return;
            }

            //create a menuPathMain
            MenuPathMain mpm = MenuPathMainBiz.Factory() as MenuPathMain;
            mpm.MenuPath1Id = menupath1Id;
            mpm.MenuPath2Id = menupath2Id;
            mpm.MenuPath3Id = parm.Entity.Id;

            if (mp3.MenuPathMains.IsNull())
                mp3.MenuPathMains = new List<MenuPathMain>();

            mp3.MenuPathMains.Add(mpm);
            MenuPathMainBiz.CreateAndSave(mpm);

            string orignalReturnUrl = parm.ReturnUrl;
            parm.ReturnUrl = Url.Action("Edit", "MenuPath3s", new { id = parm.Entity.Id, returnUrl = orignalReturnUrl, menuPathMainId = mpm.Id });

        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            if (parm.Entity.IsNull())
                return View(Biz.FactoryForHttpGet());


            MenuPath3 menuPath3 = (MenuPath3)parm.Entity;
            string menuPathMainId = parm.MenuPathMainId;
            //menuPathMainId.IsNullOrWhiteSpaceThrowException("menuPathMainId");

            //this would be during create when a menuPathMainId is sent.
            if (!menuPathMainId.IsNullOrWhiteSpace())
            {
                MenuPathMain mpm = MenuPathMainBiz.Find(menuPathMainId);
                mpm.IsNullThrowException();


                mpm.MenuPath1Id.IsNullOrWhiteSpaceThrowException("MenuPath1Id");
                mpm.MenuPath2Id.IsNullOrWhiteSpaceThrowException("MenuPath2Id");

                //this loads the MenuPath3 so that it can create a new MenuPathMain when created
                menuPath3.MenuPath1Id_Parent = mpm.MenuPath1Id;
                menuPath3.MenuPath2Id_Parent = mpm.MenuPath2Id;
            }
            return View(menuPath3);

        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            if (parm.Entity.IsNull())
                return View(Biz.FactoryForHttpGet());


            MenuPath3 menuPath3 = (MenuPath3)parm.Entity;
            string menuPathMainId = parm.MenuPathMainId;
            //menuPathMainId.IsNullOrWhiteSpaceThrowException("menuPathMainId");

            //this would be during create when a menuPathMainId is sent.
            if (!menuPathMainId.IsNullOrWhiteSpace())
            {
                MenuPathMain mpm = MenuPathMainBiz.Find(menuPathMainId);
                mpm.IsNullThrowException();


                mpm.MenuPath1Id.IsNullOrWhiteSpaceThrowException("MenuPath1Id");
                mpm.MenuPath2Id.IsNullOrWhiteSpaceThrowException("MenuPath2Id");

                //this loads the MenuPath3 so that it can create a new MenuPathMain when created
                menuPath3.MenuPath1Id_Parent = mpm.MenuPath1Id;
                menuPath3.MenuPath2Id_Parent = mpm.MenuPath2Id;
            }
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }
    }
}