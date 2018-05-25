using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ModelsClassLibrary.ModelsNS.UserModels.Changed
{
    public interface ISignInManager<TUser, TKey> : IDisposable
        where TUser : class, Microsoft.AspNet.Identity.IUser<TKey>
        where TKey : System.IEquatable<TKey>
    {


        IAuthenticationManager AuthenticationManager { get; set; }
        string AuthenticationType { get; set; }
        UserManager<TUser, TKey> UserManager { get; set; }

        TKey ConvertIdFromString(string id);
        string ConvertIdToString(TKey id);
        Task<ClaimsIdentity> CreateUserIdentityAsync(TUser user);
        void Dispose(bool disposing);

        Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent);

        Task<TKey> GetVerifiedUserIdAsync();

        Task<bool> HasBeenVerifiedAsync();

        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);

        Task<bool> SendTwoFactorCodeAsync(string provider);

        Task SignInAsync(TUser user, bool isPersistent, bool rememberBrowser);

        Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberBrowser);
    }
}
