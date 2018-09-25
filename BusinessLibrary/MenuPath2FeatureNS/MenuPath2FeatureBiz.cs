using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.FeaturesNS;
using UowLibrary.MenuNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;

namespace UowLibrary.FeaturesNS
{
    public partial class MenuPath2FeatureBiz : BusinessLayer<MenuPath2Feature>
    {


        MenuPath2Biz _menuPath2Biz;

        public MenuPath2FeatureBiz(IRepositry<MenuPath2Feature> entityDal, BizParameters bizParameters, MenuPath2Biz menuPath2Biz)//, UserBiz userBiz)
            : base(entityDal, bizParameters)
        {
            _menuPath2Biz = menuPath2Biz;
        }




        public MenuPath2Biz MenuPath2Biz
        {
            get
            {
                return _menuPath2Biz; ;
            }
        }


    }
}
