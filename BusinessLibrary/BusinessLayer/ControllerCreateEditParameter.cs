
using AliKuli.Extentions;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace UowLibrary
{
    /// <summary>
    /// This class will carry all the information from the create and edit modules in the controller all the way through
    /// Morevover, it will also dea
    /// </summary>
    /// 
    [NotMapped]
    public class ControllerCreateEditParameter
    {
        public ControllerCreateEditParameter()
        {
            MiscUploadedFiles = new ControllerCreateEditParameterDetail();
            SelfieUpload = new ControllerCreateEditParameterDetail();
            IdCardFront = new ControllerCreateEditParameterDetail();
            IdCardBack = new ControllerCreateEditParameterDetail();
            PassportFront = new ControllerCreateEditParameterDetail();
            PassportVisa = new ControllerCreateEditParameterDetail();
            LiscenseFront = new ControllerCreateEditParameterDetail();
            LiscenseBack = new ControllerCreateEditParameterDetail();
        }


        public ControllerCreateEditParameter (ICommonWithId entity, HttpPostedFileBase[] httpMiscUploadedFiles, HttpPostedFileBase[] httpSelfieUpload, HttpPostedFileBase[] httpIdCardFront, HttpPostedFileBase[] httpIdCardBack, HttpPostedFileBase[] httpPassportFront, HttpPostedFileBase[] httpPassportVisa, HttpPostedFileBase[] httpLiscenseFront, HttpPostedFileBase[] httpLiscenseBack):this()
        {
            Entity = entity;
            MiscUploadedFiles.HttpBase = httpMiscUploadedFiles;
            SelfieUpload.HttpBase = httpSelfieUpload;
            IdCardFront.HttpBase = httpIdCardFront;
            IdCardBack.HttpBase=httpIdCardBack;
            PassportFront.HttpBase=httpPassportFront;
            PassportVisa.HttpBase=httpPassportVisa;
            LiscenseFront.HttpBase=httpLiscenseFront;
            LiscenseBack.HttpBase=httpLiscenseBack;

            if(IsIHasUploads)
            {
                MiscUploadedFiles.FileLocation = Entity_IHasUploads.MiscFilesLocation;
            }

            if(IsIUserHasUploads)
            {
                SelfieUpload.FileLocation = Entity_IUserHasUploads.SelfieLocation;
                
                IdCardFront.FileLocation = Entity_IUserHasUploads.IdCardFrontLocation;
                IdCardBack.FileLocation = Entity_IUserHasUploads.IdCardBackLocation;
                
                PassportFront.FileLocation = Entity_IUserHasUploads.PassportFrontLocation;
                PassportVisa.FileLocation = Entity_IUserHasUploads.PassportVisaLocation;
                
                LiscenseFront.FileLocation = Entity_IUserHasUploads.LiscenseFrontLocation;
                LiscenseBack.FileLocation = Entity_IUserHasUploads.LiscenseBackLocation;
            }
        }

        public ICommonWithId Entity { get; set; }




        #region Uploads


        public ControllerCreateEditParameterDetail MiscUploadedFiles { get; set; }

        public ControllerCreateEditParameterDetail SelfieUpload { get; set; }
        public ControllerCreateEditParameterDetail IdCardFront { get; set; }
        public ControllerCreateEditParameterDetail IdCardBack { get; set; }
        public ControllerCreateEditParameterDetail PassportFront { get; set; }
        public ControllerCreateEditParameterDetail PassportVisa { get; set; }
        public ControllerCreateEditParameterDetail LiscenseFront { get; set; }
        public ControllerCreateEditParameterDetail LiscenseBack { get; set; }



        #endregion


        #region IsIHasUploads
        public bool IsIHasUploads
        {
            get
            {
                return !Entity_IHasUploads.IsNull();
            }
        }

        public IHasUploads Entity_IHasUploads
        {
            get
            {
                return Entity as IHasUploads;
            }
        }
        #endregion

        #region IUserHasUploads

        public bool IsIUserHasUploads
        {
            get
            {
                return !Entity_IUserHasUploads.IsNull();
            }
        }

        public IUserHasUploads Entity_IUserHasUploads
        {
            get
            {
                return Entity as IUserHasUploads;
            }
        }
        #endregion

    }
}
