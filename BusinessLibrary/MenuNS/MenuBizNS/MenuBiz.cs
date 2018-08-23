using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.LikeUnlikeNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductChildNS;
using UowLibrary.ProductNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.MenuNS
{
    public partial class MenuBiz : BusinessLayer<MenuPathMain>
    {
        protected MenuPathMainBiz _menuPathMainBiz;
        protected ProductBiz _productBiz;
        protected ProductChildBiz _productChildBiz;
        protected LikeUnlikeBiz _likeUnlikeBiz;

        public MenuBiz(MenuPathMainBiz menuPathMainBiz, LikeUnlikeBiz likeUnlikeBiz, ProductBiz productBiz, IRepositry<MenuPathMain> entityDal, MyWorkClasses myWorkClasses, UploadedFileBiz uploadedFileBiz, BreadCrumbManager breadCrumbManager)
            : base(myWorkClasses, entityDal, uploadedFileBiz, breadCrumbManager)
        {

            _menuPathMainBiz = menuPathMainBiz;
            _productBiz = productBiz;
            _productChildBiz = productBiz.ProductChildBiz;

            _likeUnlikeBiz = likeUnlikeBiz;
            //_likeUnlikeBiz.UserIdFromBiz = myWorkClasses.UserId;
            //_likeUnlikeBiz.UserNameBiz = myWorkClasses.UserName;
            //_menuPath1Biz = menuPath1Biz;
            //_menuPath2Biz = menuPath2Biz;
            //_menuPath3Biz = menuPath3Biz;
        }



        public override string SelectListCacheKey
        {
            get { return "MenuPathMainSelectListData"; }
        }

        public MenuPathMainBiz MenuPathMainBiz
        {
            get { return _menuPathMainBiz; }
        }



    }
}
