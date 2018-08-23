using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using BreadCrumbsLibraryNS.Programs;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.RightsNS;
using UowLibrary.MenuNS;
using UowLibrary.MyWorkClassesNS;
using UowLibrary.PlayersNS;
using UowLibrary.ProductChildNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz : BusinessLayer<Product>
    {


        MyWorkClassesProduct _myWorkClasses;
        UserBiz _userBiz;
        public ProductBiz(UserBiz userBiz, IRepositry<Product> entityDal, MyWorkClassesProduct myWorkClassesProduct, MyWorkClasses myWorkClasses, UploadedFileBiz uploadedFileBiz, BreadCrumbManager breadCrumbManager)
            : base(myWorkClasses, entityDal, uploadedFileBiz, breadCrumbManager)
        {

            _myWorkClasses = myWorkClassesProduct;
            _userBiz = userBiz;
        }


        public UserBiz UserBiz
        {
            get { return _userBiz; }
        }
        public UomVolumeBiz UomVolumeBiz { get { return _myWorkClasses.UomVolumeBiz; } }
        public UomLengthBiz UomLengthBiz { get { return _myWorkClasses.UomLengthBiz; } }
        public UomQuantityBiz UomQuantityBiz { get { return _myWorkClasses.UomQuantityBiz; } }
        public UomWeightBiz UomWeightBiz { get { return _myWorkClasses.UomWeightBiz; } }
        public MenuPathMainBiz MenuPathMainBiz { get { return _myWorkClasses.MenuPathMainBiz; } }
        public ProductIdentifierBiz ProductIdentifierBiz { get { return _myWorkClasses.ProductIdentifierBiz; } }
        public ProductChildBiz ProductChildBiz { get { return _myWorkClasses.ProductChildBiz; } }



        public MenuPath1Biz MenuPath1Biz
        {
            get
            {
                return _myWorkClasses.MenuPathMainBiz.MenuPath1Biz;
            }
        }
        public MenuPath2Biz MenuPath2Biz
        {
            get
            {
                return _myWorkClasses.MenuPathMainBiz.MenuPath2Biz;
            }
        }
        public MenuPath3Biz MenuPath3Biz
        {
            get
            {
                return _myWorkClasses.MenuPathMainBiz.MenuPath3Biz;
            }
        }



    }
}
