using System;
using System.Collections.Generic;
using AliKuli.Extentions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    /// <summary>
    /// This is a helper class that helps with menu items.
    /// this is used in _LoginPartial
    /// </summary>
    public class MenuItemHelperClass
    {

        public MenuItemHelperClass()
        {

        }
        public MenuItemHelperClass(string item, string toolTip)
        {
            Item = item;
            ToolTip = ToolTip;

        }
        public string Item { get; set; }
        public string ToolTip { get; set; }
        public string SearchStringForIndex { get; set; }
    }
}
