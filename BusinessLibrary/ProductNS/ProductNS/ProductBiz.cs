using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary.MenuNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz : BusinessLayer<Product>
    {


        readonly UomWeightBiz _uomWeightBiz;
        readonly UomVolumeBiz _uomVolumeBiz;
        readonly UomLengthBiz _uomLengthBiz;
        readonly UomQuantityBiz _uomQuantityBiz;
        readonly MenuPathMainBiz _menuPathMainBiz;
        readonly MenuPath1Biz _menuPath1Biz;
        readonly MenuPath2Biz _menuPath2Biz;
        readonly MenuPath3Biz _menuPath3Biz;
        readonly ProductIdentifierBiz _productIdentifierBiz;

        public ProductBiz(IRepositry<ApplicationUser> userDal, IRepositry<Product> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz, UomVolumeBiz uomVolumeBiz, UomLengthBiz uomLengthBiz, UomQuantityBiz uomQuantityBiz, UomWeightBiz uomWeightBiz, MenuPathMainBiz menuPathMainBiz, MenuPath1Biz menuPath1Biz, MenuPath2Biz menuPath2Biz, MenuPath3Biz menuPath3Biz, ProductIdentifierBiz productIdentifierBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
            _uomWeightBiz = uomWeightBiz;
            _uomVolumeBiz = uomVolumeBiz;
            _uomLengthBiz = uomLengthBiz;
            _uomQuantityBiz = uomQuantityBiz;
            _menuPathMainBiz = menuPathMainBiz;
            _menuPath1Biz = menuPath1Biz;
            _menuPath2Biz = menuPath2Biz;
            _menuPath3Biz = menuPath3Biz;
            _productIdentifierBiz = productIdentifierBiz;
        }


        public UomWeightBiz UomWeightBiz
        {
            get
            {
                return _uomWeightBiz;
            }
        }


        public UomVolumeBiz UomVolumeBiz
        {
            get
            {
                return _uomVolumeBiz;
            }
        }

        public UomLengthBiz UomLengthBiz
        {
            get
            {
                return _uomLengthBiz;
            }
        }

        public UomQuantityBiz UomQuantityBiz
        {
            get
            {
                return _uomQuantityBiz;
            }
        }


        public MenuPathMainBiz MenuPathMainBiz
        {
            get
            {
                return _menuPathMainBiz;
            }
        }

        public MenuPath1Biz MenuPath1Biz
        {
            get
            {
                return _menuPath1Biz;
            }
        }
        public MenuPath2Biz MenuPath2Biz
        {
            get
            {
                return _menuPath2Biz;
            }
        }
        public MenuPath3Biz MenuPath3Biz
        {
            get
            {
                return _menuPath3Biz;
            }
        }

        public ProductIdentifierBiz ProductIdentifierBiz
        {
            get
            {
                return _productIdentifierBiz;
            }
        }

        //public override string SelectListCacheKey
        //{
        //    get { return "ProductSelectListCache"; }
        //}

    }
}
