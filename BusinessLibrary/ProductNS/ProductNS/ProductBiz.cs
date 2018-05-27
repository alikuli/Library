using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.Interfaces;
using ErrorHandlerLibrary.ExceptionsNS;
using InvoiceNS;
using MigraDocLibrary;
using ModelsClassLibrary.ModelsNS.ProductNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.ProductNS
{
    public partial class ProductBiz : BusinessLayer<Product>
    {


        UomWeightBiz _uomWeightBiz;
        UomVolumeBiz _uomVolumeBiz;
        UomLengthBiz _uomLengthBiz;
        UomQuantityBiz _uomQuantityBiz; 

        public ProductBiz(IRepositry<ApplicationUser> userDal, IRepositry<Product> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz, UomWeightBiz uomWeightBiz, UomVolumeBiz uomVolumeBiz, UomLengthBiz uomLengthBiz, UomQuantityBiz uomQuantityBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
            _uomWeightBiz = uomWeightBiz;
            _uomVolumeBiz = uomVolumeBiz;
            _uomLengthBiz = uomLengthBiz;
            _uomQuantityBiz = uomQuantityBiz; 
        }


        public override string SelectListCacheKey
        {
            get { return "ProductSelectListCache"; }
        }

    }
}
