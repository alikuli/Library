using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers.Abstract
{
    /// <summary>
    /// This needs to know which Uow to call. It has to be hard pr
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial class EntityAbstractController<TEntity>
    {




        // GET: Countries/Create
        public virtual ActionResult Create(string isandForSearch, MenuENUM menuEnum = MenuENUM.CreateDefault, string productChildId = "", string menuPathMainId = "", string productId = "", string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, bool isMenu = false, string parentId = "", BuySellDocumentTypeENUM buySellDocumentTypeEnum = BuySellDocumentTypeENUM.Unknown, BuySellDocStateENUM buySellDocStateEnum = BuySellDocStateENUM.Unknown)
        {

            TEntity dudEntity = null;

            try
            {
                dudEntity = Biz.FactoryForHttpGet() as TEntity;
                string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);
                string buySellStatementType = "";

                //string userPersonId = "";
                //string productChildPersonId = "";
                
                ControllerIndexParams parms = MakeControlParameters(
                    "",
                    menuPathMainId,
                    searchFor,
                    isandForSearch,
                    selectedId,
                    dudEntity,
                    dudEntity,
                    BreadCrumbManager,
                    UserId,
                    UserName,
                    productId,
                    returnUrl,
                    isMenu,            
                    menuEnum,
                    sortBy,
                    print,
                    ActionNameENUM.Create, 
                    buySellDocumentTypeEnum,
                    buySellDocStateEnum);


                Biz.InitializeMenuManagerForEntity(parms);
                if (!returnUrl.IsNullOrWhiteSpace())
                {
                    parms.Entity.MenuManager.ReturnUrl = returnUrl;

                }

                AddParentIdIfChild(parms, parentId);

                //This is used in the view of Product and ProductChild
                //It causes the features to display as editable during create
                // and not during Edit or any other operation.
                parms.Entity.MenuManager.IsCreate = true;
                return Event_CreateViewAndSetupSelectList(parms);

            }
            catch (Exception e)
            {

                string nameOFDudEntity = "";
                if (!dudEntity.IsNull())
                {
                    nameOFDudEntity = dudEntity.Name;
                }

                ErrorsGlobal.Add(string.Format("'{0}' Something went wrong during creation.", nameOFDudEntity), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();

                return RedirectToAction("Index", new { id = "", searchFor = searchFor, isandForSearch = isandForSearch, selectedId = selectedId, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });
            }
        }

        public virtual void AddParentIdIfChild(ControllerIndexParams parms, string parentId)
        {
            //Use this to send the parentId into the HttpCreate. When you call the parent from its create button
            //make sure you send the Id of the parent in a field named parentId!
        }



        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(TEntity entity, string returnUrl, HttpPostedFileBase[] httpMiscUploadedFiles = null, HttpPostedFileBase[] httpSelfieUploads = null, HttpPostedFileBase[] httpIdCardFrontUploads = null, HttpPostedFileBase[] httpIdCardBackUploads = null, HttpPostedFileBase[] httpPassportFrontUploads = null, HttpPostedFileBase[] httpPassportVisaUploads = null, HttpPostedFileBase[] httpLiscenseFrontUploads = null, HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.CreateDefault, FormCollection fc = null)
        {
            try
            {
                //entity = ifProductVmThenMakeEntityIntoProduct(entity);
                //LoadUserIntoEntity(entity);


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
                    MenuENUM.CreateDefault,
                    UserName,
                    UserId,
                    returnUrl);


                Biz.InitializeMenuManager(parm);


                //I had to make this because sometimes I cannot use a certain required biz
                //because it causes a recursive error... i.e. the biz being called by itself.
                //I can fix up the parameter here before it goes in
                //Also use it in MenuPath1,2,3 to fix the returnUrl in the entity
                Event_BeforeSaveInCreateAndEdit(parm);
                await Biz.CreateAndSaveAsync(parm);
                Event_AfterSaveInCreate(parm);
                //if (returnUrl.IsNullOrWhiteSpace())
                //    return RedirectFromCreateHttpPostTo(BreadCrumbManager, parm);

                return Redirect(parm.ReturnUrl);
            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("'{0}' Not saved!", ((ICommonWithId)entity).Name), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();

                return RedirectToAction("Index", new { id = entity.Id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = entity.Id, returnUrl = returnUrl, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });

            }
        }

        public virtual void Event_AfterSaveInCreate(ControllerCreateEditParameter parm)
        {

        }

        public virtual string Event_Update_ReturnUrl_In_CreateHTTPost(string returnUrl)
        {
            return returnUrl;
        }

        public virtual void Event_BeforeSaveInCreateAndEdit(ControllerCreateEditParameter parm)
        {
            //nothing
        }


        public virtual ActionResult RedirectFromCreateHttpPostTo(BreadCrumbManager bc, ControllerCreateEditParameter parm)
        {
            if (!bc.Url_CurrMinusOne.IsNullOrWhiteSpace())
                return Redirect(bc.Url_CurrMinusOne);
            return RedirectToAction("Index");
        }

    }
}