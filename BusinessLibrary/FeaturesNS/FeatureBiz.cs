using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PeopleNS;
using ModelsClassLibrary.ModelsNS.ProductNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.SharedNS.CounterNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using AliKuli.Extentions;
using UowLibrary.MenuNS;
using UowLibrary.ProductChildNS;

namespace UowLibrary.FeaturesNS

{
    public partial class FeatureBiz : BusinessLayer<Feature>
    {

        MenuPathMainBiz _menuPathMainBiz;
        ProductBiz _productBiz;
        //UserBiz _userBiz;

        public FeatureBiz(IRepositry<Feature> entityDal, MyWorkClasses myWorkClasses, UploadedFileBiz uploadedFileBiz, BreadCrumbManager breadCrumbManager, MenuPathMainBiz menuPathMainBiz, ProductBiz productBiz)//, UserBiz userBiz)
            : base(myWorkClasses, entityDal, uploadedFileBiz, breadCrumbManager)
        {
            _productBiz = productBiz;
            _menuPathMainBiz = menuPathMainBiz;
            //_userBiz = userBiz;
        }

        //public UserBiz UserBiz
        //{
        //    get
        //    {
        //        _userBiz.IsNullThrowException();
        //        return _userBiz;
        //    }
        //}
        public MenuPathMainBiz MenuPathMainBiz
        {
            get
            {
                _menuPathMainBiz.IsNullThrowException();
                return _menuPathMainBiz;
            }
        }
        public ProductBiz ProductBiz
        {
            get
            {
                _productBiz.IsNullThrowException();
                return _productBiz;
            }
        }



        public MenuPath1Biz MenuPath1Biz
        {
            get
            {
                MenuPathMainBiz.MenuPath1Biz.IsNullThrowException();
                return MenuPathMainBiz.MenuPath1Biz;
            }
        }

        public MenuPath2Biz MenuPath2Biz
        {
            get
            {
                MenuPathMainBiz.MenuPath2Biz.IsNullThrowException();
                return MenuPathMainBiz.MenuPath2Biz;
            }
        }
        public MenuPath3Biz MenuPath3Biz
        {
            get
            {
                MenuPathMainBiz.MenuPath3Biz.IsNullThrowException();
                return MenuPathMainBiz.MenuPath3Biz;
            }
        }



        public ProductChildBiz ProductChildBiz
        {
            get
            {
                ProductBiz.ProductChildBiz.IsNullThrowException();
                return ProductBiz.ProductChildBiz;
            }
        }
    }
}
