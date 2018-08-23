using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS;
using ModelsClassLibrary.ModelsNS.ProductNS.ProductNS.ViewModels;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MarketPlace.Web6.Controllers
{
    public partial class ProductAutomobileVMsController : AbstractController
    {



        // GET: Countries/Edit/5
        /// <summary>
        /// For Menus, you must pass the menuPath1Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isandForSearch"></param>
        /// <param name="menuPath1Id"></param>
        /// <param name="menuPath2Id"></param>
        /// <param name="menuPath3Id"></param>
        /// <param name="productId"></param>
        /// <param name="menuLevelEnum"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Edit(string id, string selectedId = "", string searchFor = "", string isandForSearch = "", string menuPathMainId = "", string productChildId = "", string productId = "", MenuENUM menuEnum = MenuENUM.EditDefault, string returnUrl = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false)
        {

            try
            {
                //the id here is a product Id
                ProductAutomobileVM productAutomobileVM = await makeTheVm(id, menuPathMainId, returnUrl, selectedId, searchFor, ActionNameENUM.Edit);
                return View(productAutomobileVM);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add(string.Format("Not Saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Home", new { selectedId = id.ToString(), returnUrl = returnUrl });
            }
        }

        private async Task<ProductAutomobileVM> makeTheVm(string id, string menuPathMainId, string returnUrl, string selectId, string searchString, ActionNameENUM actionNameEnum)
        {
            id.IsNullThrowExceptionArgument("Id not received. Bad Request");
            menuPathMainId.IsNullOrWhiteSpaceThrowException("Menu path not defined.");
            returnUrl.IsNullOrWhiteSpaceThrowException("Return URL not defined.");

            Product product = await _productBiz.FindAsync(id);
            product.IsNullThrowException("Product not found.");


            MenuPathMain mpm = await _menuPathMainBiz.FindAsync(menuPathMainId);
            mpm.IsNullThrowException("MenuPathMain not found.");

            //Not Id is ProductId
            //product.MenuManager = new MenuManager(id, mpm, product, null, menuLevelEnum, returnUrl, false, "", selectId, searchString, SortOrderENUM.Item1_Asc, actionNameEnum);

            //convert to the derived class.
            ProductAutomobileVM productAutomobileVM = ProductAutomobileVM.MakeThisClassFrom(product);

            //load 
            productAutomobileVM.RestoreNameFields();
            loadSelectLists(productAutomobileVM as ICommonWithId);
            ViewBag.ReturnUrl = returnUrl;
            _productBiz.LoadMenuPathCheckedBoxes(productAutomobileVM as IProduct);

            return productAutomobileVM;
        }


        // POST: Countries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Edit(ProductAutomobileVM entity, HttpPostedFileBase[] httpMiscUploadedFiles, HttpPostedFileBase[] httpSelfieUploads, HttpPostedFileBase[] httpIdCardFrontUploads, HttpPostedFileBase[] httpIdCardBackUploads, HttpPostedFileBase[] httpPassportFrontUploads, HttpPostedFileBase[] httpPassportVisaUploads, HttpPostedFileBase[] httpLiscenseFrontUploads, HttpPostedFileBase[] httpLiscenseBackUploads, FormCollection fc, string returnUrl = "", string menuPathMainId = "", string productId = "", string productChildId = "", string searchFor = "", string isandForSearch = "", SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, string selectedId = "")
        {
            //var req = Request;
            //string fileNameOnly = Path.GetFileNameWithoutExtension(files[0].FileName);
            //string extention = Path.GetExtension(files[0].FileName);


            try
            {

                entity.IsNullThrowExceptionArgument("No Entity received!");
                //Save extra fields into storage.
                entity.SaveNameFields();

                //LoadUserIntoEntity(entity);
                Product entityConvertedToProduct = entity.MakeProductFromThis();

                //get the Db Entity for this...
                Product dbEntity = _productBiz.Find(entity.Id);
                dbEntity.IsNullThrowException("Entity not found!");

                dbEntity.UpdatePropertiesDuringModify(entityConvertedToProduct);

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
                    MenuENUM.EditDefault,
                    User.Identity.Name,
                    returnUrl);

                await _productBiz.UpdateAndSaveAsync(parm);
                return Redirect(BreadCrumbManager.Url_CurrMinusOne);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add(string.Format("Not saved!"), MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                return RedirectToAction("Index", "Home", new { selectedId = entity.Id.ToString()});
            }
        }





    }
}
