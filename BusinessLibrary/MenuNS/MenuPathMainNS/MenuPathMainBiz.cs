using DalLibrary.Interfaces;
using ModelsClassLibrary.MenuNS;
using System.Linq;
using UowLibrary.FeatureNS.MenuFeatureNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.ProductNS
{

    public partial class MenuPathMainBiz : BusinessLayer<MenuPathMain>
    {
        MenuPath1Biz _menupath1Biz;
        MenuPath2Biz _menupath2Biz;
        MenuPath3Biz _menupath3Biz;
        MenuFeatureBiz _menuFeatureBiz;
        UserBiz _userBiz;
        public MenuPathMainBiz(MenuPath1Biz menupath1Biz, UserBiz userBiz, MenuPath2Biz menupath2Biz, MenuPath3Biz menupath3Biz, IRepositry<MenuPathMain> entityDal, MenuFeatureBiz menuFeatureBiz, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {
            _menupath1Biz = menupath1Biz;
            _menupath2Biz = menupath2Biz;
            _menupath3Biz = menupath3Biz;
            _menuFeatureBiz = menuFeatureBiz;

            _userBiz = userBiz;
        }


        public MenuFeatureBiz MenuFeatureBiz
        {
            get
            {
                return _menuFeatureBiz;
            }
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
