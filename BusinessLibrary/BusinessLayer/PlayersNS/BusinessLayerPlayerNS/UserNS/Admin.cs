using AliKuli.Extentions;
using AliKuli.UtilitiesNS;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using UserModels;
namespace UowLibrary
{
    public partial class UserBiz : BusinessLayer<ApplicationUser>
    {
        ////private ApplicationSignInManager _signInManager;
        ////private ApplicationUserManager _userManager;
        //private IErrorSet _ierrorSet;
        //private IRepositry<Country> _icountryDAL;
        //private IRepositry<ApplicationUser> _iuserDAL;

        //private IAuthenticationManager _iAuthenticationManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IRoleStore<IdentityRole, string> _store;
        //private RightBiz _rightBiz;
        ////public AccountController()
        ////{
        ////}

        //public UserBiz(
        //    ApplicationDbContext db,
        //    ApplicationUserManager userManager,
        //    ApplicationSignInManager signInManager,
        //    IAuthenticationManager iAuthenticationManager,
        //    IErrorSet ierrSet,
        //    IRepositry<Country> icountryDAL,
        //    IRepositry<ApplicationUser> iuserDAL,
        //    IMemoryMain memoryMain,
        //    IErrorSet errorSet,
        //    ConfigManagerHelper configManagerHelper,
        //    RightBiz rightBiz)
        //    : base(iuserDAL, memoryMain, ierrSet, iuserDAL, db, configManagerHelper)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //    _ierrorSet = ierrSet;
        //    _icountryDAL = icountryDAL;
        //    _iuserDAL = iuserDAL;
        //    _iAuthenticationManager = iAuthenticationManager;

        //    _store = new RoleStore<IdentityRole>();
        //    _roleManager = new RoleManager<IdentityRole>(_store);
        //    _rightBiz = rightBiz;

        //}

        //public ApplicationSignInManager SignInManager { get; set; }
        //public ApplicationUserManager UserManager { get; set; }
        //public RoleManager<IdentityRole> RoleManager { get { return _roleManager; } }
        //public IAuthenticationManager AuthenticationManager { get { return _iAuthenticationManager; } }

        //public IEnumerable<AuthenticationDescription> GetExternalAuthenticationTypes
        //{
        //    get
        //    {
        //        return AuthenticationManager.GetExternalAuthenticationTypes();
        //    }
        //}
        ////public string UserName { get; set; }
        ////public string UserId { get; set; }

        ////public ErrorSet ErrorsGlobal
        ////{
        ////    get
        ////    {
        ////        if (_ierrorSet.IsNull())
        ////            throw new Exception("ErrorSet is null. UserDAL. " + MethodBase.GetCurrentMethod().Name);

        ////        return (ErrorSet)_ierrorSet;

        ////    }
        ////}
        //private Repositry<Country> CountryDal
        //{
        //    get
        //    {
        //        if (_icountryDAL.IsNull())
        //        {
        //            ErrorsGlobal.Add("Country DAL is null.", MethodBase.GetCurrentMethod());
        //            throw new Exception(ErrorsGlobal.ToString());

        //        }

        //        return (Repositry<Country>)_icountryDAL;
        //    }
        //}

        ////private Repositry<ApplicationUser> UserDal
        ////{
        ////    get
        ////    {
        ////        if (_iuserDAL.IsNull())
        ////        {
        ////            ErrorsGlobal.Add("User DAL is null.", MethodBase.GetCurrentMethod());
        ////            throw new Exception(ErrorsGlobal.ToString());

        ////        }

        ////        return (Repositry<ApplicationUser>)_icountryDAL;
        ////    }
        ////}
        //public async Task<string> GenerateChangePhoneNumberTokenAsync(string userId, string phoneNumber)
        //{
        //    string code = await UserManager.GenerateChangePhoneNumberTokenAsync(userId, phoneNumber);
        //    return code;
        //}



        //public override SelectList SelectList()
        //{
        //    if (this.IsNull())
        //        return null;

        //    var allUsers = UserManager.Users.ToList();

        //    if (allUsers.IsNull() || allUsers.Count() == 0)
        //        return null;

