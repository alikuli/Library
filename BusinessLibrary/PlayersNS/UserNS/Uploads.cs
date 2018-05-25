using AliKuli.ConstantsNS;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System;
using System.Reflection;
using UserModels;

namespace UowLibrary
{
    public partial class UserBiz : BusinessLayer<ApplicationUser>
    {

        public override void AddEntityRecordIntoUpload(UploadedFile uploadFile, ApplicationUser entity, IUserHasUploadsTypeENUM iuserHasUploadsTypeEnum)
        {

            //this uses Enum which are sent along with the item that is uploaded.
            switch (iuserHasUploadsTypeEnum)
            {
                case IUserHasUploadsTypeENUM.Misc:
                    uploadFile.ApplicationUserId = entity.Id;
                    uploadFile.ApplicationUser = entity;
                    break;


                case IUserHasUploadsTypeENUM.Selfie:
                    uploadFile.SelfieId = entity.Id;
                    uploadFile.Selfie = entity;
                    break;


                case IUserHasUploadsTypeENUM.IdCardFront:
                    uploadFile.IdCardFrontUploadId = entity.Id;
                    uploadFile.IdCardFrontUpload = entity;
                    break;


                case IUserHasUploadsTypeENUM.IdCardBack:
                    uploadFile.IdCardBackUploadId = entity.Id;
                    uploadFile.IdCardBackUpload = entity;
                    break;


                case IUserHasUploadsTypeENUM.PassportFront:
                    uploadFile.PassportFrontUploadId = entity.Id;
                    uploadFile.PassportFrontUpload = entity;
                    break;


                case IUserHasUploadsTypeENUM.PassportVisa:
                    uploadFile.PassportVisaUploadId = entity.Id;
                    uploadFile.PassportVisaUpload = entity;
                    break;


                case IUserHasUploadsTypeENUM.LiscenseFront:
                    uploadFile.LiscenseFrontUploadId = entity.Id;
                    uploadFile.LiscenseFrontUpload = entity;
                    break;


                case IUserHasUploadsTypeENUM.LiscenseBack:
                    uploadFile.LiscenseBackUploadId = entity.Id;
                    uploadFile.LiscenseBackUpload = entity;
                    break;


                default:
                    ErrorsGlobal.Add("Programming error. Your uploads for User Has Uploads, are not configured properly.", MethodBase.GetCurrentMethod());
                    throw new Exception(ErrorsGlobal.ToString());
            }
        }


    }
}
