
using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using ModelsClassLibrary.ModelsNS.GlobalObjectNS;
using System;
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
            string userId,
            string returnUrl,
            GlobalObject globalObject,
            string buttonValue)
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
            ReturnUrl = returnUrl;
            UserName = userName;
            UserId = userId;
            //MenuParameters = new MenuParameters(menuEnum, Entity.Id);
            MenuEnum = menuEnum;
            Id = Entity.Id;
            GlobalObject = globalObject;
            ButtonValue = buttonValue;
        }
        public string Id { get; set; }
        public MenuENUM MenuEnum { get; set; }
        public string ButtonValue { get; set; }
        public GlobalObject GlobalObject { get; set; }
        public ICommonWithId Entity { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }

        public string ReturnUrl { get; set; }
        //public MenuParameters MenuParameters { get; set; }
        public ControllerIndexParams ConvertToControllerIndexParams()
        {
            ControllerIndexParams cip = new ControllerIndexParams();
            cip.Entity = Entity;
            cip.MenuEnum = MenuEnum;
            cip.Id = Id;
            cip.UserId = UserId;
            cip.UserName = UserName;
            cip.ReturnUrl = ReturnUrl;
            return cip;
        }

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


        public BuySellDocStateModifierENUM BuySellDocStateModifierEnum { get { return get_BuySellDocStateModifierEnum_from_Button(); } }
        private BuySellDocStateModifierENUM get_BuySellDocStateModifierEnum_from_Button()
        {
            if (ButtonValue.IsNullOrWhiteSpace())
            {
                return BuySellDocStateModifierENUM.Unknown;
            }
            else
            {
                BuySellDocStateModifierENUM buySellDocStateModifierEnum;
                bool success = Enum.TryParse(ButtonValue, out buySellDocStateModifierEnum);

                if (success)
                {
                    return buySellDocStateModifierEnum;
                }
                else
                {
                    throw new Exception("Unable to get Document Modifier");
                }


            }
        }


    }
}
