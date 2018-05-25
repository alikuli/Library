using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using ApplicationDbContextNS;
using DalLibrary.DalNS;
using DalLibrary.Interfaces;
using DalNS;
using ErrorHandlerLibrary.ExceptionsNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Reflection;
using UowLibrary.UploadFileNS;
using UserModels;
using WebLibrary.Programs;

namespace UowLibrary.ProductNS
{
    public partial class ProductCat3Biz : BusinessLayer<ProductCategory3>
    {

        public ProductCat3Biz(IRepositry<ApplicationUser> userDal, IRepositry<ProductCategory3> entityDal, IMemoryMain memoryMain, IErrorSet errorSet, ApplicationDbContext db, ConfigManagerHelper configManager, UploadedFileBiz uploadedFileBiz)
            : base(userDal, memoryMain, errorSet, entityDal, db, configManager, uploadedFileBiz)
        {
        }


        //private Repositry<UploadedFile> UploadDal
        //{

        //    get
        //    {
        //        if (_iUploadDAL.IsNull())
        //        {
        //            ErrorsGlobal.Add("Upload DAL not loaded.", MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());
        //        }
        //        return (Repositry<UploadedFile>)_iUploadDAL;
        //    }
        //}

    }
}
