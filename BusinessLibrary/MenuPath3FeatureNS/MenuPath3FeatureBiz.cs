using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;

namespace UowLibrary.FeaturesNS
{
    public partial class MenuPath3FeatureBiz : BusinessLayer<MenuPath3Feature>
    {


        MenuPath3Biz _menuPath3Biz;

        public MenuPath3FeatureBiz(IRepositry<MenuPath3Feature> entityDal, BizParameters bizParameters, MenuPath3Biz menuPath3Biz)//, UserBiz userBiz)
            : base(entityDal, bizParameters)
        {
            _menuPath3Biz = menuPath3Biz;
        }




        public MenuPath3Biz MenuPath3Biz
        {
            get
            {
                return _menuPath3Biz; ;
            }
        }


    }
}
