using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary.MenuNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.ProductNS
{
    public partial class ProductCatMainBiz : BusinessLayer<ProductCategoryMain>
    {
        MenuPath1Biz _productCat1Biz;
        ProductCat2Biz _productCat2Biz;
        ProductCat3Biz _productCat3Biz;
        public ProductCatMainBiz(IRepositry<ApplicationUser> userDal, MenuPath1Biz productCat1Biz, ProductCat2Biz productCat2Biz, ProductCat3Biz productCat3Biz, IRepositry<ProductCategoryMain> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
            _productCat1Biz = productCat1Biz;
            _productCat2Biz = productCat2Biz;
            _productCat3Biz = productCat3Biz;
        }
    }
}
