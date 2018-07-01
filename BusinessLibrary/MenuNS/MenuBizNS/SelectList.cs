using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz
    {

        public SelectList MenuPath1_SelectList()
        {
            return _menuPathMainBiz.MenuPath1_SelectList();
        }

        public SelectList MenuPath2_SelectList()
        {
            return _menuPathMainBiz.MenuPath2_SelectList();
        }

        public SelectList MenuPath3_SelectList()
        {
            return _menuPathMainBiz.MenuPath3_SelectList();
        }

        public SelectList MenuPath2_SelectList_FilteredFor(MenuPath1ENUM menuPath1Enum)
        {


            return _menuPathMainBiz.MenuPath2_SelectList_FilteredFor(menuPath1Enum);
        }

    }
}
