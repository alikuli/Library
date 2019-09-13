using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// Use the button to send info about a button where name of the button will be button and the value will be the info.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity>
    {

        // GET: Countries/Edit/5
        //https://stackoverflow.com/questions/12948156/asp-net-mvc-how-to-disable-automatic-caching-option
        //[OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration
        //[NoCache]
        //[OutputCache(Duration = 0, Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]

        public virtual async Task<ActionResult> Edit(string id, string selectedId = "", string searchFor = "", string isandForSearch = "", MenuENUM menuEnum = MenuENUM.EditDefault, string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, bool isMenu = false, string menuPathMainId = "", string productId = "", BuySellDocumentTypeENUM buySellDocumentTypeEnum = BuySellDocumentTypeENUM.Unknown, BuySellDocStateENUM buySellDocStateEnum = BuySellDocStateENUM.Unknown, string button = "")
        {
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.

            TEntity entity = _icrudBiz.Factory() as TEntity;
            try
            {
                id.IsNullThrowExceptionArgument("Id not received. Bad Request");

                entity = await Biz.FindAsync(id) as TEntity;
                entity.IsNullThrowException("Entity not found.");
                //Biz.Detach(entity);
                //entity = await Biz.FindAsync(id) as TEntity;
                //entity.IsNullThrowException("Entity not found.");

                ControllerIndexParams parms = MakeControlParameters(
                    id,
                    menuPathMainId,
                    searchFor,
                    isandForSearch,
                    selectedId,
                    entity,
                    entity,
                    BreadCrumbManager,
                    UserId,
                    UserName,
                    productId,
                    returnUrl,
                    isMenu,
                    button,
                    menuEnum,
                    sortBy,
                    print,
                    ActionNameENUM.Edit,
                    buySellDocumentTypeEnum,
                    buySellDocStateEnum);

                Biz.InitializeMenuManagerForEntity(parms);

                return Event_Edit_ViewAndSetupSelectList_GET(parms);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Not Saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();

                if (returnUrl.IsNullOrWhiteSpace())
                    return RedirectToAction("Index", new { id = id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = id, returnUrl = returnUrl, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });

                return Redirect(returnUrl);
            }
        }

        public virtual ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            if (parm.Entity.IsNull())
                return View(Biz.FactoryForHttpGet());

            TEntity entity = (TEntity)parm.Entity;

            return View(entity);
        }


        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //https://stackoverflow.com/questions/12948156/asp-net-mvc-how-to-disable-automatic-caching-option
        //[OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration
        //[NoCache]
        //[OutputCache(Duration = 0, Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]

        public virtual async Task<ActionResult> Edit(TEntity entity, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.EditDefault, bool isMenu = false, string button = "", FormCollection fc = null)
        {
            //var req = Request;
            //string fileNameOnly = Path.GetFileNameWithoutExtension(files[0].FileName);
            //string extention = Path.GetExtension(files[0].FileName);
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.

            string dbEntiyId = "";
            ControllerCreateEditParameter parm = new ControllerCreateEditParameter();
            try
            {

                entity.IsNullThrowExceptionArgument("No Entity received!");
                dbEntiyId = entity.Id;

                //get the Db Entity for this...
                TEntity dbEntity = Biz.Find(dbEntiyId);
                dbEntity.IsNullThrowException("Entity not found!");
                //Event_Edit_Update_The_entity(dbEntity, entity);

                //we dont need entity after this
                //remember to add the UpdatePropertiesDuringModify when you are adding new stuff.
                //You forget this.
                dbEntity.UpdatePropertiesDuringModify(entity);

                dbEntity.IsEditing = true;
                entity.IsEditing = true;

                GlobalObject globalObject = ViewBag.GlobalObject as GlobalObject;

                parm = new ControllerCreateEditParameter(
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
                    UserName,
                    UserId,
                    returnUrl,
                    globalObject,
                    button);

                //I had to make this because sometimes I cannot use a certain required biz
                //because it causes a recursive error... i.e. the biz being called by itself.
                //I can fix up the parameter here before it goes in
                //Event_BeforeSaveInCreateAndEdit(parm);
                Event_Before_Edit_UpdateAndSaveAsync(parm);
                await Biz.UpdateAndSaveAsync(parm);
                Biz.InitializeMenuManager(parm);

                Event_AfterSaveInEdit_Post(parm);

                if (parm.ReturnUrl.IsNullOrWhiteSpace())
                    return RedirectToAction("Index", new { selectedId = selectedId, menuEnum = menuEnum });

            }
            catch (Exception e)
            {
                Event_Edit_InError_Post(parm);
                ErrorsGlobal.Add(string.Format("Not saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                if (parm.ReturnUrl.IsNullOrWhiteSpace())
                    return RedirectToAction("Index", "Home");
            }

            return Redirect(parm.ReturnUrl);
        }

        public virtual void Event_Before_Edit_UpdateAndSaveAsync(ControllerCreateEditParameter parm)
        {
        }




        /// <summary>
        /// I created this because whenI move the entity to ICommonWithId, I lose
        /// the classes.
        /// </summary>
        /// <param name="dbEntity"></param>
        public virtual void Event_Edit_Update_The_entity(TEntity dbEntity, TEntity inComingEntity)
        {
            //do nothing

        }

        /// <summary>
        /// this event occours in Edit Post in the error. this can be used to change there return URL of the Error from the Index
        /// </summary>
        /// <param name="parm"></param>
        public virtual void Event_Edit_InError_Post(ControllerCreateEditParameter parm)
        {
            //throw new NotImplementedException();
        }

        public virtual void Event_AfterSaveInEdit_Post(ControllerCreateEditParameter parm)
        {
            //do nothing
        }




    }
}