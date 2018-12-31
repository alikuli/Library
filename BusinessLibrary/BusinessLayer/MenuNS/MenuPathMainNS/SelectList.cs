using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ViewModels;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

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

        public SelectList MenuPath2_SelectList_FilteredFor(MenuPath1ENUM menuPath1Enum)
        {
            var allMenuPathMainsForMp1Lst = FindAllMenuPathMainsFor(menuPath1Enum)
                .ToList();

            if(allMenuPathMainsForMp1Lst.IsNull())
                return new SelectList(null);

            HashSet<string> hashMp2Id = new HashSet<string>();
            foreach (MenuPathMain mpm in allMenuPathMainsForMp1Lst)
            {
                hashMp2Id.Add(mpm.MenuPath2Id);
            }

            if(hashMp2Id.IsNullOrEmpty())
                return new SelectList(null);

            List<MenuPath2> Mp2Lst = new List<MenuPath2>();
            foreach (var mp2Id in hashMp2Id)
            {
                MenuPath2 mp2 = _menupath2Biz.Find(mp2Id);
                mp2.IsNullThrowException();
                Mp2Lst.Add(mp2);

            }

            SelectList selectList = _menupath2Biz.SelectList_Engine(Mp2Lst.AsQueryable());

            return selectList;
        }

    }
}
