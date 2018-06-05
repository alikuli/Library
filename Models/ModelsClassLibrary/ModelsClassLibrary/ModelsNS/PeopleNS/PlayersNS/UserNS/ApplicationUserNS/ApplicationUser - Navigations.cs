using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.UserNameSpace;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.ProductChildNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using ModelsClassLibrary.ModelsNS.UploadedFileNS;
using ModelsClassLibrary.RightsNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UserModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser, ICommonWithId, IUserHasUploads
    {

        [Display(Name = "Country")]
        public string CountryId { get; set; }
        public Country Country { get; set; }


        public ICollection<Right> UserRights { get; set; }


        public virtual ICollection<UploadedFile> MiscFiles { get; set; }

        public string MiscFilesLocation ()
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY,"User");
        }

        public virtual ICollection<UploadedFile> SelfieUploads { get; set; }

        public string SelfieLocationConst (string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "Selfie");

        }

        public virtual ICollection<UploadedFile> IdCardFrontUploads { get; set; }
        
        public string IdCardFrontLocationConst(string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "IdCardfront");

        }

        public virtual ICollection<UploadedFile> IdCardBackUploads { get; set; }

        public string IdCardBackLocationConst (string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "IdCardBack");
        }

        public virtual ICollection<UploadedFile> PassportFrontUploads { get; set; }

        
        public string PassportFrontLocationConst (string userName)
        {
            return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "PassportFront");

        }


        public virtual ICollection<UploadedFile> PassportVisaUploads { get; set; }
        
        public string PassportVisaLocationConst (string userName)
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


        public virtual ICollection<ProductChild> ProductChildren { get; set; }



        //[NotMapped]


        //public bool IsCreating { get; set; }
        //[NotMapped]

        //public bool IsDeleting { get; set; }
        //[NotMapped]

        //public bool IsUpdating { get; set; }
    }
}
