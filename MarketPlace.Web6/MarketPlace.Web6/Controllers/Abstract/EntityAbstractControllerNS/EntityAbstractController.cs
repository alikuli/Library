using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.Interface;
using UserModels;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity> : AbstractController where TEntity : class, ICommonWithId
    {

        protected IBusinessLayer<TEntity> _icrudBiz;
        protected Type _tEntityType;
        private UserBiz _userBiz;
        public EntityAbstractController(IBusinessLayer<TEntity> icrudUow, IErrorSet errorSet, UserBiz userbiz, BreadCrumbManager breadCrumbManager)
            : base(errorSet, userbiz, breadCrumbManager)
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

        //#region Various Interupts



        ///// <summary>
        ///// This method makes sure that both SetupSelectList and CreateViewEditAndCreateActions happen together and in the correct
        ///// order
        ///// </summary>
        ///// <param name="iCommonWithId"></param>
        ///// <returns></returns>
        //public virtual ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        //{
        //    if (parm.Entity.IsNull())
        //        return View(Biz.EntityFactoryForHttpGet());

        //    TEntity entity = (TEntity)parm.Entity;
        //    return View(entity);
        //}

        //#endregion

        //#region Index

        //// GET: Countries
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id">This is used for the Menus. It brings back the selected Id.</param>
        ///// <param name="searchFor"></param>
        ///// <param name="selectedId"></param>
        ///// <param name="menuPath1Id"></param>
        ///// <param name="menuPath2Id"></param>
        ///// <param name="menuPath3Id"></param>
        ///// <param name="menuEnum"></param>
        ///// <param name="sortBy"></param>
        ///// <param name="print"></param>
        ///// <returns></returns>
        //public virtual async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, MenuENUM menuEnum = MenuENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        //{
        //    try
        //    {
        //        TEntity dudEntity = Biz.Factory() as TEntity;
        //        //ApplicationUser user = GetApplicationUser();

        //        //bool isUserAdmin = IsUserAdmin(user);

        //        //string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);


        //        ControllerIndexParams parms = MakeControlParameters(
        //            id,
        //            searchFor,
        //            isandForSearch,
        //            selectedId,
        //            null,
        //            dudEntity as ICommonWithId,
        //            menuEnum,
        //            sortBy,
        //            print,
        //            returnUrl,
        //            ActionNameENUM.Index);

        //        IndexListVM indexListVM = await indexEngine(parms);

        //        if (print)
        //        {
        //            //return View("Print", await Print(parms));
        //            return PrintPdf(indexListVM);
        //        }


        //        if (!Request.IsAjaxRequest())
        //        {
        //            return View(indexListVM);
        //        }

        //        //this is an Ajax Request.
        //        return PartialView("ViewControls/index/_IndexMiddlePart", indexListVM);

        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Something went wrong while printing Index.", MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();


        //        return RedirectToAction("Index", "Home", null);

        //    }


        //}


        //protected async Task<IndexListVM> indexEngine(ControllerIndexParams parameters)
        //{
        //    IndexListVM indexListVM = await Biz.IndexAsync(parameters);

        //    if (indexListVM.IsNull())
        //    {
        //        ErrorsGlobal.Add("No data received to make Index list", MethodBase.GetCurrentMethod());
        //        ErrorsGlobal.MemorySave();
        //    }

        //    return indexListVM;
        //}


        //public async Task<IndexListVM> Print(ControllerIndexParams parameters)
        //{

        //    try
        //    {
        //        var indexListVM = await indexEngine(parameters);
        //        return indexListVM;
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Cannot print", MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        throw new Exception(ErrorsGlobal.ToString());

        //    }

        //}


        //public ActionResult PrintPdf(IndexListVM indexListVM)
        //{
        //    //Biz.PrintIndex(indexListVM);

        //    return File(Biz.PrintIndex(indexListVM), "application/pdf", indexListVM.DownloadFileName + ".pdf");

        //    //LanguageBiz bz = (LanguageBiz)Biz;

        //    //string downloadFileName = "invoice_" + DateTime.Now.Ticks.ToString() + ".pdf";
        //    //return File(bz.PrintInvoice(), "application/pdf", downloadFileName);
        //}


        ////public virtual void EventBeforeIndexView(ModelsClassLibrary.ViewModels.IndexListVM data)
        ////{

        ////}

        //#endregion

        //#region Create


        //// GET: Countries/Create
        //public virtual ActionResult Create(string isandForSearch, MenuENUM menuEnum = MenuENUM.unknown, string productChildId = "", string menuPathMainId = "", string productId = "", string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false)
        //{
        //    //for product this needs to create the correct vm.

        //    TEntity dudEntity = Biz.EntityFactoryForHttpGet() as TEntity;

        //    try
        //    {
        //        ApplicationUser user = GetApplicationUser();


        //        bool isUserAdmin = IsUserAdmin(user);

        //        string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);

        //        ControllerIndexParams parms = MakeControlParameters(
        //            "",
        //            searchFor,
        //            isandForSearch,
        //            selectedId,
        //            null,
        //            dudEntity,
        //            menuEnum,
        //            sortBy,
        //            print,
        //            returnUrl,
        //            ActionNameENUM.Create);



        //        return Event_CreateViewAndSetupSelectList(parms);

        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add(string.Format("'{0}' Something went wrong during creation.", ((ICommonWithId)dudEntity).Name), MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();

        //        return RedirectToAction("Index", new { id = "", searchFor = searchFor, isandForSearch = isandForSearch, selectedId = selectedId, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });
        //    }
        //}



        //// POST: Countries/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public virtual async Task<ActionResult> Create(TEntity entity, string returnUrl, string menuPathMainId, string productId, string productChildId, string MenuPath1Id, string MenuPath2Id, string MenuPath3Id, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.unknown, FormCollection fc = null)
        //{
        //    try
        //    {
        //        //entity = ifProductVmThenMakeEntityIntoProduct(entity);

        //        LoadUserIntoEntity(entity);

        //        ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
        //            entity,
        //            httpMiscUploadedFiles,
        //            httpSelfieUploads,
        //            httpIdCardFrontUploads,
        //            httpIdCardBackUploads,
        //            httpPassportFrontUploads,
        //            httpPassportVisaUploads,
        //            httpLiscenseFrontUploads,
        //            httpLiscenseBackUploads,
        //            MenuENUM.unknown,
        //            User.Identity.Name,
        //            menuPathMainId,
        //            productId,
        //            productChildId,
        //            returnUrl);

        //        await Biz.CreateAndSaveAsync(parm);

        //        if (returnUrl.IsNullOrWhiteSpace())
        //        {
        //            return Event_UpdateCreateRedicrectToAction(parm);
        //        }
        //        return Redirect(returnUrl);

        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add(string.Format("'{0}' Not saved!", ((ICommonWithId)entity).Name), MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Index", new { id = entity.Id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = entity.Id, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });
        //    }
        //}

        ////private static TEntity ifProductVmThenMakeEntityIntoProduct(TEntity entity)
        ////{

        ////    IProductVM ipvm = entity as IProductVM;
        ////    if (ipvm.IsNull())
        ////        return entity; //do nothing

        ////    entity.SelfErrorCheck();

        ////    Product product = entity as Product;

        ////    if (!ipvm.IsNull()) //this is a product and a VM has returned
        ////    {
        ////        product.Name = ipvm.MakeName();
        ////        ipvm.SaveNameFields();
        ////        //Now move the VM to a Product Class
        ////        entity = product as TEntity;
        ////    }
        ////    return entity;
        ////}



        ///// <summary>
        ///// I needed to add this because I would like the return from FileDoc Create to be sorted by file number.
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public virtual RedirectToRouteResult Event_UpdateCreateRedicrectToAction(ControllerCreateEditParameter parm)
        //{
        //    return RedirectToAction("Index", new { id = parm.Entity.Id, selectedId = parm.Entity.Id, returnUrl = parm.Menu.ReturnUrl, menuEnum = parm.Menu.MenuEnum });

        //}



        ///// <summary>
        ///// If this is a product and it is coming from the menu, then load menupath1 into the product because the ProductVM need it.
        ///// </summary>
        ///// <param name="menuPath1Id"></param>
        ///// <param name="entity"></param>
        ////private static void ifProductThenLoadMenuPath1(string menuPath1Id, TEntity entity, MenuLevelENUM menuLevelEnum)
        ////{
        ////    bool isMenuPathNull = menuPath1Id.IsNullOrWhiteSpace();
        ////    if (isMenuPathNull)
        ////        return;

        ////    IProduct product = entity as IProduct;
        ////    bool isProductNull = product.IsNull();

        ////    if (isProductNull)
        ////        return;

        ////    if (menuLevelEnum == MenuLevelENUM.unknown)
        ////        return;

        ////    if (menuLevelEnum == MenuLevelENUM.Level_1)
        ////        return;

        ////    //If it is a product, and the menu level is 2, then feed the menu levels in.
        ////    product.Menu.MenuPath1Id = menuPath1Id;

        ////}

        ////private MenuLevelENUM getPreviousMenuLevel(MenuLevelENUM menuLevelEnum)
        ////{
        ////    MenuModel mm = new MenuModel();
        ////    mm.MenuLevelEnum = menuLevelEnum;
        ////    return mm.GetPreviousMenuLevel();
        ////}

        ////GetErrorsFromModelState();

        //#endregion

        //#region Edit
        //// GET: Countries/Edit/5
        //public virtual async Task<ActionResult> Edit(string id, string selectedId = "", string searchFor = "", string isandForSearch = "", string menuPathMainId = "", string productChildId = "", string productId = "", MenuENUM menuEnum = MenuENUM.unknown, string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        //{
        //    TEntity entity = _icrudBiz.Factory() as TEntity;
        //    try
        //    {
        //        id.IsNullThrowExceptionArgument("Id not received. Bad Request");

        //        entity = await Biz.FindAsync(id) as TEntity;
        //        entity.IsNullThrowException("Entity not found.");

        //        ControllerIndexParams parms = MakeControlParameters(
        //            id,
        //            searchFor,
        //            isandForSearch,
        //            selectedId,
        //            entity,
        //            entity,
        //            menuEnum,
        //            sortBy,
        //            print,
        //            returnUrl,
        //            ActionNameENUM.Edit);

        //        return Event_CreateViewAndSetupSelectList(parms);






        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add(string.Format("Not Saved!"), MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Index", new { id = id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = id, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });
        //    }
        //}


        //// POST: Countries/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public virtual async Task<ActionResult> Edit(TEntity entity, string returnUrl, string menuPathMainId, string productId, string productChildId, string MenuPath1Id, string MenuPath2Id, string MenuPath3Id, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.unknown, FormCollection fc = null)
        //{
        //    //var req = Request;
        //    //string fileNameOnly = Path.GetFileNameWithoutExtension(files[0].FileName);
        //    //string extention = Path.GetExtension(files[0].FileName);


        //    try
        //    {

        //        entity.IsNullThrowExceptionArgument("No Entity received!");

        //        LoadUserIntoEntity(entity);

        //        //get the Db Entity for this...
        //        TEntity dbEntity = Biz.Find(entity.Id);
        //        dbEntity.IsNullThrowException("Entity not found!");

        //        dbEntity.UpdatePropertiesDuringModify(entity);

        //        ControllerCreateEditParameter parm = new ControllerCreateEditParameter(
        //            entity,
        //            httpMiscUploadedFiles,
        //            httpSelfieUploads,
        //            httpIdCardFrontUploads,
        //            httpIdCardBackUploads,
        //            httpPassportFrontUploads,
        //            httpPassportVisaUploads,
        //            httpLiscenseFrontUploads,
        //            httpLiscenseBackUploads,
        //            MenuENUM.unknown,
        //            User.Identity.Name,
        //            menuPathMainId,
        //            productId,
        //            productChildId,
        //            returnUrl);

        //        await Biz.UpdateAndSaveAsync(parm);

        //        if (returnUrl.IsNullOrWhiteSpace())
        //            return RedirectToAction("Index", new { id = entity.Id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = entity.Id, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuEnum = menuEnum, sortBy = sortBy, print = print });

        //        return Redirect(returnUrl);
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add(string.Format("Not saved!"), MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Index", new { id = entity.Id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = entity.Id, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuEnum = menuEnum, sortBy = sortBy, print = print });
        //    }
        //}

        //#endregion

        //#region Details

        //// GET: Countries/Details/5
        //public async Task<ActionResult> Details(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, string productId, string menuPathMainId, string productChildId, MenuENUM menuEnum = MenuENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    try
        //    {
        //        TEntity entity = await Biz.FindAsync(id ?? string.Empty);

        //        if (entity == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);

        //        ApplicationUser user = _userBiz.FindByUserName_UserManager(User.Identity.Name);
        //        bool isUserAdmin = _userBiz.IsUserAdmin(user);

        //        //ControllerIndexParams parm = new ControllerIndexParams("", "", SortOrderENUM.Item1_Asc, menuLevelEnum, id, "", "", "", logoAddress, (ICommonWithId)entity, user, "", isUserAdmin, returnUrl, isandForSearchDummy);

        //        ControllerIndexParams parms =
        //            MakeControlParameters(
        //                entity.Id,
        //                searchFor,
        //                isandForSearch,
        //                selectedId,
        //                entity,
        //                entity,
        //                menuEnum,
        //                sortBy,
        //                print,
        //                returnUrl,
        //                ActionNameENUM.Detail);

        //        return Event_CreateViewAndSetupSelectList(parms);


        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Not found!", MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Index", new { id = id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = id, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuEnum = menuEnum, sortBy = sortBy, print = print });
        //    }
        //}


        //#endregion

        //#region Delete
        //// GET: Countries/Delete/5
        //public async Task<ActionResult> Delete(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, string productId, string menuPathMainId, string productChildId, MenuENUM menuEnum = MenuENUM.unknown, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        //{

        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    try
        //    {
        //        TEntity entity = await Biz.FindAsync(id);

        //        if (entity.IsNull())
        //        {
        //            return HttpNotFound();
        //        }

        //        //this is where the return URL is added.
        //        //this is used when you want to return somewhere else other than the Index
        //        //It will be passed on to a hidden field is Delete and then passed on
        //        //to DeleteConfirmed via the hidden field
        //        entity.ReturnUrl = returnUrl;
        //        string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);
        //        ApplicationUser user = _userBiz.FindByUserName_UserManager(User.Identity.Name);
        //        bool isUserAdmin = _userBiz.IsUserAdmin(user);

        //        ControllerIndexParams parms = MakeControlParameters(
        //            id,
        //            searchFor,
        //            isandForSearch,
        //            selectedId,
        //            entity,
        //            entity,
        //            menuEnum,
        //            sortBy,
        //            print,
        //            returnUrl,
        //            ActionNameENUM.Delete);

        //        return Event_CreateViewAndSetupSelectList(parms);

        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add(string.Format("Not found!"), MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();

        //        if (!returnUrl.IsNullOrWhiteSpace())
        //            return Redirect(returnUrl);

        //        return RedirectToAction("Index", new { selectedId = id.ToString() });

        //    }
        //}


        //// POST: Countries/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(string id, string returnUrl)
        //{
        //    try
        //    {
        //        bool success = await Biz.DeleteAsync(id);
        //        if (success)
        //        {
        //            Event_AfterDeleting(id);
        //        }

        //        if (!returnUrl.IsNullOrWhiteSpace())
        //            return Redirect(returnUrl);

        //        return RedirectToAction("Index");


        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add(string.Format("Not found!"), MethodBase.GetCurrentMethod(), e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("Delete", new { id = id });

        //    }
        //}


        ///// <summary>
        ///// This event fires right after a SUCCESSFUL delete. If the delete is not successful, this event will not fire.
        ///// </summary>
        ///// <param name="id"></param>
        //public virtual void Event_AfterDeleting(string id)
        //{
        //    //throw new NotImplementedException();
        //}

        //#endregion

        //#region DeleteAll
        //// GET: Countries/Delete/5
        //public virtual ActionResult DeleteAll()
        //{
        //    var entity = Biz.Factory();
        //    ViewBag.Heading = entity.ClassNamePlural.ToTitleSentance();

        //    return View();

        //}


        //// POST: Countries/Delete/5
        //[HttpPost, ActionName("DeleteAll")]
        //[ValidateAntiForgeryToken]
        //public virtual async Task<ActionResult> DeleteAllConfirmed()
        //{
        //    try
        //    {
        //        await Biz.DeleteActuallyAllAndSaveAsync();
        //        return RedirectToAction("Index");


        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add(string.Format("Unable to delete all {0}", typeof(TEntity).Name),
        //            MethodBase.GetCurrentMethod(),
        //            e);
        //        ErrorsGlobal.MemorySave();
        //        return RedirectToAction("DeleteAll");

        //    }
        //}

        //#endregion

        //#region Initialize
        //public ActionResult InitializeDb()
        //{
        //    try
        //    {
        //        //Biz.SaveAfterEveryAddition(IsSavingAfterEveryAddition);
        //        Biz.InitializationData();
        //        ErrorsGlobal.Add(string.Format("*** {0} Initialized", typeof(TEntity).Name), "");

        //    }
        //    catch (Exception e)
        //    {
        //        ErrorsGlobal.Add("Unable to initialize", MethodBase.GetCurrentMethod(), e);
        //    }

        //    ErrorsGlobal.MemorySave();
        //    return RedirectToAction("Index", "Home", null);

        //}

        ///// <summary>
        ///// If true then items will be saved after every addition during initialization. This kind of initilization
        ///// is required by some items such as fileDocs which generate the next file number.
        ///// </summary>
        ////public bool IsSavingAfterEveryAddition { get; set; }
        //#endregion
    }
}