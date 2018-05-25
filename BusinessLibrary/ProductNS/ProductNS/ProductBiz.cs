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


        public ProductBiz(IRepositry<ApplicationUser> userDal, IRepositry<Product> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
        }




        public override string SelectListCacheKey
        {
            get { return "ProductSelectListCache"; }
        }

    }
}
