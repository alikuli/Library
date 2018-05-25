using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
namespace UowLibrary.MenuNS
{
    public partial class MenuBiz : BusinessLayer<ProductCategoryMain>
    {

        public SelectList ProductCat1_SelectList()
        {
            return _productCatMainBiz.ProductCat1_SelectList();
        }

        public SelectList ProductCat2_SelectList()
        {
            return _productCatMainBiz.ProductCat2_SelectList();
        }

        public SelectList ProductCat3_SelectList()
        {
            return _productCatMainBiz.ProductCat3_SelectList();
        }


    }
}
