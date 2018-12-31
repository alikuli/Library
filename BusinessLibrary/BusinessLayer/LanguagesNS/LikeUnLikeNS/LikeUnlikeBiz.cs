using DalLibrary.Interfaces;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using UowLibrary.ParametersNS;
using UowLibrary.ProductNS;

namespace UowLibrary.LikeUnlikeNS
{
    public partial class LikeUnlikeBiz : BusinessLayer<LikeUnlike>
    {


        MenuPathMainBiz _menuPathMainBiz;
        //ProductBiz _productBiz;
        UserBiz _userBiz;
        public LikeUnlikeBiz(MenuPathMainBiz menuPathMainBiz, UserBiz userBiz, /* ProductBiz productBiz, */ AbstractControllerParameters myWorkClasses, IRepositry<LikeUnlike> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)
        {
            _menuPathMainBiz = menuPathMainBiz;
            //_productBiz = productBiz;
            _userBiz = userBiz;
        }


        //public ProductBiz ProductBiz
        //{
        //    get
        //    {
        //        _productBiz.UserId = UserId;
        //        _productBiz.UserName = UserName;
        //        return _productBiz;
        //    }

        //}

        public UserBiz UserBiz
        {
            get
            {
                _userBiz.UserId = UserId;
                _userBiz.UserName = UserName;
                return _userBiz;

            }
        }



        public override string SelectListCacheKey
        {
            get
            {
                return "LikeUnlikesSelectListData";
            }
        }
    }
}
