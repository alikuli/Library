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

        public SelectList ProductCat1_SelectList()
        {
            return _productCatMainBiz.MenuPath1_SelectList();
        }

        public SelectList ProductCat2_SelectList()
        {
            return _productCatMainBiz.MenuPath2_SelectList();
        }

        public SelectList ProductCat3_SelectList()
        {
            return _productCatMainBiz.MenuPath3_SelectList();
        }


    }
}
