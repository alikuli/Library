using AliKuli.Extentions;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ViewModels;
using System.Web.Mvc;

namespace UowLibrary.ProductNS

{
    public partial class MenuPathMainBiz 
    {


        public override string SelectListCacheKey
        {
            get { return "MenuPathMainSelectListData"; }
        }

        public SelectList MenuPath1_SelectList()
        {
            return _menupath1Biz.SelectList();
        }
        
        public SelectList MenuPath2_SelectList()
        {
            return _menupath2Biz.SelectList();
        }

        public SelectList MenuPath3_SelectList()
        {
            return _menupath3Biz.SelectList();
        }


    }
}
