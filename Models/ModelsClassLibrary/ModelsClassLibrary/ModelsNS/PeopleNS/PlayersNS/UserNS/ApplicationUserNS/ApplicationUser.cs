using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.MenuNS.MenuManagerNS.MenuStateNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.UserNameSpace;
using ModelsClassLibrary.ModelsNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UserModels
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public partial class ApplicationUser : IdentityUser, ICommonWithId
    {
        //todo I have removed the will cascade delete from all the IUserHaveUploads except one.... will all get deleted? This needs to be checked.
        public ApplicationUser()
        {
            AddressComplex = new AddressComplex();
            PersonComplex = new PersonComplex();
            IsActive = false;
            BlackListed = new UserActive();
            Suspended = new UserActive();
            MetaData = new MetaDataComplex();
            //Country = new Country();
        }

        //[Display(Name = "Country")]
        //public string CountryId { get; set; }
        //public Country Country { get; set; }
        //public string CountryAbbreviation
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //        //if (Country.IsNull())
        //        //{
        //        //    throw new Exception("No Country found. ApplicationUser.");
        //        //}
        //        //return Country.Abbreviation;
        //    }
        //}
        [Display(Name = "Phone")]
        [MaxLength(50, ErrorMessage = "Max length allowed is {0} charecters")]

        public string PhoneNumberAsEntered { get; set; }
        public string CountryIdCardNumber { get; set; }
        public AddressComplex AddressComplex { get; set; }
        public PersonComplex PersonComplex { get; set; }
        [Display(Name = "Active?")]
        public bool IsActive { get; set; }

        [Display(Name = "Black Listed?")]
        public UserActive BlackListed { get; set; }

        [Display(Name = "Suspended?")]
        public UserActive Suspended { get; set; }


        [NotMapped]
        public bool SuspendedOldValue { get; set; }
        [NotMapped]
        public bool BlackListOldValue { get; set; }


        //[Display(Name = "Default Bill Address")]
        //[MaxLength(128)]
        //public virtual string DefaultBillAddressId { get; set; }
        //public virtual AddressWithId DefaultBillAddress { get; set; }



        //[NotMapped]
        //public SelectList SelectListDefaultBillAddress { get; set; }

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


        [NotMapped]
        public IMenuManager MenuManager { get; set; }

       



    }
}
