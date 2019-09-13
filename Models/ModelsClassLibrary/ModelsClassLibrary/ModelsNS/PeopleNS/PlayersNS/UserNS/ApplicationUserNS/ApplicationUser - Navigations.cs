using AliKuli.UtilitiesNS;
using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web.Mvc;

namespace UserModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser, ICommonWithId, IUserHasUploads
    {

        [NotMapped]
        public bool IsCreating { get; set; }
        [NotMapped]
        public bool IsDeleting { get; set; }
        [NotMapped]
        public bool IsEditing { get; set; }
        public LongWithDateComplex NoOfVisits { get; set; }

        //public virtual ICollection<Person> People { get; set; }

        [Display(Name = "Person")]
        [MaxLength(128)]
        public string PersonId { get; set; }
        public virtual Person Person { get; set; }

        [NotMapped]
        public SelectList SelectListPeople { get; set; }

        //[Display(Name = "Country")]
        //[MaxLength(128)]
        //public string CountryId { get; set; }
        //public virtual Country Country { get; set; }


        //https://stackoverflow.com/questions/44550386/one-to-one-relationship-with-different-primary-key-in-ef-6-1-code-first
        //this is not supported in EF
        //[MaxLength(128)]
        //public string MailerId { get; set; }

        //public virtual ICollection<Mailer> Mailers { get; set; }



        //public ICollection<AddressVerificationTrx> AddressVerificationTrxs { get; set; }
        //public ICollection<Right> UserRights { get; set; }


        public virtual ICollection<UploadedFile> MiscFiles { get; set; }

        public string MiscFilesLocation(string aName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "User");
        }


        public virtual ICollection<UploadedFile> SelfieUploads { get; set; }

        public string SelfieLocationConst(string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "Selfie");

        }



        public virtual ICollection<UploadedFile> IdCardFrontUploads { get; set; }

        public string IdCardFrontLocationConst(string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "IdCardfront");

        }



        public virtual ICollection<UploadedFile> IdCardBackUploads { get; set; }

        public string IdCardBackLocationConst(string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "IdCardBack");
        }



        public virtual ICollection<UploadedFile> PassportFrontUploads { get; set; }


        public string PassportFrontLocationConst(string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "PassportFront");

        }


        public virtual ICollection<UploadedFile> PassportVisaUploads { get; set; }

        public string PassportVisaLocationConst(string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "PassportVisa");

        }


        public virtual ICollection<UploadedFile> LiscenseFrontUploads { get; set; }

        public string LiscenseFrontLocationConst(string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "LiscenseFront");

        }

        public virtual ICollection<UploadedFile> LiscenseBackUploads { get; set; }

        public string LiscenseBackLocationConst(string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "LiscenseBack");
        }



        //public virtual ICollection<FileDoc> FileDocs { get; set; }



        /// <summary>
        /// Every user can have many addresses.
        /// </summary>
        //public virtual ICollection<AddressMain> Addresses { get; set; }


        //public virtual ICollection<ProductChild> ProductChildren { get; set; }


        //public virtual ICollection<GlobalComment> GlobalComments { get; set; }


        //public virtual ICollection<LikeUnlike> LikeUnlikes { get; set; }

        public bool DisableNameInView()
        {
            return false;
        }


        [NotMapped]
        public string DefaultDisplayImage
        {
            get
            {
                return ConfigManagerHelper.DefaultBlankPicture;
            }
        }



        public bool IsAllowDuplicates
        {
            get { return true; }
        }

       /// <summary>
        /// This is the heading in the Create form. Usually this will be Model.ClassName.ToString().ToTitleSentance()
        /// </summary>
        [NotMapped]
        
        public string HeadingForCreateForm { get; set; }
    }
}
