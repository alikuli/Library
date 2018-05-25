using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AliKuli.Extentions;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.SharedNS;


namespace UserModelsLibrary.ModelsNS
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    /// <summary>
    /// The user will be created using strings for address. A letter will be sent by snail mail to the user from the program with a confirmation number.
    /// The user will have 6 months to confirm their address, after which they will become a full
    /// The UserName will be any unique name. This will be used for logging in.
    /// The PhoneNumber, Email will be kept for sending info. However, you cannot use the SalesManager to find them
    /// because it bypasses ObjectMaterialization for some reason
    /// </summary>
    public class User : IdentityUser
    {


        public User()
        {
            MetaData = new MetaDataComplex();
            MetaData.Created.DateStart = DateTime.UtcNow;

            PersonInfo = new PersonComplex();
            AddressComplex = new AddressComplex();
            IsBlackListed = false;
            IsSuspended = false;

            SnailMailDetail = new DateAndByComplex();
            BlackListDetail = new DateAndByComplex();
            SuspensionDetail = new DateAndByComplex();
            EmailConfirmDetail = new DateAndByComplex();
            PhoneNoConfirmDetail = new DateAndByComplex();

            _err = new ErrorSet();
            _err.SetLibAndClass(Assembly.GetCallingAssembly().GetName().Name, this.GetType().Name);
        }
        readonly ErrorSet _err;
        //todo change the UserManager Here.
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, string> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;

        }

        [NotMapped]
        [Display(Name = "Admin?")]

        public bool IsAdmin { get; set; }

        //[NotMapped]
        //public string UserNameUnEncrypted { get; set; }

        //[NotMapped]
        //public string EmailUnEncrypted { get; set; }

        public bool IsEncrypted { get; set; }

        public string PhoneNumberAsEntered { get; set; }
        public ErrorSet Errors_User { get { return _err; } }

        //public new string Email { get; set; }

        [Display(Name = "Personal Info")]
        public PersonComplex PersonInfo { get; set; }


        #region Address

        [Display(Name = "Address")]
        public AddressComplex AddressComplex { get; set; }
        public Guid? AddressId { get; set; }
        public virtual AddressWithId Address { get; set; }



        #endregion

        #region Bools

        /// <summary>
        /// If black listed, no services can be used. User is a proven felon. User cannot be reinitiated
        /// </summary>
        /// 
        [Display(Name = "Blacklisted?")]
        public bool IsBlackListed { get; set; }
        public DateAndByComplex BlackListDetail { get; set; }





        /// <summary>
        /// If suspended, user may be reinitiated.
        /// </summary>
        /// 
        [Display(Name = "Suspended?")]
        public bool IsSuspended { get; set; }
        public DateAndByComplex SuspensionDetail { get; set; }
        public DateAndByComplex EmailConfirmDetail { get; set; }
        public DateAndByComplex PhoneNoConfirmDetail { get; set; }

        /// <summary>
        /// If ok, both, IsSuspended and IsBlackListed are false.
        /// </summary>

        bool _isOk;
        public bool IsOk
        {
            get
            {
                _isOk = !IsBlackListed && !IsSuspended;
                return _isOk;
            }
        }

        #endregion
        #region SnailMailConfirmations

        [Display(Name = "Snail Mail Confirmed?")]
        public bool IsSnailMailAddressConfirmed { get; set; }

        [Display(Name = "SnailMail Confirmation #")]
        public string SnailMailConfirmationNumber { get; set; }

        [Display(Name = "Date SnailMail Confirmed")]
        public DateAndByComplex SnailMailDetail { get; set; }

        #endregion

        public MetaDataComplex MetaData { get; set; }



        public string Name { get; set; }




        //public new List<IdentityRoleGuid> Roles { get; set; }
        //public new List<Claim> Claims { get; set; }

        #region Address


        // Methods

        public void SelfErrorCheck()
        {
            if (UserName.IsNull())
                Errors_User.Add("User Name is empty", MethodBase.GetCurrentMethod());

            if (PhoneNumber.IsNull())
                Errors_User.Add("User Phone is empty", MethodBase.GetCurrentMethod());

            //if (Email.IsNull())
            //    Errors_User.Add("User Email is empty", MethodBase.GetCurrentMethod());

            //if (!IsAdmin)
            //    if (PersonInfo.IdentificationNo.IsNull())
            //        Errors_User.Add("User Country Id Number is empty", MethodBase.GetCurrentMethod());

        }

        public string MakeUniqueName()
        {
            return DateTime.UtcNow.Ticks.ToString();
        }

        public override string ToString()
        {
            string s = string.Format("Name: '{0}', Phone: '{1}', Email: '{2}'", UserName, PhoneNumber, Email);
            return s;
        }

        public string FullName()
        {
            return UserName;
        }

        public string IdString()
        {
            return Id.ToString();
        }

        public void LoadFrom(ICommonWithId c)
        {
            throw new NotImplementedException();
        }

        public virtual void LoadFrom(User user)
        {
            this.PersonInfo = user.PersonInfo;
            this.AddressComplex = user.AddressComplex;
            this.MetaData = user.MetaData;

            this.Email = user.Email;
            this.EmailConfirmDetail = user.EmailConfirmDetail;
            this.EmailConfirmed = user.EmailConfirmed;

            this.IsBlackListed = user.IsBlackListed;
            this.BlackListDetail = user.BlackListDetail;

            this.IsEncrypted = user.IsEncrypted;

            this.IsSnailMailAddressConfirmed = user.IsSnailMailAddressConfirmed;
            this.SnailMailConfirmationNumber = user.SnailMailConfirmationNumber;
            this.SnailMailDetail = user.SnailMailDetail;

            this.IsSuspended = user.IsSuspended;
            this.SuspensionDetail = user.SuspensionDetail;

            this.PhoneNumber = user.PhoneNumber;
            this.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            this.PhoneNoConfirmDetail = user.PhoneNoConfirmDetail;
            this.UserName = user.UserName;

            //this.UserNameUnEncrypted = user.UserNameUnEncrypted;
            //this.EmailUnEncrypted = user.EmailUnEncrypted;
            //todo incase you want EmailUnEncrypted

        }

        #endregion

        #region ICommonWithId Members



        #endregion
    }
}