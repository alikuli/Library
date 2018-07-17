using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;

namespace ModelsClassLibrary.ModelsNS.MenuManagerNS.MenuStatesNS
{
    public interface IMenuManagerState
    {
        //bool IsMenu { get; }
        void Load(string id, MenuPathMain menuPathMain, Product product, ProductChild productChild, MenuLevelENUM menuLevelEnum, string returnUrl, bool isMenu, string controllerCurrent, string selectId, string searchString, SortOrderENUM sortOrderEnum, ActionNameENUM actionNameEnum);

        string MenuPath1Id { get; }
        string MenuPath2Id { get; }
        string MenuPath3Id { get; }
        string ProductId { get; }
        string MenuPathMainId { get; }
        string ProductChildId { get; }

        string MenuPath1Name { get; }
        string MenuPath2Name { get; }
        string MenuPath3Name { get; }
        string MenuPath4Name { get; }
        string MenuPath5Name { get; }


        string LinkForEdit { get; }
        string LinkForCreate { get; }

        string ControllerNameInViewIndexForCreate { get; }
        string ControllerNameInViewIndexForEdit { get; }

        string BreadCrumbName_Lv1 { get; }
        string BreadCrumbName_Lv2 { get; }
        string BreadCrumbName_Lv3 { get; }
        string BreadCrumbName_Lv4 { get; }
        string BreadCrumbName_Lv5 { get; }

        string BreadCrumbLink_Lv1 { get; }
        string BreadCrumbLink_Lv2 { get; }
        string BreadCrumbLink_Lv3 { get; }
        string BreadCrumbLink_Lv4 { get; }
        string BreadCrumbLink_Lv5 { get; }

        bool ShowCreateButton { get; }
        bool ShowEditButton { get; }
        string BackToListLink { get; }
        string GetProductVmName();
        MenuLevelENUM GetPreviousMenuLevel();

        ICreateEditMenuState CreateMenuState { get; }


    }
}
