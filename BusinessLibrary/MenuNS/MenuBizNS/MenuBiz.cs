using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
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
        protected MenuPath1Biz _menuPath1Biz;
        protected ProductChildBiz _productChildBiz;
        public MenuBiz(IRepositry<ApplicationUser> userDal, MenuPathMainBiz menuPathMainBiz, ProductBiz productBiz, IRepositry<MenuPathMain> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz, MenuPath1Biz menuPath1Biz, ProductChildBiz productChildBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {

            _menuPathMainBiz = menuPathMainBiz;
            _productBiz = productBiz;
            _menuPath1Biz = menuPath1Biz;
            _productChildBiz = productChildBiz;
        }



        public override string SelectListCacheKey
        {
            get { throw new System.NotImplementedException(); }
        }





    }
}
