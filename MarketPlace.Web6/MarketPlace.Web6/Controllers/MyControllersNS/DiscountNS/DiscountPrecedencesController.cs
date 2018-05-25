using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ErrorHandlerLibrary.ExceptionsNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary.DiscountPrecedenceNS;

namespace MarketPlace.Web6.Controllers
{
    public class DiscountPrecedencesController : EntityAbstractController<DiscountPrecedence>
    {
        DiscountPrecedenceBiz _discountPrecBiz;
        readonly static string[] bindArray;

        #region Constructo and initializers

        public DiscountPrecedencesController(DiscountPrecedenceBiz DiscountPrecedencesBiz, IErrorSet errorSet)
            : base(DiscountPrecedencesBiz, errorSet)
        {
            _discountPrecBiz = DiscountPrecedencesBiz;
        }
        static DiscountPrecedencesController()
        {
            bindArray = new DiscountPrecedence().FieldsToLoadFromView().ToArray();

        }
        #endregion

        public override ActionResult Event_CreateViewAndSetupSelectList(ControllerIndexParams parm)
        {

            //we want to send a VM
            DiscountPrecedence entity = (DiscountPrecedence)parm.Entity;
            DiscountPrecedenceVM dpVm = new DiscountPrecedenceVM(entity);
            dpVm.UsersSelectList = _discountPrecBiz.UsersSelectList();
            return View(dpVm);
        }


        public async Task<ActionResult> MoveUp(string searchFor, string selectedId, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc)
        {
            ControllerIndexParams parms = new ControllerIndexParams
            {
                SearchFor = searchFor,
                SelectedId = selectedId,
                SortBy = SortOrderENUM.Item1_Asc, //different
            };


            var indexListVM = await indexEngine(parms);
            if (selectedId.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.AddMessage("No selected Id Received.");
                return PartialView("_IndexMiddlePart", indexListVM);
            }

            _discountPrecBiz.SwapRanks(indexListVM.SelectedIdStr, indexListVM.SelectedID_PreviousItem.ToString());
            return RedirectToAction("Index", new { searchFor = searchFor, selectedId = selectedId, sortBy = sortBy });



        }
        public async Task<ActionResult> MoveDown(string searchFor, string selectedId, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc)
        {
            ControllerIndexParams parms = new ControllerIndexParams
            {
                SearchFor = searchFor,
                SelectedId = selectedId,
                SortBy = SortOrderENUM.Item1_Asc, //different
            };


            var indexListVM = await indexEngine(parms);
            if (selectedId.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.AddMessage("No selected Id Received.");
                return PartialView(@"_IndexMiddlePart", indexListVM);
            }

            _discountPrecBiz.SwapRanks(indexListVM.SelectedIdStr, indexListVM.SelectedId_NextItem.ToString());
            return RedirectToAction("Index", new { searchFor = searchFor, selectedId = selectedId, sortBy = sortBy });



        }


        public async Task<ActionResult> MoveTop(string searchFor, string selectedId, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc)
        {
            ControllerIndexParams parms = new ControllerIndexParams
            {
                SearchFor = searchFor,
                SelectedId = selectedId,
                SortBy = SortOrderENUM.Item1_Asc, //different
            };

            var indexListVM = await indexEngine(parms);
            if (selectedId.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.AddMessage("No selected Id Received.");
                return PartialView(@"_IndexMiddlePart", indexListVM);
            }

            _discountPrecBiz.MakeFirst(indexListVM.SelectedId, indexListVM.FirstItemId);
            return RedirectToAction("Index", new { searchFor = searchFor, selectedId = selectedId, sortBy = sortBy });



        }

        public async Task<ActionResult> MoveBottom(string searchFor, string selectedId, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc)
        {

            ControllerIndexParams parms = new ControllerIndexParams
            {
                SearchFor = searchFor,
                SelectedId = selectedId,
                SortBy = SortOrderENUM.Item1_Asc, //different
            };

            //SortOrderENUM.Item1_Asc is by rank.
            var indexListVM = await indexEngine(parms);
            if (selectedId.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.AddMessage("No selected Id Received.");
                return PartialView(@"_IndexMiddlePart", indexListVM);
            }

            _discountPrecBiz.MakeLast(indexListVM.SelectedId, indexListVM.LastItemId);
            return RedirectToAction("Index", new { searchFor = searchFor, selectedId = selectedId, sortBy = sortBy });



        }



    }
}