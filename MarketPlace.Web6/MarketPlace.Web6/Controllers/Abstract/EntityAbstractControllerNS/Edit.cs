using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserModels;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity> : AbstractController where TEntity : class, ICommonWithId
    {

        // GET: Countries/Edit/5
        public virtual async Task<ActionResult> Edit(string id, string selectedId = "", string searchFor = "", string isandForSearch = "", string menuPathMainId = "", string productChildId = "", string productId = "", MenuENUM menuEnum = MenuENUM.unknown, string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
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
                    menuEnum,
                    sortBy,
                    print,
                    returnUrl,
                    ActionNameENUM.Edit);

                return Event_CreateViewAndSetupSelectList(parms);






            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Not Saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", new { id = id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = id, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });
            }
        }


        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(TEntity entity, string returnUrl, string menuPathMainId, string productId, string productChildId, string MenuPath1Id, string MenuPath2Id, string MenuPath3Id, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.unknown, FormCollection fc = null)
        {
            //var req = Request;
            //string fileNameOnly = Path.GetFileNameWithoutExtension(files[0].FileName);
            //string extention = Path.GetExtension(files[0].FileName);


            try
            {

                entity.IsNullThrowExceptionArgument("No Entity received!");

                LoadUserIntoEntity(entity);

                //get the Db Entity for this...
                TEntity dbEntity = Biz.Find(entity.Id);
                dbEntity.IsNullThrowException("Entity not found!");

                dbEntity.UpdatePropertiesDuringModify(entity);

                ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
                    entity,
                    httpMiscUploadedFiles,
                    httpSelfieUploads,
                    httpIdCardFrontUploads,
                    httpIdCardBackUploads,
                    httpPassportFrontUploads,
                    httpPassportVisaUploads,
                    httpLiscenseFrontUploads,
                    httpLiscenseBackUploads,
                    MenuENUM.unknown,
                    User.Identity.Name,
                    returnUrl);

                await Biz.UpdateAndSaveAsync(parm);

                if (returnUrl.IsNullOrWhiteSpace())
                    return RedirectToAction("Index", new { searchFor = searchFor, isandForSearch = isandForSearch, selectedId = entity.Id, returnUrl = returnUrl, menuEnum = menuEnum, sortBy = sortBy, print = print });

                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Not saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", new { id = entity.Id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = entity.Id, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuEnum = menuEnum, sortBy = sortBy, print = print });
            }
        }


    }
}