        //    var sortedList = allUsers.OrderBy(x => x.UserName)
        //        .Select(x =>
        //        new
        //        {
        //            Text = x.UserName,
        //            Value = x.Id
        //        })
        //        .ToList();
        //    var selectList = new SelectList(sortedList, "Value", "Text");
        //    return selectList;
        //}



        //public ApplicationUser FindById_UserManager(string userId)
        //{
        //    ApplicationUser user = UserManager.FindById(userId);
        //    return user;
        //}

        //public async Task<bool> GetTwoFactorBrowserRememberedAsync(string userId)
        //{
        //    return await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId);
        //}


        //private void throwErrorUserIsNullOrWhiteSpace(string userName)
        //{
        //    if (userName.IsNullOrWhiteSpace())
        //    {
        //        ErrorsGlobal.Add("User name is empty", "Creating User.");
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }
        //}

        ////public IdentityRole CreateRole_UserManager(string roleName)
        //{
        //    try
        //    {

        //        //first check to see if role exists...
        //        if (RoleManager.RoleExists(roleName))
        //            return RoleManager.FindByName(roleName);

        //        IdentityRole role = new IdentityRole(roleName);
        //        RoleManager.Create(role);
        //        return role;
        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add("Unable to create role.", MethodBase.GetCurrentMethod(), e);
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }

        //}

        //public void AddUserToRole_UserManager(string userId, string roleName)
        //{
        //    try
        //    {
        //        UserManager.AddToRole(userId, roleName);
        //    }
        //    catch (Exception e)
        //    {

        //        ErrorsGlobal.Add(string.Format("Unable to add user: '{0}' to role: '{1}'.", userId, roleName), MethodBase.GetCurrentMethod(), e);
        //        throw new Exception(ErrorsGlobal.ToString());
        //    }
        //}


        //public override string SelectListCacheKey
        //{
        //    get { throw new NotImplementedException(); }
        //}


        //public void InitializeSystem()
        //{
        //    InitializeAdministrator();
        //    createDefaultRightsForAnnonymous(_rightBiz.FactoryCountryRightDefault(null));

        //}
        //private void createDefaultRightsForAnnonymous(Right r)
        //{
        //    try
        //    {
        //        _db.Rights.Add(r);
        //        _db.SaveChanges();
        //    }
        //    catch (Exception )
        //    {

        //    }
        //}
        private ApplicationUser InitializeAdministrator()
        {
            IdentityRole role = initializeAdminRole();
            ApplicationUser user = initializeAdminUser();
            addAdminToAdminRole(user.Id, role.Name);
            return user;
        }

        private void addAdminToAdminRole(string userId, string roleName)
        {
            AddUserToRole_UserManager(userId, roleName);
        }

        private ApplicationUser initializeAdminUser()
        {
            string adminName = ConfigManagerHelper.AdminName;
            string password = ConfigManagerHelper.AdminPassword;
            ApplicationUser user = CreateUser_UserManager(adminName, password);

            return user;
        }

        private IdentityRole initializeAdminRole()
        {
            string adminRole = ConfigManagerHelper.AdminRole;
            IdentityRole role = CreateRole_UserManager(adminRole);
            return role;

        }

        public bool IsAdmin(string userId)
        {
            ApplicationUser user = UserManager.FindById(userId);
            if (user.IsNull())
                return false;

            string adminRole = ConfigManagerHelper.AdminRole;
            bool isAdmin = UserManager.IsInRole(userId, adminRole);
            return isAdmin;

        }

        public List<ApplicationUser> GetAllAdmin()
        {
            List<ApplicationUser> allUserList = FindAll().ToList();

            List<ApplicationUser> allAdminList = new List<ApplicationUser>();
            if (!allUserList.IsNullOrEmpty())
            {
                foreach (ApplicationUser user in allUserList)
                {
                    if (IsAdmin(user.Id))
                        allUserList.Add(user);
                }
            }
            return allAdminList;
        }

        //public bool IsAdmin(string userId)
        //{
        //    if (userId.IsNullOrWhiteSpace())
        //        return false;
        //    return UserManager.IsInRole(userId, ConfigManagerHelper.AdminRole);
        //}

    }
}
