using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
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




        // GET: Countries/Create
        public virtual ActionResult Create(string isandForSearch, MenuENUM menuEnum = MenuENUM.unknown, string productChildId = "", string menuPathMainId = "", string productId = "", string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false)
        {
            //for product this needs to create the correct vm.

            TEntity dudEntity = Biz.EntityFactoryForHttpGet() as TEntity;

            try
            {
                ApplicationUser user = GetApplicationUser();


                bool isUserAdmin = IsUserAdmin(user);

                string logoAddress = Server.MapPath(AliKuli.ConstantsNS.MyConstants.LOGO_LOCATION);

                ControllerIndexParams parms = MakeControlParameters(
                    "",
                    searchFor,
                    isandForSearch,
                    selectedId,
                    dudEntity,
                    dudEntity,
                    menuEnum,
                    sortBy,
                    print,
                    returnUrl,
                    ActionNameENUM.Create);



                return Event_CreateViewAndSetupSelectList(parms);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("'{0}' Something went wrong during creation.", ((ICommonWithId)dudEntity).Name), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();

                return RedirectToAction("Index", new { id = "", searchFor = searchFor, isandForSearch = isandForSearch, selectedId = selectedId, returnUrl = returnUrl, productId = productId, menuPathMainId = menuPathMainId, productChildId = productChildId, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });
            }
        }



        // POST: Countries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Create(TEntity entity, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.unknown, FormCollection fc = null)
        {
            try
            {
                //entity = ifProductVmThenMakeEntityIntoProduct(entity);

                LoadUserIntoEntity(entity);

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

                await Biz.CreateAndSaveAsync(parm);

                if (returnUrl.IsNullOrWhiteSpace())
                {
                    return Event_UpdateCreateRedicrectToAction(parm);
                }
                return Redirect(returnUrl);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("'{0}' Not saved!", ((ICommonWithId)entity).Name), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", new { id = entity.Id, searchFor = searchFor, isandForSearch = isandForSearch, selectedId = entity.Id, returnUrl = returnUrl, menuLevelEnum = menuEnum, sortBy = sortBy, print = print });
            }
        }

    }
}