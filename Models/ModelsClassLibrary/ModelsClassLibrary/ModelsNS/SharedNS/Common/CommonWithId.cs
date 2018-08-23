using AliKuli.Extentions;
using EnumLibrary.EnumNS;
using InterfacesLibrary.SharedNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelsClassLibrary.ModelsNS.SharedNS
{
    /// <summary>
    /// To make a unique name for the class use MakeUniqueName(). The framework will then
    /// automatically check for it.
    /// </summary>
    public abstract class CommonWithId : ICommonWithId, IIndexListItems, IFieldsToLoadFromView
    {

        /// <summary>
        /// If true, then duplicates are allowed.
        /// </summary>
        [NotMapped]
        public virtual bool IsAllowDuplicates { get { return false; } }

        public CommonWithId()
        {
            MetaData = new MetaDataComplex();
            Id = CreateNewId();
            DetailInfoToDisplayOnWebsite = "";
            Comment = "";

        }

        /// <summary>
        /// Marked true if creating
        /// </summary>
        [NotMapped]
        public bool IsCreating { get; set; }

        /// <summary>
        /// Marked true if deleting
        /// </summary>
        [NotMapped]
        public bool IsDeleting { get; set; }

        /// <summary>
        /// This controls the Menus.
        /// </summary>
        [NotMapped]
        public IMenuManager MenuManager { get; set; }


        /// <summary>
        /// Marked true if updating
        /// </summary>
        [NotMapped]
        public bool IsUpdating { get; set; }


        /// <summary>
        /// Use this class to create your own ID value.
        /// </summary>
        /// <returns></returns>
        public virtual string CreateNewId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// This contains storage rules for the record.
        ///     Encryption
        ///     Unique Record
        /// </summary>
        //public DbBoolsStorageComplex DbBoolsStorageRules { get; set; }

        #region Properties
        [Key]
        public virtual string Id { get; set; }



        /// <summary>
        /// This is comments that the website owner/user can use to keep information about the entity. This is NOT displayed 
        /// on the website at customer level.
        /// </summary>
        [MaxLength(1000, ErrorMessage = "Max length allowed is {0} charecters")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        /// <summary>
        /// Add item's detailed information here. This is being used in the categories. Basically
        /// This is information you want to display to the customers
        /// </summary>
        /// 
        [Display(Name = "Detail Info")]
        public string DetailInfoToDisplayOnWebsite { get; set; }

        //[Required]
        //[Display(Name = "Name for Reference")]
        [MaxLength(2000, ErrorMessage = "Max length allowed in {0} is {1} charecters")]
        /// <summary>
        /// The name can be empty. In cases such as States, sometimes a country does not have a state. So then we use the country
        /// but so that we can connect the city with the country we are allowing blank states
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Use this if you want to redirect to some other controler/action. I am currently using this in delete.
        /// When I delete an uploaded image, I would like the view to go back to the original location eg. In ProductCategory1
        /// If I delete an Image, I would like to end up back in the Edit of ProductCategory1. 
        /// This needs to be shown as a huidden field in all the views. So, that has been done, currently in Delete and Edit.
        /// It will have to done on a need to basis in the _FieldsOnlyEditFormat.cshtml files.
        /// </summary>
        [NotMapped]
        public string ReturnUrl { get; set; }

        public MetaDataComplex MetaData { get; set; }

        /// <summary>
        /// This transfers the changed fields to the Db Record. Remember to run the base as well because it contains:
        ///     DetailInfoToDisplayOnWebsite = icommonWithId.DetailInfoToDisplayOnWebsite;
        ///     MetaData.Modified.DateStart = icommonWithId.MetaData.Modified.DateStart;
        ///     Name = icommonWithId.Name;
        ///     ReturnUrl = icommonWithId.ReturnUrl;
        ///     Note. You can control who updates what from here.
        ///     No need to exchange fields that have not been changed.
        /// </summary>
        public virtual void UpdatePropertiesDuringModify(ICommonWithId icommonWithId)
        {

            MetaData.Modified.DateStart = icommonWithId.MetaData.Modified.DateStart;

            DetailInfoToDisplayOnWebsite = icommonWithId.DetailInfoToDisplayOnWebsite;
            Name = icommonWithId.Name;
            ReturnUrl = icommonWithId.ReturnUrl;
            Comment = icommonWithId.Comment;

        }

        #endregion


        #region SelfErrorChecks
        public virtual void SelfErrorCheck()
        {

            Check_NameIsNullOrEmpty();
        }

        #region Checks
        private void Check_NameIsNullOrEmpty()
        {
            //if (Name.IsNullOrWhiteSpace())
            //    throw new ErrorHandlerLibrary.ExceptionsNS.RequiredDataMissingException("Name is empty. CommonWithId.");
            Name.IsNullOrWhiteSpaceThrowException();
        }


        #endregion


        #endregion

        #region IdString



        /// <summary>
        /// You can switch off Sentance case for name from here
        /// </summary>
        public virtual bool IsAllowNameToBeSentanceCased { get { return true; } }


        #endregion


        #region MakeName...
        /// <summary>
        /// This makes the name. The default is the Id String.
        /// </summary>
        public virtual string MakeUniqueName()
        {
            if (Name.IsNullOrEmpty())
                return Id;
            else
                return Name;
        }

        public virtual string FullName()
        {
            return Name;
        }

        #endregion

        #region Loaders
        /// <summary>
        /// Every class will have to define it's own Loader.
        /// </summary>
        /// <param name="c"></param>
        public void LoadFrom(ICommonWithId c)
        {
            MetaData.LoadFrom((IMetaData)c);

            Id = c.Id;
            Name = c.Name;
        }


        #endregion


        #region IIndexListItems Members

        public virtual string Input1SortString
        {
            get
            {
                return FullName();
            }
        }

        public virtual string Input2SortString
        {
            get { return ""; }
        }

        public virtual string Input3SortString
        {
            get { return ""; }
        }

        #endregion

        #region IIndexListItems Members

        public virtual string NameInput1
        {
            get { return "Name"; }
        }

        public virtual string NameInput2
        {
            get { return ""; }
        }

        public virtual string NameInput3
        {
            get { return ""; }
        }

        #endregion


        public virtual List<string> FieldsToLoadFromView()
        {
            List<string> lst = new List<string>();

            lst.Add("Comment");
            lst.Add("DetailInfoToDisplayOnWebsite");
            lst.Add("Name");

            return lst;
        }

        public int ClassNameForRightsVal()
        {
            return (int)ClassNameForRights();
        }

        public virtual ClassesWithRightsENUM ClassNameForRights()
        {
            string error = string.Format("NOT IMPLEMENTED! You have not set Classes With Rights ENUM for: {0}. ", this.GetType().Name);

            throw new Exception(error);
        }

        /// <summary>
        /// When true this will hide the name in all views.
        /// </summary>
        /// <returns></returns>
        public virtual bool HideNameInView()
        {
            return false;
        }

        public virtual bool DisableNameInView()
        {
            return false;
        }

        public string ClassNameRaw
        {
            get
            {
                return ClassNameForRights().ToString();
            }
        }
        public string ClassName
        {
            get
            {
                return ClassNameRaw.ToTitleSentance();
            }
        }
        public virtual string ClassNamePlural
        {
            get
            {
                if (ClassName.IsNullOrWhiteSpace())
                    return "";

                return ClassName + "s";
            }
        }


        [NotMapped]
        public string DefaultDisplayImage { get { return AliKuli.ConstantsNS.MyConstants.DEFAULT_IMAGE_LOCATION; } }
    }

}