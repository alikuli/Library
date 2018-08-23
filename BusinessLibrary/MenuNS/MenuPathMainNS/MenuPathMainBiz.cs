using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using System.Linq;
using UowLibrary.MenuNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.ProductNS
{
    public partial class MenuPathMainBiz : BusinessLayer<MenuPathMain>
    {
        MenuPath1Biz _menupath1Biz;
        MenuPath2Biz _menupath2Biz;
        MenuPath3Biz _menupath3Biz;
        UserBiz _userBiz;
        public MenuPathMainBiz(MenuPath1Biz menupath1Biz, MenuPath2Biz menupath2Biz, MenuPath3Biz menupath3Biz, IRepositry<MenuPathMain> entityDal, MyWorkClasses myWorkClasses, UploadedFileBiz uploadedFileBiz, BreadCrumbManager breadCrumbManager, UserBiz userBiz)
            : base(myWorkClasses, entityDal, uploadedFileBiz, breadCrumbManager)
        {
            _menupath1Biz = menupath1Biz;
            _menupath2Biz = menupath2Biz;
            _menupath3Biz = menupath3Biz;
            _userBiz = userBiz;
        }


        UserBiz UserBiz
        {
            get
            {
                return _userBiz;
            }
        }
        public MenuPathMain FindByMenuPath1Id(string menuPath1Id)
        {
            MenuPathMain mp1 = FindAll().FirstOrDefault(x => x.MenuPath1Id == menuPath1Id);
            return mp1;
        }



        public MenuPath1Biz MenuPath1Biz
        {
            get
            {
                return _menupath1Biz;
            }
        }
        public MenuPath2Biz MenuPath2Biz
        {
            get
            {
                return _menupath2Biz;
            }
        }
        public MenuPath3Biz MenuPath3Biz
        {
            get
            {
                return _menupath3Biz;
            }
        }
    }
}
