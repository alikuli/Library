using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using MarketPlace.Web6.Controllers.Abstract;
using ModelsClassLibrary.ModelsNS.DiscountNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System.Threading.Tasks;
using System.Web.Mvc;
using UowLibrary;
using UowLibrary.DiscountPrecedenceNS;
using UowLibrary.ParametersNS;

namespace MarketPlace.Web6.Controllers
{
    public class DiscountPrecedencesController : EntityAbstractController<DiscountPrecedence>
    {
        DiscountPrecedenceBiz _discountPrecBiz;
        readonly static string[] bindArray;
        UserBiz _userBiz;


        public DiscountPrecedencesController(DiscountPrecedenceBiz biz, UserBiz userBiz, AbstractControllerParameters param)
            : base(biz, param)
        {
            _discountPrecBiz = biz;
            _userBiz = userBiz;
        }
        static DiscountPrecedencesController()
        {
            bindArray = new DiscountPrecedence().FieldsToLoadFromView().ToArray();

        }


        UserBiz UserBiz
        {
            get
            {
                return _userBiz;
            }
        }

        public override ActionResult Event_Create_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {

            //we want to send a VM
            DiscountPrecedence entity = (DiscountPrecedence)parm.Entity;
            DiscountPrecedenceVM dpVm = new DiscountPrecedenceVM(entity);
            dpVm.UsersSelectList = UserBiz.SelectList();
            return View(dpVm);
        }

        public override ActionResult Event_Edit_ViewAndSetupSelectList_GET(ControllerIndexParams parm)
        {
            DiscountPrecedence entity = (DiscountPrecedence)parm.Entity;
            DiscountPrecedenceVM dpVm = new DiscountPrecedenceVM(entity);
            dpVm.UsersSelectList = UserBiz.SelectList();
            return base.Event_Edit_ViewAndSetupSelectList_GET(parm);
        }
        public async Task<ActionResult> MoveUp(string searchFor, string selectedId, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc)
        {
            ControllerIndexParams parms = new ControllerIndexParams
            {
                SearchFor = searchFor,
                SelectedId = selectedId,
                SortBy = SortOrderENUM.Item1_Asc, //different
                //Menu = new MenuParameters(),
            };
            var indexListVM = await IndexEngineAsync(parms);
            if (selectedId.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.AddMessage("No selected Id Received.");
                return PartialView("_IndexMiddlePart", indexListVM);
            }
            indexListVM.SelectedId = selectedId;
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


            var indexListVM = await IndexEngineAsync(parms);
            indexListVM.SelectedId = selectedId;
            if (selectedId.IsNullOrWhiteSpace())
            {
                ErrorsGlobal.AddMessage("No selected Id Received.");
                return PartialView(@"_IndexMiddlePart", indexListVM);
            }

            _discountPrecBiz.SwapRanks(indexListVM.SelectedIdStr, indexListVM.SelectedId_NextItem.ToString());
            return RedirectToAction("Index", new { searchFor = searchFor, selectedId = selectedId, sortBy = sortBy });



        }


        public ActionResult MoveTop(string searchFor, string selectedId, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc)
        {
            if (!selectedId.IsNullOrWhiteSpace())
                _discountPrecBiz.MakeFirst(selectedId, GlobalObject);

            return RedirectToAction("Index", new { searchFor = searchFor, selectedId = selectedId, sortBy = sortBy });



        }

        public ActionResult MoveBottom(string searchFor, string selectedId, SortOrderENUM sortBy = SortOrderENUM.Item1_Asc)
        {


            if (!selectedId.IsNullOrWhiteSpace())
                _discountPrecBiz.MakeLast(selectedId, GlobalObject);

            return RedirectToAction("Index", new { searchFor = searchFor, selectedId = selectedId, sortBy = sortBy });



        }



    }
}