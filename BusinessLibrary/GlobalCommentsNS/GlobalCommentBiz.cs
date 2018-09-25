using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using UowLibrary.MenuNS;
using UowLibrary.ProductNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;
using AliKuli.Extentions;
using UowLibrary.ProductChildNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.PlayersNS;
using UowLibrary.ParametersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;


namespace UowLibrary.GlobalCommentsNS
{
    public partial class GlobalCommentBiz : BusinessLayer<GlobalComment>
    {
        MenuPathMainBiz _menuPathMainBiz;
        ProductBiz _productBiz;
        UserBiz _userBiz;

        public GlobalCommentBiz(MenuPathMainBiz menuPathMainBiz, ProductBiz productBiz, UserBiz userBiz, IRepositry<GlobalComment> entityDal, BizParameters bizParameters)
            : base(entityDal, bizParameters)

        {
            _menuPathMainBiz = menuPathMainBiz;
            _productBiz = productBiz;
            _userBiz = userBiz;
        }

        public MenuPath1Biz MenuPath1Biz
        {
            get
            {
                _menuPathMainBiz.IsNullThrowException();
                _menuPathMainBiz.MenuPath1Biz.IsNullThrowException();
                return _menuPathMainBiz.MenuPath1Biz;
            }
        }

        public MenuPath2Biz MenuPath2Biz
        {
            get
            {
                _menuPathMainBiz.IsNullThrowException();
                _menuPathMainBiz.MenuPath2Biz.IsNullThrowException();
                return _menuPathMainBiz.MenuPath2Biz;
            }
        }


        public MenuPath3Biz MenuPath3Biz
        {
            get
            {
                _menuPathMainBiz.IsNullThrowException();
                _menuPathMainBiz.MenuPath3Biz.IsNullThrowException();
                return _menuPathMainBiz.MenuPath3Biz;
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


        public ProductChildBiz ProductChildBiz
        {
            get
            {
                _productBiz.IsNullThrowException();
                _productBiz.ProductChildBiz.IsNullThrowException();
                return _productBiz.ProductChildBiz;
            }
        }

    }
}
