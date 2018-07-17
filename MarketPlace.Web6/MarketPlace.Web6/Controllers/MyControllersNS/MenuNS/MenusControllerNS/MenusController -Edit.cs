using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.MenuNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers
{
    public partial class MenusController : EntityAbstractController<MenuPathMain>
    {







        public override async Task<ActionResult> Edit(MenuPathMain entity, string returnUrl, string menuPathMainId, string productId, string productChildId, string MenuPath1Id, string MenuPath2Id, string MenuPath3Id, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.unknown, FormCollection fc = null)
        {
            return await base.Edit(entity, returnUrl, menuPathMainId, productId, productChildId, MenuPath1Id, MenuPath2Id, MenuPath3Id, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuEnum, fc);
        }
    }





}