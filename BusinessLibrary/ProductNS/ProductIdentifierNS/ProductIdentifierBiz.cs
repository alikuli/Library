using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using DalNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.ViewModels;
using System;
using System.Reflection;
using UowLibrary.ProductNS;
using UowLibrary.StateNS;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary
{
    public partial class ProductIdentifierBiz : BusinessLayer<ProductIdentifier>
    {

        //ProductBiz _productBiz;
        public ProductIdentifierBiz(IRepositry<ApplicationUser> userDal, IRepositry<ProductIdentifier> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
            //_productBiz = productBiz;
        }




    }
}
