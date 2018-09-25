using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.FeaturesNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath1sController : EntityAbstractController<MenuPath1>
    {

        MenuPath1Biz _menupath1Biz;
        //FeatureBiz _featureBiz;
        //MenuPath1FeatureBiz _menuPath1FeatureBiz;
        public MenuPath1sController(MenuPath1Biz biz,  AbstractControllerParameters param)
            : base(biz, param) 
        {
            _menupath1Biz = biz;
            //featureBiz.IsNullThrowException();
            //_featureBiz = featureBiz;
            //_menuPath1FeatureBiz = menuPath1FeatureBiz;
        }




        public async Task<ActionResult> DeleteUploadedFile(string menupathId, string uploadedFileId)
        {
            //delete from the productCategory1
            await _menupath1Biz.DeleteUploadedFile(menupathId, uploadedFileId);
            return RedirectToAction("Edit", new { id = menupathId });
        }











        //public ActionResult AddNewField(string id)
        //{
        //    //Feature feature = _featureBiz.Factory() as Feature;
        //    //feature.MenuPath1Id = id;


        //    FeatureTypeENUM TypeEnum = FeatureTypeENUM.Unknown;
        //    ViewBag.SelectListFeaturesTypeEnum = EnumExtention.ToSelectListSorted<FeatureTypeENUM>(TypeEnum);


        //    return View(feature);


        //}

        //[HttpPost]
        //public ActionResult AddNewField(Feature feature)
        //{
        //    //first get the Menupath
        //    //add the feature to it
        //    //save it.

        //    try
        //    {
        //        _menupath1Biz.AddFeature(feature.MenuPath1Id, feature);
        //        return RedirectToAction("Edit", new { id = feature.MenuPath1Id });
        //    }

        //    catch (System.Exception e)
        //    {

        //        ErrorsGlobal.Add("Error while adding new field", MethodBase.GetCurrentMethod(), e);
        //        return RedirectToAction("Edit", new { id = feature.MenuPath1Id });
        //    }


        //}


    }
}
