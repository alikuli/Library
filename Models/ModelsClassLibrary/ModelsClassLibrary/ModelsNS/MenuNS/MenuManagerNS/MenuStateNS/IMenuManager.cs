using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary.MenuNS.MenuStateNS;
namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS
{
    public interface IMenuManager
    {
        //string ControllerCurrentName { get; set; }
        string CurrentUrl { get; set; }
        //MenuENUM MenuEnum { get; set; }
        MenuENUM MenuEnumCreateButton { get; set; }
        MenuENUM MenuEnumEditButtom { get; set; }
        //string MenuPath1Id { get; set; }
        //string MenuPath2Id { get; set; }
        //string MenuPath3Id { get; set; }
        MenuPathMain MenuPathMain { get; set; }
        IMenuState MenuState { get; set; }
        Product Product { get; set; }
        ProductChild ProductChild { get; set; }
        string SearchString { get; set; }
        string SelectedId { get; set; }
        SortOrderENUM SortOrderEnum { get; set; }
    }
}
