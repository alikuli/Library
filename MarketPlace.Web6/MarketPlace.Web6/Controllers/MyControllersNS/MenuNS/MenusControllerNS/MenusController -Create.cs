using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace MarketPlace.Web6.Controllers
{
    public partial class MenusController : EntityAbstractController<MenuPathMain>
    {




        public override async Task<ActionResult> Create(
            MenuPathMain entity, 
            string returnUrl, 
            HttpPostedFileBase[] httpMiscUploadedFiles = null, 
            HttpPostedFileBase[] httpSelfieUploads = null, 
            HttpPostedFileBase[] httpIdCardFrontUploads = null, 
            HttpPostedFileBase[] httpIdCardBackUploads = null, 
            HttpPostedFileBase[] httpPassportFrontUploads = null, 
            HttpPostedFileBase[] httpPassportVisaUploads = null, 
            HttpPostedFileBase[] httpLiscenseFrontUploads = null, 
            HttpPostedFileBase[] httpLiscenseBackUploads = null, 
            SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, 
            string searchFor = "", 
            string selectedId = "", 
            bool print = false, 
            string isandForSearch = "", 
            MenuENUM menuEnum = MenuENUM.CreateDefault,
            string button = "",
            FormCollection fc = null)
        {

            try
            {

                return await base.Create(entity, returnUrl, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuEnum, button, fc);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Error in Menu Controller.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();

                return RedirectToAction("Index", new { id = "", searchFor = searchFor, isandForSearch = isandForSearch, selectedId = selectedId, returnUrl = returnUrl, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });

            }
        }





    }





}