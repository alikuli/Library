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

namespace UowLibrary.MenuNS
{
    public partial class MenuPath2Biz 
    {

        public override void AddEntityRecordIntoUpload(UploadedFile uploadFile, MenuPath2 entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            uploadFile.MenuPath2Id = entity.Id;
            uploadFile.MenuPath2 = entity; 
            ;
        }


    }
}
