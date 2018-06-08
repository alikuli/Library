using InterfacesLibrary.SharedNS;
using InterfacesLibrary.SharedNS.FeaturesNS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.UserNameSpace;
using ModelsClassLibrary.ModelsNS.PlacesNS;
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
        //todo I have removed the will cascade delete from all the IUserHaveUploads except one.... will all get deleted? This needs to be checked.
        public ApplicationUser()
        {
            AddressComplex = new AddressComplex();
            PersonComplex = new PersonComplex();
            IsActive = false;
            IsBlackListed = new UserActive();
            IsSuspended = new UserActive();
            MetaData = new MetaDataComplex();
            //Country = new Country();
        }

        //[Display(Name = "Country")]
        //public string CountryId { get; set; }
        //public Country Country { get; set; }
        public string CountryAbbreviation
        {
            get
            {
                throw new NotImplementedException();
                //if (Country.IsNull())
                //{
                //    throw new Exception("No Country found. ApplicationUser.");
                //}
                //return Country.Abbreviation;
            }
        }
        [Display(Name = "Phone")]
        [MaxLength(50, ErrorMessage = "Max length allowed is {0} charecters")]

        public string PhoneNumberAsEntered { get; set; }
        public string CountryIdCardNumber { get; set; }
        public AddressComplex AddressComplex { get; set; }
        public PersonComplex PersonComplex { get; set; }
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }

        [Display(Name = "Black Listed?")]
        public UserActive IsBlackListed { get; set; }

        [Display(Name = "Suspended?")]
        public UserActive IsSuspended { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Comment { get; set; }


        public string DetailInfoToDisplayOnWebsite { get; set; }


        public string FullName() { return UserName; }



        public string MakeUniqueName()
        {
            return Guid.NewGuid().ToString();
        }

        public MetaDataComplex MetaData { get; set; }


        public string Name { get; set; }




        public void SelfErrorCheck()
        {
            //throw new NotImplementedException();
        }

        public string ReturnUrl { get; set; }

        //public ICollection<Right> UserRights { get; set; }


        //public virtual ICollection<UploadedFile> MiscFiles { get; set; }

        //public string MiscFilesLocation ()
        //{
        //    return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY,"User");
        //}

        //public virtual ICollection<UploadedFile> SelfieUploads { get; set; }

        //public string SelfieLocationConst (string userName)
        //{
        //    return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "Selfie");

        //}

        //public virtual ICollection<UploadedFile> IdCardFrontUploads { get; set; }
        
        //public string IdCardFrontLocationConst(string userName)
        //{
        //    return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "IdCardfront");

        //}

        //public virtual ICollection<UploadedFile> IdCardBackUploads { get; set; }

        //public string IdCardBackLocationConst (string userName)
        //{
        //    return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "IdCardBack");
        //}

        //public virtual ICollection<UploadedFile> PassportFrontUploads { get; set; }

        
        //public string PassportFrontLocationConst (string userName)
        //{
        //    return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "PassportFront");

        //}


        //public virtual ICollection<UploadedFile> PassportVisaUploads { get; set; }
        
        //public string PassportVisaLocationConst (string userName)
        //{
        //    return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "PassportVisa");

        //}


        //public virtual ICollection<UploadedFile> LiscenseFrontUploads { get; set; }

        //public string LiscenseFrontLocationConst(string userName)
        //{
        //    return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "LiscenseFront");

        //}

        //public virtual ICollection<UploadedFile> LiscenseBackUploads { get; set; }

        //public string LiscenseBackLocationConst(string userName)
        //{
        //    return Path.Combine(AliKuli.ConstantsNS.MyConstants.SAVE_ROOT_DIRECTORY, "LiscenseBack");
        //}




    }
}
