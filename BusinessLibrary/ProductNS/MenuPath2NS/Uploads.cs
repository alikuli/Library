using AliKuli.ConstantsNS;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UowLibrary.ProductNS
{
    public partial class ProductCat2Biz 
    {

        public override void AddEntityRecordIntoUpload(UploadedFile uploadFile, ProductCategory2 entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            uploadFile.ProductCategory2Id = entity.Id;
            uploadFile.ProductCategory2 = entity; 
            ;
        }


    }
}
