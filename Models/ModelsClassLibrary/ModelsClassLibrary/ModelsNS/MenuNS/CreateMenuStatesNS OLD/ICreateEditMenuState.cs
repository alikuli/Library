namespace ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.CreateMenuStatesNS
{
    public interface ICreateEditMenuState
    {
        bool Disable_MenuPath1 { get; }
        bool Disable_MenuPath2 { get; }
        bool Disable_MenuPath3 { get; }
        string CreateButtonController_Text { get; }
        string CreateButtonController_ControllerName { get; }
        string EditLinkController_ControllerName { get; }
        bool ShowCreateButton {get;}
        string Id_EditLink { get; }
        string Id_CreateLink { get; }
        string ProductVmNameEditLink { get; }

    }
}
