using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
//using ModelsClassLibrary.ModelsNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Threading.Tasks;
using System.Web.Mvc;
//using UowLibrary.FeaturesNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class MenuPath2sController : EntityAbstractController<MenuPath2>
    {

        MenuPath2Biz _menupath2Biz;
        //MenuPath2FeatureBiz _menuPath2FeatureBiz;
        #region Constructo and initializers

        public MenuPath2sController(MenuPath2Biz biz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _menupath2Biz = biz;
            //_menuPath2FeatureBiz = menuPath2FeatureBiz;

        }

        #endregion



        public async Task<ActionResult> DeleteUploadedFile(string menuPathId, string uploadedFileId)
        {
            //delete from the productCategory2
            await _menupath2Biz.DeleteUploadedFile(menuPathId, uploadedFileId);
            return RedirectToAction("Edit", new { id = menuPathId });
            //return RedirectToAction("DeleteConfirmed", "UploadedFiles", new { id = uploadedFileId });
        }

        //public async Task<ActionResult> CreateParentChild(MenuPath2 parent, MenuPath1Feature child, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, EnumLibrary.EnumNS.SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", EnumLibrary.EnumNS.MenuENUM menuEnum = MenuENUM.CreateDefault, FormCollection fc = null)
        //{
        //    //check to see if the entity exists.
        //    bool parentExists = await isParentExistingAsync(parent); ;

        //    //check to see if child is Null
        //    bool childIsNull = child.IsNull();

        //    //do the normal create for this controller
        //    if (!parentExists)
        //    {
        //        createAndSaveTheParent(parent, child, returnUrl, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuEnum);
        //    }

        //    if (!childIsNull)
        //    {
        //        createAndSaveTheChild(parent, child, returnUrl, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuEnum);
        //    }

        //    //return to parent
        //    return RedirectToAction("Edit", new { id = parent.Id });
        //}

        //private void createAndSaveTheParent(MenuPath2 parent, MenuPath1Feature child, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles, System.Web.HttpPostedFileBase[] httpSelfieUploads, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads, System.Web.HttpPostedFileBase[] httpIdCardBackUploads, System.Web.HttpPostedFileBase[] httpPassportFrontUploads, System.Web.HttpPostedFileBase[] httpPassportVisaUploads, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads, SortOrderENUM sortBy, string searchFor, string selectedId, bool print, string isandForSearch, MenuENUM menuEnum)
        //{
        //    MenuPath2 dudEntity = Biz.EntityFactoryForHttpGet() as MenuPath2;

        //    ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
        //        parent,
        //        httpMiscUploadedFiles,
        //        httpSelfieUploads,
        //        httpIdCardFrontUploads,
        //        httpIdCardBackUploads,
        //        httpPassportFrontUploads,
        //        httpPassportVisaUploads,
        //        httpLiscenseFrontUploads,
        //        httpLiscenseBackUploads,
        //        MenuENUM.CreateDefault,
        //        UserName,
        //        UserId,
        //        returnUrl);

        //    InitializeMenuManager(parm);
        //    Biz.CreateAndSave(parm);
        //}

        //private void createAndSaveTheChild(MenuPath2 parent, MenuPath1Feature child, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles, System.Web.HttpPostedFileBase[] httpSelfieUploads, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads, System.Web.HttpPostedFileBase[] httpIdCardBackUploads, System.Web.HttpPostedFileBase[] httpPassportFrontUploads, System.Web.HttpPostedFileBase[] httpPassportVisaUploads, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads, SortOrderENUM sortBy, string searchFor, string selectedId, bool print, string isandForSearch, MenuENUM menuEnum)
        //{
        //    MenuPath2Feature dudEntity = _menupath2Biz.EntityFactoryForHttpGet() as MenuPath2Feature;

        //    ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
        //        child,
        //        httpMiscUploadedFiles,
        //        httpSelfieUploads,
        //        httpIdCardFrontUploads,
        //        httpIdCardBackUploads,
        //        httpPassportFrontUploads,
        //        httpPassportVisaUploads,
        //        httpLiscenseFrontUploads,
        //        httpLiscenseBackUploads,
        //        MenuENUM.CreateDefault,
        //        UserName,
        //        UserId,
        //        returnUrl);

        //    InitializeMenuManager(parm);
        //    _menuPath2FeatureBiz.CreateAndSave(parm);
        //}


        //private async Task<bool> isParentExistingAsync(MenuPath2 parent)
        //{
        //    var entity = await Biz.FindAsync(parent.Id);
        //    bool found = !entity.IsNull();
        //    return found;
        //}


    }
}