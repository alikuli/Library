using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.ParametersNS;
using UowLibrary.PlayersNS.OwnerNS;
using UowLibrary.ProductNS;

namespace MarketPlace.Web6.Controllers
{
    public class ProductChildsController : EntityAbstractController<ProductChild>
    {

        ProductBiz _productBiz;
        OwnerBiz _ownerBiz;

        public ProductChildsController(ProductBiz biz, OwnerBiz ownerBiz, AbstractControllerParameters param)
            : base(biz.ProductChildBiz, param)
        {
            _productBiz = biz;
            _ownerBiz = ownerBiz;
        }

        OwnerBiz OwnerBiz
        {
            get
            {
                _ownerBiz.UserId = UserId;
                _ownerBiz.UserName = UserName;
                return _ownerBiz;
            }
        }

        ProductBiz ProductBiz
        {
            get
            {
                _productBiz.UserId = UserId;
                _productBiz.UserName = UserName;
                return _productBiz;
            }
        }
        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {
            ProductChild pc = parm.Entity as ProductChild;

            Owner ownerForUser = OwnerBiz.GetOwnerForUser(UserId);
            ownerForUser.IsNullThrowException("Owner not found!");

            pc.OwnerId = ownerForUser.Id;
            pc.Owner = ownerForUser;

            pc.IsNullThrowException("Unable to unbox product Child");
            pc.SelectListOwners = OwnerBiz.SelectList();
            pc.SelectListProducts = ProductBiz.SelectList();

            return base.Event_CreateViewAndSetupSelectList(parm);

        }

        public override async Task<ActionResult> Create(ProductChild entity, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, EnumLibrary.EnumNS.SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", EnumLibrary.EnumNS.MenuENUM menuEnum = MenuENUM.CreateDefault, FormCollection fc = null)
        {
            try
            {

                //entity.IsNullThrowExceptionArgument("entity");
                //ProductChild pc = entity as ProductChild;
                //pc.IsNullThrowException("unable to unbox ProductChild");

                ////load the product. We need to do it here because ProductBiz is not accessable in ProductChildBiz
                //pc.ProductId.IsNullOrWhiteSpaceThrowException("There is no product!");
                //pc.Product = await ProductBiz.FindAsync(pc.ProductId);
                //pc.Product.IsNullThrowException("Product not found!");
                await loadProductIntoProductChild(entity);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
            }
            return await base.Create(entity, returnUrl, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuEnum, fc);
        }



        public override async Task<ActionResult> Edit(ProductChild entity, string returnUrl, System.Web.HttpPostedFileBase[] httpMiscUploadedFiles = null, System.Web.HttpPostedFileBase[] httpSelfieUploads = null, System.Web.HttpPostedFileBase[] httpIdCardFrontUploads = null, System.Web.HttpPostedFileBase[] httpIdCardBackUploads = null, System.Web.HttpPostedFileBase[] httpPassportFrontUploads = null, System.Web.HttpPostedFileBase[] httpPassportVisaUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseFrontUploads = null, System.Web.HttpPostedFileBase[] httpLiscenseBackUploads = null, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, string searchFor = "", string selectedId = "", bool print = false, string isandForSearch = "", MenuENUM menuEnum = MenuENUM.EditDefault, bool isMenu = false, FormCollection fc = null)
        {

            await loadProductIntoProductChild(entity);

            return await base.Edit(entity, returnUrl, httpMiscUploadedFiles, httpSelfieUploads, httpIdCardFrontUploads, httpIdCardBackUploads, httpPassportFrontUploads, httpPassportVisaUploads, httpLiscenseFrontUploads, httpLiscenseBackUploads, sortBy, searchFor, selectedId, print, isandForSearch, menuEnum, isMenu, fc);
        }

        private async Task loadProductIntoProductChild(ProductChild entity)
        {
            entity.IsNullThrowExceptionArgument("entity");
            ProductChild pc = entity as ProductChild;
            pc.IsNullThrowException("unable to unbox ProductChild");

            //load the product. We need to do it here because ProductBiz is not accessable in ProductChildBiz
            pc.ProductId.IsNullOrWhiteSpaceThrowException("There is no product!");
            pc.Product = await ProductBiz.FindAsync(pc.ProductId);
            pc.Product.IsNullThrowException("Product not found!");
        }

    }



}
