using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity> : AbstractController where TEntity : class, ICommonWithId
    {

        // GET: Countries/Edit/5
        public virtual async Task<ActionResult> Edit(string id, string selectedId = "", string searchFor = "", string isandForSearch = "", MenuENUM menuEnum = MenuENUM.EditDefault, string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, bool isMenu = false)
        {
            TEntity entity = _icrudBiz.Factory() as TEntity;
            try
            {
                id.IsNullThrowExceptionArgument("Id not received. Bad Request");

                entity = await Biz.FindAsync(id) as TEntity;
                entity.IsNullThrowException("Entity not found.");

                ControllerIndexParams parms = MakeControlParameters(
                    id,
                    searchFor,
                    isandForSearch,
                    selectedId,
                    entity,
                    entity,
                    BreadCrumbManager,
                    UserId,
                    UserName,
                    isMenu,
                    menuEnum,
                    sortBy,
                    print,
                    ActionNameENUM.Edit);

                InitializeMenuManager(parms);
                return Event_CreateViewAndSetupSelectList(parms);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Not Saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", new { id = id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = id, returnUrl = returnUrl, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });
            }
        }


        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(TEntity entity, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.EditDefault, bool isMenu = false, FormCollection fc = null)
        {
            //var req = Request;
            //string fileNameOnly = Path.GetFileNameWithoutExtension(files[0].FileName);
            //string extention = Path.GetExtension(files[0].FileName);

            string dbEntiyId = "";
            try
            {

                entity.IsNullThrowExceptionArgument("No Entity received!");
                dbEntiyId = entity.Id;
                //LoadUserIntoEntity(entity);

                //get the Db Entity for this...
                TEntity dbEntity = Biz.Find(dbEntiyId);
                dbEntity.IsNullThrowException("Entity not found!");

                //we dont need entity after this
                //remember to add the UpdatePropertiesDuringModify when you are adding new stuff.
                //You forget this.
                dbEntity.UpdatePropertiesDuringModify(entity);

                ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
                    dbEntity,
                    httpMiscUploadedFiles,
                    httpSelfieUploads,
                    httpIdCardFrontUploads,
                    httpIdCardBackUploads,
                    httpPassportFrontUploads,
                    httpPassportVisaUploads,
                    httpLiscenseFrontUploads,
                    httpLiscenseBackUploads,
                    MenuENUM.EditDefault,
                    User.Identity.Name,
                    returnUrl);

                await Biz.UpdateAndSaveAsync(parm);
                //The returnUrl is coming null because we have not implemented BreadCrumbs.
                //After we implement BreadCrumbs, it will go to the correct Controller, Now with
                //this not working when I save MenuPath1, it goes to the MenuPath1 Controller whereas it
                //should be going to the MenuPathMain Controller
                //if (returnUrl.IsNullOrWhiteSpace())
                //    return RedirectToAction("Index", new { searchFor = searchFor, isandForSearch = isandForSearch, selectedId = dbEntity.Id, returnUrl = returnUrl, menuEnum = menuEnum, sortBy = sortBy, print = print });
                if (BreadCrumbManager.Url_Curr != BreadCrumbManager.Url_CurrMinusOne)
                    return Redirect(BreadCrumbManager.Url_CurrMinusOne);

                InitializeMenuManager(parm);

                return RedirectToAction("Index", new { selectedId = selectedId, menuEnum = menuEnum });
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Not saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return Redirect(BreadCrumbManager.Url_CurrMinusTwo);
                //return Redirect(BreadCrumbManager.Url_CurrMinusOne);
            }
        }


    }
}