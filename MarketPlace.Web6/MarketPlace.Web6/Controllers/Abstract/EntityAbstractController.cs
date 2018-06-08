using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.Interface;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityAbstractController<TEntity> : AbstractController where TEntity : class, ICommonWithId
    {

        protected IBusinessLayer<TEntity> _icrudBiz;
        protected Type _tEntityType;
        UserBiz _userBiz;
        public EntityAbstractController(IBusinessLayer<TEntity> icrudUow, IErrorSet errorSet, UserBiz userbiz)
            : base(errorSet)
        {
            _icrudBiz = icrudUow;

            //_icrudBiz.UserNameBiz = UserName; //Passes the user name to the biz level.
            //_icrudBiz.UserIdBiz = UserId;//Passes the User Id to the biz level

            _tEntityType = typeof(TEntity);
            _userBiz = userbiz;
        }

        public BusinessLayer<TEntity> Biz
        {
            get
            {
                return (BusinessLayer<TEntity>)_icrudBiz;
            }
        }

        #region Various Interupts



        /// <summary>
        /// This method makes sure that both SetupSelectList and CreateViewEditAndCreateActions happen together and in the correct
        /// order
        /// </summary>
        /// <param name="iCommonWithId"></param>
        /// <returns></returns>
        public virtual ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            TEntity entity = (TEntity)parm.Entity;
            return View(entity);
        }

        #endregion

        #region Index

        // GET: Countries
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">This is used for the Menus. It brings back the selected Id.</param>
        /// <param name="searchFor"></param>
        /// <param name="selectedId"></param>
        /// <param name="menuPath1Id"></param>
        /// <param name="menuPath2Id"></param>
        /// <param name="menuPath3Id"></param>
        /// <param name="menuLevelEnum"></param>
        /// <param name="sortBy"></param>
        /// <param name="print"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Index(string id, string searchFor, string selectedId, MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, string menuPath1Id = "", string menuPath2Id = "", string menuPath3Id = "", string productId = "")
        {
            try
            {

                //load parameters
                TEntity dudEntity = Biz.Factory();
                string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);

                //todo note... the company name is missing. We may need it.
                ControllerIndexParams parms = new ControllerIndexParams(searchFor, selectedId, sortBy, menuLevelEnum, id, menuPath1Id, menuPath2Id, menuPath3Id, logoAddress, dudEntity, User.Identity.Name, productId);

                IndexListVM indexListVM = await indexEngine(parms);

                if (print)
                {
                    //return View("Print", await Print(parms));
                    return PrintPdf(indexListVM);
                    ;
                }


                if (!Request.IsAjaxRequest())
                {
                    return View(indexListVM);
                }

                //this is an Ajax Request.
                return PartialView("ViewControls/index/_IndexMiddlePart", indexListVM);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong while printing Index.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();


                return RedirectToAction("Index", "Home", null);

            }


        }

        protected async Task<IndexListVM> indexEngine(ControllerIndexParams parameters)
        {
            IndexListVM indexListVM = await Biz.IndexAsync(parameters);

            if (indexListVM.IsNull())
            {
                ErrorsGlobal.Add("No data received to make Index list", MethodBase.GetCurrentMethod());
                ErrorsGlobal.MemorySave();
            }

            return indexListVM;
        }


        public async Task<IndexListVM> Print(ControllerIndexParams parameters)
        {

            try
            {
                var indexListVM = await indexEngine(parameters);
                return indexListVM;
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Cannot print", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                throw new Exception(ErrorsGlobal.ToString());

            }

        }


        public ActionResult PrintPdf(IndexListVM indexListVM)
        {
            //Biz.PrintIndex(indexListVM);

            return File(Biz.PrintIndex(indexListVM), "application/pdf", indexListVM.DownloadFileName + ".pdf");

            //LanguageBiz bz = (LanguageBiz)Biz;

            //string downloadFileName = "invoice_" + DateTime.Now.Ticks.ToString() + ".pdf";
            //return File(bz.PrintInvoice(), "application/pdf", downloadFileName);
        }


        //public virtual void EventBeforeIndexView(ModelsClassLibrary.ViewModels.IndexListVM data)
        //{

        //}

        #endregion

        #region Create


        /// <summary>
        /// I needed to add this because I would like the return from FileDoc Create to be sorted by file number.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual RedirectToRouteResult Event_UpdateCreateRedicrectToAction(TEntity entity)
        {
            return RedirectToAction("Index", new { selectedId = entity.Id.ToString() });

        }
        // GET: Countries/Create
        public virtual ActionResult Create(MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown, string menuPath1Id = "", string menuPath2Id = "", string menuPath3Id = "", string productId = "")
        {
            TEntity entity = Biz.EntityFactoryForHttpGet();

            try
            {
                string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);
                ControllerIndexParams parm = new ControllerIndexParams("", "", SortOrderENUM.Item1_Asc, menuLevelEnum, entity.Id, menuPath1Id, menuPath2Id, menuPath3Id, logoAddress, (ICommonWithId)entity, User.Identity.Name, productId);

                //we want to get the previous menu because when we do a back to list, the Index will automatically advance the menu to the next.
                ViewBag.MenuLevelEnum = getPreviousMenuLevel(menuLevelEnum);

                return Event_CreateViewAndSetupSelectList(parm);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("'{0}' Something went wrong during creation.", ((ICommonWithId)entity).Name), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", new { selectedId = entity.Id.ToString() });
            }
        }

        private MenuLevelENUM getPreviousMenuLevel(MenuLevelENUM menuLevelEnum)
        {
            MenuModel mm = new MenuModel();
            mm.MenuLevelEnum = menuLevelEnum;
            return mm.GetPreviousMenuLevel();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(TEntity entity, HttpPostedFileBase[] httpMiscUploadedFiles = null, HttpPostedFileBase[] httpSelfieUploads = null, HttpPostedFileBase[] httpIdCardFrontUploads = null, HttpPostedFileBase[] httpIdCardBackUploads = null, HttpPostedFileBase[] httpPassportFrontUploads = null, HttpPostedFileBase[] httpPassportVisaUploads = null, HttpPostedFileBase[] httpLiscenseFrontUploads = null, HttpPostedFileBase[] httpLiscenseBackUploads = null, string menuPath1Id = "", string menuPath2Id = "", string menuPath3Id = "", string productId = "")
        {
            try
            {
                loadUserIntoEntity(entity);
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
                    MenuLevelENUM.unknown,
                    User.Identity.Name,
                    menuPath1Id,
                    menuPath2Id,
                    menuPath3Id,
                    productId);

                await Biz.CreateAndSaveAsync(parm);

                return Event_UpdateCreateRedicrectToAction(entity);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("'{0}' Not saved!", ((ICommonWithId)entity).Name), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", new { selectedId = entity.Id.ToString() });
            }
        }

        private void loadUserIntoEntity(TEntity entity)
        {
            IHasUser iuser = entity as IHasUser;

            if (iuser.IsNull())
                return;

            //is user loggerd in
            UserName.IsNullOrWhiteSpaceThrowException("User is not logged in");

            iuser.User = _userBiz.FindAll().FirstOrDefault(x => x.UserName.ToLower() == UserName.ToLower());

            if (iuser.User.IsNull())
            {
                ErrorsGlobal.Add("The User was not found.", MethodBase.GetCurrentMethod());
                throw new Exception(ErrorsGlobal.ToString());
            }

            iuser.UserId = iuser.User.Id;

        }
        //GetErrorsFromModelState();




        #endregion

        #region Edit
        // GET: Countries/Edit/5
        public virtual async Task<ActionResult> Edit(string id, string menuPath1Id = "", string menuPath2Id = "", string menuPath3Id = "", string productId = "", MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                TEntity entity = await Biz.FindAsync(id);
                if (entity == null)
                {
                    ErrorsGlobal.Add(string.Format("Not found!"), MethodBase.GetCurrentMethod());
                    return HttpNotFound();
                }
                //ViewBag.MenuLevelEnum = menuLevelEnum;
                string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);
                ControllerIndexParams parm = new ControllerIndexParams("", "", SortOrderENUM.Item1_Asc, menuLevelEnum, id, menuPath1Id, menuPath2Id, menuPath3Id, logoAddress, (ICommonWithId)entity, User.Identity.Name, productId);

                return Event_CreateViewAndSetupSelectList(parm);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Not Saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", new { selectedId = id.ToString() });
            }
        }


        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(TEntity entity, HttpPostedFileBase[] httpMiscUploadedFiles, HttpPostedFileBase[] httpSelfieUploads, HttpPostedFileBase[] httpIdCardFrontUploads, HttpPostedFileBase[] httpIdCardBackUploads, HttpPostedFileBase[] httpPassportFrontUploads, HttpPostedFileBase[] httpPassportVisaUploads, HttpPostedFileBase[] httpLiscenseFrontUploads, HttpPostedFileBase[] httpLiscenseBackUploads, FormCollection fc)
        {
            //var req = Request;
            //string fileNameOnly = Path.GetFileNameWithoutExtension(files[0].FileName);
            //string extention = Path.GetExtension(files[0].FileName);


            try
            {

                if (entity.IsNull())
                {

                    ErrorsGlobal.AddMessage("No item received.");
                }

                loadUserIntoEntity(entity);

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
                    MenuLevelENUM.unknown,
                    User.Identity.Name ?? "",
                    "", "", "", "");

                await Biz.UpdateAndSaveAsync(parm);

                return RedirectToAction("Index", new { selectedId = entity.Id.ToString() });
            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Not saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", new { selectedId = entity.Id.ToString() });
            }
        }

        #endregion

        #region Details

        // GET: Countries/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                TEntity entity = await Biz.FindAsync(id ?? string.Empty);

                if (entity == null)
                {
                    return HttpNotFound();
                }
                MenuLevelENUM menuLevelEnum = MenuLevelENUM.unknown; //dummy entry
                string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);

                ControllerIndexParams parm = new ControllerIndexParams("", "", SortOrderENUM.Item1_Asc, menuLevelEnum, id, "", "", "", logoAddress, (ICommonWithId)entity, User.Identity.Name, "");

                return Event_CreateViewAndSetupSelectList(parm);


            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Not found!", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", new { selectedId = id.ToString() });
            }
        }


        #endregion

        #region Delete
        // GET: Countries/Delete/5
        public async Task<ActionResult> Delete(string id, string returnUrl)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                TEntity entity = await Biz.FindAsync(id);

                if (entity.IsNull())
                {
                    return HttpNotFound();
                }

                //this is where the return URL is added.
                //this is used when you want to return somewhere else other than the Index
                //It will be passed on to a hidden field is Delete and then passed on
                //to DeleteConfirmed via the hidden field
                entity.ReturnUrl = returnUrl;
                string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);

                ControllerIndexParams parm = new ControllerIndexParams("", "", SortOrderENUM.Item1_Asc, MenuLevelENUM.unknown, id, "", "", "", logoAddress, (ICommonWithId)entity, User.Identity.Name, "");

                return Event_CreateViewAndSetupSelectList(parm);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Not found!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();

                if (!returnUrl.IsNullOrWhiteSpace())
                    return Redirect(returnUrl);

                return RedirectToAction("Index", new { selectedId = id.ToString() });

            }
        }


        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id, string returnUrl)
        {
            try
            {
                bool success = await Biz.DeleteAsync(id);
                if (success)
                {
                    Event_AfterDeleting(id);
                }

                if (!returnUrl.IsNullOrWhiteSpace())
                    return Redirect(returnUrl);

                return RedirectToAction("Index");


            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Not found!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Delete", new { id = id });

            }
        }


        /// <summary>
        /// This event fires right after a SUCCESSFUL delete. If the delete is not successful, this event will not fire.
        /// </summary>
        /// <param name="id"></param>
        public virtual void Event_AfterDeleting(string id)
        {
            //throw new NotImplementedException();
        }

        #endregion

        #region DeleteAll
        // GET: Countries/Delete/5
        public virtual ActionResult DeleteAll()
        {
            var entity = Biz.Factory();
            ViewBag.Heading = entity.ClassNamePlural.ToTitleSentance();

            return View();

        }


        // POST: Countries/Delete/5
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> DeleteAllConfirmed()
        {
            try
            {
                await Biz.DeleteActuallyAllAndSaveAsync();
                return RedirectToAction("Index");


            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Unable to delete all {0}", typeof(TEntity).Name),
                    MethodBase.GetCurrentMethod(),
                    e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("DeleteAll");

            }
        }

        #endregion

        #region Initialize
        public ActionResult InitializeDb()
        {
            try
            {
                //Biz.SaveAfterEveryAddition(IsSavingAfterEveryAddition);
                Biz.InitializationData();
                ErrorsGlobal.Add(string.Format("*** {0} Initialized", typeof(TEntity).Name), "");

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Unable to initialize", MethodBase.GetCurrentMethod(), e);
            }

            ErrorsGlobal.MemorySave();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// If true then items will be saved after every addition during initialization. This kind of initilization
        /// is required by some items such as fileDocs which generate the next file number.
        /// </summary>
        //public bool IsSavingAfterEveryAddition { get; set; }
        #endregion
    }
}