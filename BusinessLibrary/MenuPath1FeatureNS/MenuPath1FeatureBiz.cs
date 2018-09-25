using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;

namespace UowLibrary.FeaturesNS
{
    public partial class MenuPath1FeatureBiz : BusinessLayer<MenuPath1Feature>
    {


        MenuPath1Biz _menuPath1Biz;

        public MenuPath1FeatureBiz(IRepositry<MenuPath1Feature> entityDal, BizParameters bizParameters, MenuPath1Biz menuPath1Biz)//, UserBiz userBiz)
            : base(entityDal, bizParameters)
        {
            _menuPath1Biz = menuPath1Biz;
        }




        public MenuPath1Biz MenuPath1Biz
        {
            get
            {
                return _menuPath1Biz; ;    
            }
        }


    }
}
