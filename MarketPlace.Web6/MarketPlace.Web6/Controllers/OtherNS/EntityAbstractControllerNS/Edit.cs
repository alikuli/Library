using AliKuli.Extentions;
using EnumLibrary.EnumNS;
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
    public partial class EntityAbstractController<TEntity>
    {

        // GET: Countries/Edit/5
        //https://stackoverflow.com/questions/12948156/asp-net-mvc-how-to-disable-automatic-caching-option
        //[OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration
        //[NoCache]
        //[OutputCache(Duration = 0, Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]

        public virtual async Task<ActionResult> Edit(string id, string selectedId = "", string searchFor = "", string isandForSearch = "", MenuENUM menuEnum = MenuENUM.EditDefault, string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, bool isMenu = false, string menuPathMainId = "", string productId = "")
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
                    isMenu,
                    menuEnum,
                    sortBy,
                    print,
                    ActionNameENUM.Edit,
                    productId,
                    returnUrl);

                Biz.InitializeMenuManagerForEntity(parms);
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
        //https://stackoverflow.com/questions/12948156/asp-net-mvc-how-to-disable-automatic-caching-option
        //[OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)] // will be applied to all actions in MyController, unless those actions override with their own decoration
        //[NoCache]
        //[OutputCache(Duration = 0, Location = System.Web.UI.OutputCacheLocation.None, NoStore = true)]

        public virtual async Task<ActionResult> Edit(TEntity entity, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.EditDefault, bool isMenu = false, FormCollection fc = null)
        {
            //var req = Request;
            //string fileNameOnly = Path.GetFileNameWithoutExtension(files[0].FileName);
            //string extention = Path.GetExtension(files[0].FileName);
            Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.

            string dbEntiyId = "";
            try
            {

                entity.IsNullThrowExceptionArgument("No Entity received!");
                dbEntiyId = entity.Id;

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
                    UserName,
                    UserId,
                    returnUrl);

                //I had to make this because sometimes I cannot use a certain required biz
                //because it causes a recursive error... i.e. the biz being called by itself.
                //I can fix up the parameter here before it goes in
                Event_BeforeSaveInCreateAndEdit(parm);

                await Biz.UpdateAndSaveAsync(parm);
                Biz.InitializeMenuManager(parm);

                if (returnUrl.IsNullOrWhiteSpace())
                    return RedirectToAction("Index", new { selectedId = selectedId, menuEnum = menuEnum });

                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Not saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Home");
            }
        }



    }
}