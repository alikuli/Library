
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    /// <summary>
    /// This class will carry all the information from the create and edit modules in the controller all the way through.
    /// You will need to add the locations of the files later from the entity. This will have to happen in the Biz class Fix event where 
    /// user info will be available.
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


        public ControllerCreateEditParameter(
            ICommonWithId entity,
            HttpPostedFileBase[] httpMiscUploadedFiles,
            HttpPostedFileBase[] httpSelfieUpload,
            HttpPostedFileBase[] httpIdCardFront,
            HttpPostedFileBase[] httpIdCardBack,
            HttpPostedFileBase[] httpPassportFront,
            HttpPostedFileBase[] httpPassportVisa,
            HttpPostedFileBase[] httpLiscenseFront,
            HttpPostedFileBase[] httpLiscenseBack,
            MenuENUM menuEnum,
            string userName,
            string returnUrl)
            : this()
        {
            Entity = entity;
            MiscUploadedFiles.HttpBase = httpMiscUploadedFiles;
            SelfieUpload.HttpBase = httpSelfieUpload;
            IdCardFront.HttpBase = httpIdCardFront;
            IdCardBack.HttpBase = httpIdCardBack;
            PassportFront.HttpBase = httpPassportFront;
            PassportVisa.HttpBase = httpPassportVisa;
            LiscenseFront.HttpBase = httpLiscenseFront;
            LiscenseBack.HttpBase = httpLiscenseBack;

            UserName = userName;
            //Menu = new MenuParameters(menuEnum, entity.Id);

            //Menu = new MenuParameters(menuEnum, menuPathMainId, productId, productChildId, returnUrl);
            //todo what should we do if userName is empty?

            //if (IsIHasUploads)
            //{
            //    //we need to load the user in entities that require user right here...
            //    //because user is required to find where to store the file.
                
                //MiscUploadedFiles.FileLocationConst = Entity_IHasUploads.MiscFilesLocation();
            //}

            ////this is where the file gets it's location. The location comes from the constants
            //if (IsIUserHasUploads)
            //{
            //    SelfieUpload.FileLocationConst = Entity_IUserHasUploads.SelfieLocationConst(UserName);

            //    IdCardFront.FileLocationConst = Entity_IUserHasUploads.IdCardFrontLocationConst(UserName);
            //    IdCardBack.FileLocationConst = Entity_IUserHasUploads.IdCardBackLocationConst(UserName);

            //    PassportFront.FileLocationConst = Entity_IUserHasUploads.PassportFrontLocationConst(UserName);
            //    PassportVisa.FileLocationConst = Entity_IUserHasUploads.PassportVisaLocationConst(UserName);

            //    LiscenseFront.FileLocationConst = Entity_IUserHasUploads.LiscenseFrontLocationConst(UserName);
            //    LiscenseBack.FileLocationConst = Entity_IUserHasUploads.LiscenseBackLocationConst(UserName);
            //}

        }

        public ICommonWithId Entity { get; set; }
        public string UserName { get; set; }

        //public MenuParameters Menu { get; set; }

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
