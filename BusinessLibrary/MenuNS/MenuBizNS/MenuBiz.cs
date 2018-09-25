using AliKuli.Extentions;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ModelsClassLibrary.MenuNS;
using UowLibrary.LikeUnlikeNS;
using UowLibrary.ParametersNS;
using UowLibrary.PageViewNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;
using UowLibrary.UploadFileNS;

namespace UowLibrary.MenuNS
{
    public partial class MenuBiz : BusinessLayer<MenuPathMain>
    {
        protected MenuPathMainBiz _menuPathMainBiz;
        protected ProductBiz _productBiz;
        protected ProductChildBiz _productChildBiz;
        protected LikeUnlikeBiz _likeUnlikeBiz;
        UserBiz _userBiz;
        //PageViewBiz _pageViewBiz;

        public MenuBiz(MenuPathMainBiz menuPathMainBiz, LikeUnlikeBiz likeUnlikeBiz, ProductBiz productBiz, UserBiz userBiz, IRepositry<MenuPathMain> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {

            _menuPathMainBiz = menuPathMainBiz;
            _productBiz = productBiz;
            _productChildBiz = productBiz.ProductChildBiz;
            _userBiz = userBiz;
            _likeUnlikeBiz = likeUnlikeBiz;
          //  _pageViewBiz = pageViewBiz;
        }



        public override string SelectListCacheKey
        {
            get { return "MenuPathMainSelectListData"; }
        }

        public MenuPathMainBiz MenuPathMainBiz
        {
            get { return _menuPathMainBiz; }
        }


        public UserBiz UserBiz
        {
            get
            {
                _userBiz.IsNullThrowExceptionArgument();
                return _userBiz;
            }
        }


    }
}
