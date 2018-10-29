using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.AddressNS.AddressVerificationTrxNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesDocsNS;
using ModelsClassLibrary.ModelsNS.GlobalCommentsNS;
using ModelsClassLibrary.ModelsNS.LikeUnlikeNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace UserModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser, ICommonWithId, IUserHasUploads
    {

        [Display(Name = "Country")]
        public string CountryId { get; set; }
        public virtual Country Country { get; set; }

        public string MailerId { get; set; }

        public virtual Mailer Mailer { get; set; }


        //public ICollection<AddressVerificationTrx> AddressVerificationTrxs { get; set; }
        //public ICollection<Right> UserRights { get; set; }


        public virtual ICollection<UploadedFile> MiscFiles { get; set; }

        public string MiscFilesLocation()
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



        public virtual ICollection<FileDoc> FileDocs { get; set; }



        /// <summary>
        /// Every user can have many addresses.
        /// </summary>
        public virtual ICollection<AddressWithId> Addresses { get; set; }


        public virtual ICollection<ProductChild> ProductChildren { get; set; }


        public virtual ICollection<GlobalComment> GlobalComments { get; set; }


        public virtual ICollection<LikeUnlike> LikeUnlikes { get; set; }

        public bool DisableNameInView()
        {
            return false;
        }


        [NotMapped]
        public string DefaultDisplayImage { get { return AliKuli.ConstantsNS.MyConstants.DEFAULT_IMAGE_LOCATION; } }



        public bool IsAllowDuplicates
        {
            get { return true; }
        }

    }
}
