using AliKuli.ConstantsNS;
using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.MenuNS;
using ModelsClassLibrary.ModelsNS.ProductNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace UowLibrary.MenuNS

{
    public partial class MenuPath1Biz 
    {
        //private readonly IRepositry<UploadedFile> _uploadFileDAL;

        /// <summary>
        /// This is where all the uploaded Files will be saved
        /// </summary>
        /// <returns></returns>
        //public override string Event_SaveLocationForUploadedFiles()
        //{
        //    return MyConstants.SAVE_LOCATION_PRODUCT_CATEGORY1;
        //}


        public override void AddEntityRecordIntoUpload(UploadedFile uploadFile, MenuPath1 entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {
            uploadFile.MenuPath1Id = entity.Id;
            uploadFile.MenuPath1 = entity;
        }




    }
}
