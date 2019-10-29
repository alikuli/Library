using EnumLibrary.EnumNS;
namespace UowLibrary.MenuNS.MenuStateNS
{
    public interface IMenuState
    {
        MenuENUM BackLink_MenuEnum { get; }
        string BackLink_Name { get; }
        MenuENUM CreateLink_MenuEnum { get; }
        string CreateLink_Name { get; }
        string CreateAndEditLink_ControllerName { get; }

        MenuENUM EditLink_MenuEnum { get; }
        bool ShowCreateButton { get; }
        bool ShowCreateProductButton { get; }
        bool ShowEditButton { get; }

        string MenuPath1Id { get; }
        string MenuPath2Id { get; }
        string MenuPath3Id { get; }
        string MenuPathMainId { get; }
        string ProductId { get; }
        string ProductChildId { get; }
        bool IsMenu { get; }
        MenuENUM NextMenu { get; }
        MenuENUM MenuEnum { get; }
        //string GetProductVM();
        string ControllerCurrentName { get; set; }
        string MenuDisplayName { get; set; }
        bool IsProductChild { get; }
        string CreateButtonAction { get; }


    }
}
