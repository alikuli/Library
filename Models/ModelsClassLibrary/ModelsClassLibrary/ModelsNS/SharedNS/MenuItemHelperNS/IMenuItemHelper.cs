using System;
namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    public interface IMenuItemHelper
    {
        //this is what the menu item will read
        string MenuItem { get; }



        //this is the tooltip for the menuitem
        string ToolTip { get; }
    }
}
