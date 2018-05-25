using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using ErrorHandlerLibrary.ExceptionsNS;
using InterfacesLibrary.AddressNS;
using InterfacesLibrary.PeopleNS;
using InterfacesLibrary.PeopleNS.PlayersNS;
using InterfacesLibrary.SharedNS;
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.InterfacesNS.AddressNS;
using ModelsClassLibrary.ModelsNS.AddressNS;
using ModelsClassLibrary.ModelsNS.DocumentsNS.FilesNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.PlayersNS;
using ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS.NewUserClassesForGuid;
using ModelsClassLibrary.ModelsNS.PlacesNS;
using ModelsClassLibrary.ModelsNS.SharedNS;
using AliKuli.Extentions;

namespace ModelsClassLibrary.ModelsNS
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //I have encrypted the User fields (some of them)
    //To load values into the field use the field that is FieldName. When you give a value to this field name it will give an encrypted value to
    //the corresponding field name. When you get the value from it, it will get back the value from there.
    public class User : IdentityUserGuid, IUserCustom, IAddressComplexWithTown, ICommonWithId
    {
        #region Constructor

        private readonly ErrorSet _err;

        public User()
        {
            MetaData = new MetaDataComplex();
            Id = Guid.NewGuid();
            IsBlackList = false;
            EmailConfirmed = false;
            IsSuspeneded = false;

            FileCategories = new List<FileCategory>();
            FileDocs = new List<FileDoc>();

            Person = new PersonComplex(MetaData.IsEncrypted, MetaData.GetCreatedTicks);
            AddressStr = new AddressComplex(MetaData.IsEncrypted, MetaData.GetCreatedTicks);
            _err = new ErrorSet("ModelsClassLibrary", "User", "");
        }

        public User(
            PersonComplex person,
            IAddressWithId address,
            string userName,
            string comment,
            bool isAdmin)
            : this()
        {


            //Helper_CreateUserFor(
            //    person,
            //    address,
            //    userName,
            //    comment,
            //    isAdmin);


        }


        #endregion



        #region Address
        //The user will have the string version of the address and the Address as Id. After the string version is filled by the user,
        //the id version will be created and added from the information provided. I imagine the address will be created first, and then the user?

        public Guid? AddressId { get; set; }
        public IAddressWithId Address { get; set; }
        public AddressComplex AddressStr { get; set; }
        public Guid? TownId { get; set; }
        public Town Town { get; set; }


        #endregion

        //This is a complex field
        public PersonComplex Person { get; set; }


        public MetaDataComplex MetaData { get; set; }


        #region login Fields
        #region LastLogin

        [Column(TypeName = "DateTime2")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Login (UTC)")]
        public virtual DateTime? LastLogin { get; set; }


        #endregion
        #region LastLockout

        [Column(TypeName = "DateTime2")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Lockout (UTC)")]
        public virtual DateTime? LastLockout { get; set; }



        #endregion
        #region LastSignInFailure

        [Column(TypeName = "DateTime2")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Sign in Fail (UTC)")]
        public virtual DateTime? LastSignInFailure { get; set; }



        #endregion
        #region NoOfLogins


        [Display(Name = "# of Logins")]
        [DisplayFormat(DataFormatString = "{0:#,0}", ApplyFormatInEditMode = false)]
        public virtual int NoOfLogins { get; set; }


        #endregion
        //[Display(Name = "# of Logins")]
        //[DisplayFormat(DataFormatString = "{0:#,0}", ApplyFormatInEditMode = false)]
        //public int NoFailedLogins { get; set; }
        #region IpAddressOfLastLogin


        [Display(Name = "IP Address Of Last Login")]
        public virtual string IpAddressOfLastLogin { get; set; }

        #endregion
        #endregion




        #region User System Methods
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManagerGuid<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #endregion
        #region Navigation
        public ICollection<IAddressWithId> Addresses { get; set; }
        public ICollection<FileDoc> FileDocs { get; set; }
        public ICollection<FileCategory> FileCategories { get; set; }

        #endregion

        #region booleans
        //===============

        ///// <summary>
        ///// If this is true, then the data is encrypted in this record.
        ///// </summary>
        //[NotMapped]
        //public bool IsEncrypted { get; set; }

        /// <summary>
        /// This is used in the error. Default is false.
        /// </summary>
        [NotMapped]
        public bool IsAdmin { get; set; }

        //public bool IsActive { get; set; }



        /// <summary>
        /// If a person is blacklisted, they cannot be activated.
        /// </summary>
        [Display(Name = "BlackList User?")]
        public virtual bool IsBlackList { get; set; }


        /// <summary>
        /// Suspension is temporary
        /// </summary>
        [Display(Name = "Suspend User?")]
        public bool IsSuspeneded { get; set; }



        #endregion


        public void LoadFrom(User u)
        {
            //LoadFrom(u as IAddressWithId);
            LoadFrom(u as IMetaData);
            LastLogin = u.LastLogin;
            LastLockout = u.LastLockout;
            LastSignInFailure = u.LastSignInFailure;
            NoOfLogins = u.NoOfLogins;
            Person = u.Person;

            //Navigation
            Addresses = u.Addresses;

            FileDocs = u.FileDocs;
            FileCategories = u.FileCategories;
        }
        public void LoadFrom(IPerson p)
        {
            Person = (PersonComplex)p;
        }
        public void LoadFrom(IAddressString a)
        {
            AddressStr = (AddressComplex)a;
        }
        public void LoadFrom(IAddressStringWithTown Iaddress)
        {
            LoadFrom((IAddressString)Iaddress);
            this.TownId = Iaddress.TownId;
            this.Town = Iaddress.Town;
        }

        public void LoadFrom(IMetaData i)
        {
            IMetaData c = this as IMetaData;
            this.LoadFrom(i);
        }

        public void SelfErrorCheck()
        {
            Check_UserName();

            if (!IsAdmin)
            {
                //Convert User to IPerson...
                //Check_FathersNameOrHusbandsNameEntered();
                //Check_SonOfWifeOfField();
                //Check_Sex();
                //Check_NationalIdentificationNumber();
                //Check_FName();

                //AddressWithTownClass iawtc = new AddressWithTownClass(this);
                //iawtc.SelfErrorCheck();

            }

            if (_err.HasErrors)
                throw new Exception(_err.ToString());

        }
        private void Check_UserName()
        {
            if (UserName.IsNullOrEmpty())
            {
                _err.Add("User name ismissing.", "Check_UserName");
            }
        }




        #region IUserCustom Members


        public void LoadFrom(IUserCustom u)
        {
            throw new NotImplementedException();
        }


        #endregion






        #region ICommonWithId Members


        public string FullName()
        {
            return Person.PersonFullName();
        }


        public string IdString()
        {
            return Id.ToString();
        }

        public void LoadFrom(ICommonWithId c)
        {
            throw new NotImplementedException();
        }

        public string MakeUniqueName()
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }

}