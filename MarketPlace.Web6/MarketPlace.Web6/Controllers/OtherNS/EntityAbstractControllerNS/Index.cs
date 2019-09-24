using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using MarketPlace.Web4.Controllers;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
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
        /// <param name="menuEnum"></param>
        /// <param name="sortBy"></param>
        /// <param name="print"></param>
        /// <returns></returns>
        public virtual async Task<ActionResult> Index(string id, string searchFor, string isandForSearch, string selectedId, string returnUrl, MenuENUM menuEnum = MenuENUM.IndexDefault, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc, bool print = false, bool isMenu = false, string menuPathMainId = "", string viewName = "Index")
        {
            try
            {
                TEntity dudEntity = Biz.Factory() as TEntity;


                string productIdDud = "";
                if (returnUrl.IsNullOrWhiteSpace())
                    returnUrl = Request.Url.PathAndQuery;

                string buttonDud = "";
                TEntity nullEntity = null;
                ControllerIndexParams parms = MakeControlParameters(
                    id,
                    menuPathMainId,
                    searchFor,
                    isandForSearch,
                    selectedId,
                    nullEntity,
                    nullEntity,
                    BreadCrumbManager,
                    UserId,
                    UserName,
                    productIdDud,
                    returnUrl,
                    isMenu,
                    buttonDud,
                    menuEnum,
                    sortBy,
                    print,
                    ActionNameENUM.Index);

                IndexListVM indexListVM = await IndexEngineAsync(parms);
                indexListVM.MenuManager.ReturnUrl = returnUrl;

                if (print)
                {
                    //return View("Print", await Print(parms));
                    return PrintPdf(indexListVM);
                }


                if (!Request.IsAjaxRequest())
                {
                    return View(viewName, indexListVM);
                }

                //this is an Ajax Request.
                return PartialView("ViewControls/index/_IndexMiddlePart", indexListVM);

            }
            catch (Exception e)
            {
                ErrorsGlobal.Add("Something went wrong while printing Index.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                //you must redirect this to some index that is surely available
                //i.e. even unauthourized people can get to it.
                return RedirectToAction("Index", "Home");


            }


        }


        public async virtual Task<IndexListVM> IndexEngineAsync(ControllerIndexParams parameters)
        {
            IndexListVM indexListVM = new IndexListVM();
            try
            {
                indexListVM = await Biz.IndexAsync(parameters);

            }
            catch (Exception e)
            {

                ErrorsGlobal.Add("Something went wrong.", MethodBase.GetCurrentMethod(), e);
                ErrorsGlobal.MemorySave();
                throw new Exception(ErrorsGlobal.ToString());
            }


            return indexListVM ?? new IndexListVM(parameters);

        }


        public async Task<IndexListVM> Print(ControllerIndexParams parameters)
        {

            try
            {
                var indexListVM = await IndexEngineAsync(parameters);
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



    }
}