using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.MenuNS;
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
        //protected MenuPath1Biz _menuPath1Biz;
        //protected MenuPath2Biz _menuPath2Biz;
        //protected MenuPath3Biz _menuPath3Biz;
        protected ProductChildBiz _productChildBiz;
        public MenuBiz(IRepositry<ApplicationUser> userDal, MenuPathMainBiz menuPathMainBiz, ProductBiz productBiz, IRepositry<MenuPathMain> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz,  ProductChildBiz productChildBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {

            _menuPathMainBiz = menuPathMainBiz;
            _productBiz = productBiz;
            _productChildBiz = productChildBiz;
            //_menuPath1Biz = menuPath1Biz;
            //_menuPath2Biz = menuPath2Biz;
            //_menuPath3Biz = menuPath3Biz;
        }



        public override string SelectListCacheKey
        {
            get { return "MenuPathMainSelectListData"; }
        }





    }
}
