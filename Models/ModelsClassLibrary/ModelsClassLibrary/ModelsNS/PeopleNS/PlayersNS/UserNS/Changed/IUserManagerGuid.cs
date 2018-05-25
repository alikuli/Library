using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace UserModelsLibrary.ModelsNS.Changed
{
    // Summary:
    //     Exposes user related api which will automatically save changes to the UserStore
    //
    // Type parameters:
    //   TUser:
    //
    //   TKey:
    public interface IUserManagerGuid<TUser, TKey> : IDisposable
        where TUser : class, Microsoft.AspNet.Identity.IUser<TKey>
        where TKey : System.IEquatable<TKey>
    {

        // Summary:
        //     Used to create claims identities from users
        IClaimsIdentityFactory<TUser, TKey> ClaimsIdentityFactory { get; set; }
        //
        // Summary:
        //     Default amount of time that a user is locked out for after MaxFailedAccessAttemptsBeforeLockout
        //     is reached
        TimeSpan DefaultAccountLockoutTimeSpan { get; set; }
        //
        // Summary:
        //     Used to send email
        IIdentityMessageService EmailService { get; set; }
        //
        // Summary:
        //     Number of access attempts allowed before a user is locked out (if lockout
        //     is enabled)
        int MaxFailedAccessAttemptsBeforeLockout { get; set; }
        //
        // Summary:
        //     Used to hash/verify passwords
        IPasswordHasher PasswordHasher { get; set; }
        //
        // Summary:
        //     Used to validate passwords before persisting changes
        IIdentityValidator<string> PasswordValidator { get; set; }
        //
        // Summary:
        //     Used to send a sms message
        IIdentityMessageService SmsService { get; set; }

        // Summary:
        //     Returns true if the store is an IQueryableUserStore
        bool SupportsQueryableUsers { get; }
        //
        // Summary:
        //     Returns true if the store is an IUserClaimStore
        bool SupportsUserClaim { get; }
        //
        // Summary:
        //     Returns true if the store is an IUserEmailStore
        bool SupportsUserEmail { get; }
        //
        // Summary:
        //     Returns true if the store is an IUserLockoutStore
        bool SupportsUserLockout { get; }
        //
        // Summary:
        //     Returns true if the store is an IUserLoginStore
        bool SupportsUserLogin { get; }
        //
        // Summary:
        //     Returns true if the store is an IUserPasswordStore
        bool SupportsUserPassword { get; }
        //
        // Summary:
        //     Returns true if the store is an IUserPhoneNumberStore
        bool SupportsUserPhoneNumber { get; }
        //
        // Summary:
        //     Returns true if the store is an IUserRoleStore
        bool SupportsUserRole { get; }
        //
        // Summary:
        //     Returns true if the store is an IUserSecurityStore
        bool SupportsUserSecurityStamp { get; }
        //
        // Summary:
        //     Returns true if the store is an IUserTwoFactorStore
        bool SupportsUserTwoFactor { get; }
        //
        // Summary:
        //     Maps the registered two-factor authentication providers for users by their
        //     id
        IDictionary<string, IUserTokenProvider<TUser, TKey>> TwoFactorProviders { get; }
        //
        // Summary:
        //     If true, will enable user lockout when users are created
        bool UserLockoutEnabledByDefault { get; set; }
        //
        // Summary:
        //     Returns an IQueryable of users if the store is an IQueryableUserStore
        IQueryable<TUser> Users { get; }
        //
        // Summary:
        //     Used for generating reset password and confirmation tokens
        IUserTokenProvider<TUser, TKey> UserTokenProvider { get; set; }
        //
        // Summary:
        //     Used to validate users before changes are saved
        IIdentityValidator<TUser> UserValidator { get; set; }

        // Summary:
        //     Increments the access failed count for the user and if the failed access
        //     account is greater than or equal to the MaxFailedAccessAttempsBeforeLockout,
        //     the user will be locked out for the next DefaultAccountLockoutTimeSpan and
        //     the AccessFailedCount will be reset to 0. This is used for locking out the
        //     user account.
        //
        // Parameters:
        //   userId:
        Task<IdentityResult> AccessFailedAsync(TKey userId);
        //
        // Summary:
        //     Add a user claim
        //
        // Parameters:
        //   userId:
        //
        //   claim:
        Task<IdentityResult> AddClaimAsync(TKey userId, Claim claim);
        //
        // Summary:
        //     Associate a login with a user
        //
        // Parameters:
        //   userId:
        //
        //   login:
        Task<IdentityResult> AddLoginAsync(TKey userId, UserLoginInfo login);
        //
        // Summary:
        //     Add a user password only if one does not already exist
        //
        // Parameters:
        //   userId:
        //
        //   password:

        Task<IdentityResult> AddPasswordAsync(TKey userId, string password);
        //
        // Summary:
        //     Add a user to a role
        //
        // Parameters:
        //   userId:
        //
        //   role:

        Task<IdentityResult> AddToRoleAsync(TKey userId, string role);
        //
        // Summary:
        //     Method to add user to multiple roles
        //
        // Parameters:
        //   userId:
        //     user id
        //
        //   roles:
        //     list of role names

        Task<IdentityResult> AddToRolesAsync(TKey userId, params string[] roles);
        //
        // Summary:
        //     Change a user password
        //
        // Parameters:
        //   userId:
        //
        //   currentPassword:
        //
        //   newPassword:

        Task<IdentityResult> ChangePasswordAsync(TKey userId, string currentPassword, string newPassword);
        //
        // Summary:
        //     Set a user's phoneNumber with the verification token
        //
        // Parameters:
        //   userId:
        //
        //   phoneNumber:
        //
        //   token:

        Task<IdentityResult> ChangePhoneNumberAsync(TKey userId, string phoneNumber, string token);
        //
        // Summary:
        //     Returns true if the password is valid for the user
        //
        // Parameters:
        //   user:
        //
        //   password:

        Task<bool> CheckPasswordAsync(TUser user, string password);
        //
        // Summary:
        //     Confirm the user's email with confirmation token
        //
        // Parameters:
        //   userId:
        //
        //   token:

        Task<IdentityResult> ConfirmEmailAsync(TKey userId, string token);
        //
        // Summary:
        //     Create a user with no password
        //
        // Parameters:
        //   user:

        Task<IdentityResult> CreateAsync(TUser user);
        //
        // Summary:
        //     Create a user with the given password
        //
        // Parameters:
        //   user:
        //
        //   password:

        Task<IdentityResult> CreateAsync(TUser user, string password);
        //
        // Summary:
        //     Creates a ClaimsIdentity representing the user
        //
        // Parameters:
        //   user:
        //
        //   authenticationType:
        Task<ClaimsIdentity> CreateIdentityAsync(TUser user, string authenticationType);
        //
        // Summary:
        //     Delete a user
        //
        // Parameters:
        //   user:

        Task<IdentityResult> DeleteAsync(TUser user);
        //
        // Summary:
        //     When disposing, actually dipose the store
        //
        // Parameters:
        //   disposing:
        void Dispose(bool disposing);
        //
        // Summary:
        //     Returns the user associated with this login
        Task<TUser> FindAsync(UserLoginInfo login);
        //
        // Summary:
        //     Return a user with the specified username and password or null if there is
        //     no match.
        //
        // Parameters:
        //   userName:
        //
        //   password:

        Task<TUser> FindAsync(string userName, string password);
        //
        // Summary:
        //     Find a user by his email
        //
        // Parameters:
        //   email:
        Task<TUser> FindByEmailAsync(string email);
        //
        // Summary:
        //     Find a user by id
        //
        // Parameters:
        //   userId:
        Task<TUser> FindByIdAsync(TKey userId);
        //
        // Summary:
        //     Find a user by user name
        //
        // Parameters:
        //   userName:
        Task<TUser> FindByNameAsync(string userName);
        //
        // Summary:
        //     Generate a code that the user can use to change their phone number to a specific
        //     number
        //
        // Parameters:
        //   userId:
        //
        //   phoneNumber:

        Task<string> GenerateChangePhoneNumberTokenAsync(TKey userId, string phoneNumber);
        //
        // Summary:
        //     Get the email confirmation token for the user
        //
        // Parameters:
        //   userId:
        Task<string> GenerateEmailConfirmationTokenAsync(TKey userId);
        //
        // Summary:
        //     Generate a password reset token for the user using the UserTokenProvider
        //
        // Parameters:
        //   userId:
        Task<string> GeneratePasswordResetTokenAsync(TKey userId);
        //
        // Summary:
        //     Get a token for a specific two factor provider
        //
        // Parameters:
        //   userId:
        //
        //   twoFactorProvider:

        Task<string> GenerateTwoFactorTokenAsync(TKey userId, string twoFactorProvider);
        //
        // Summary:
        //     Get a user token for a specific purpose
        //
        // Parameters:
        //   purpose:
        //
        //   userId:

        Task<string> GenerateUserTokenAsync(string purpose, TKey userId);
        //
        // Summary:
        //     Returns the number of failed access attempts for the user
        //
        // Parameters:
        //   userId:

        Task<int> GetAccessFailedCountAsync(TKey userId);
        //
        // Summary:
        //     Get a users's claims
        //
        // Parameters:
        //   userId:

        Task<IList<Claim>> GetClaimsAsync(TKey userId);
        //
        // Summary:
        //     Get a user's email
        //
        // Parameters:
        //   userId:

        Task<string> GetEmailAsync(TKey userId);
        //
        // Summary:
        //     Returns whether lockout is enabled for the user
        //
        // Parameters:
        //   userId:

        Task<bool> GetLockoutEnabledAsync(TKey userId);
        //
        // Summary:
        //     Returns when the user is no longer locked out, dates in the past are considered
        //     as not being locked out
        //
        // Parameters:
        //   userId:

        Task<DateTimeOffset> GetLockoutEndDateAsync(TKey userId);
        //
        // Summary:
        //     Gets the logins for a user.
        //
        // Parameters:
        //   userId:

        Task<IList<UserLoginInfo>> GetLoginsAsync(TKey userId);
        //
        // Summary:
        //     Get a user's phoneNumber
        //
        // Parameters:
        //   userId:

        Task<string> GetPhoneNumberAsync(TKey userId);
        //
        // Summary:
        //     Returns the roles for the user
        //
        // Parameters:
        //   userId:

        Task<IList<string>> GetRolesAsync(TKey userId);
        //
        // Summary:
        //     Returns the current security stamp for a user
        //
        // Parameters:
        //   userId:

        Task<string> GetSecurityStampAsync(TKey userId);
        //
        // Summary:
        //     Get whether two factor authentication is enabled for a user
        //
        // Parameters:
        //   userId:

        Task<bool> GetTwoFactorEnabledAsync(TKey userId);
        //
        // Summary:
        //     Returns a list of valid two factor providers for a user
        //
        // Parameters:
        //   userId:

        Task<IList<string>> GetValidTwoFactorProvidersAsync(TKey userId);
        //
        // Summary:
        //     Returns true if the user has a password
        //
        // Parameters:
        //   userId:

        Task<bool> HasPasswordAsync(TKey userId);
        //
        // Summary:
        //     Returns true if the user's email has been confirmed
        //
        // Parameters:
        //   userId:

        Task<bool> IsEmailConfirmedAsync(TKey userId);
        //
        // Summary:
        //     Returns true if the user is in the specified role
        //
        // Parameters:
        //   userId:
        //
        //   role:

        Task<bool> IsInRoleAsync(TKey userId, string role);
        //
        // Summary:
        //     Returns true if the user is locked out
        //
        // Parameters:
        //   userId:

        Task<bool> IsLockedOutAsync(TKey userId);
        //
        // Summary:
        //     Returns true if the user's phone number has been confirmed
        //
        // Parameters:
        //   userId:

        Task<bool> IsPhoneNumberConfirmedAsync(TKey userId);
        //
        // Summary:
        //     Notify a user with a token using a specific two-factor authentication provider's
        //     Notify method
        //
        // Parameters:
        //   userId:
        //
        //   twoFactorProvider:
        //
        //   token:

        Task<IdentityResult> NotifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token);
        //
        // Summary:
        //     Register a two factor authentication provider with the TwoFactorProviders
        //     mapping
        //
        // Parameters:
        //   twoFactorProvider:
        //
        //   provider:
        void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<TUser, TKey> provider);
        //
        // Summary:
        //     Remove a user claim
        //
        // Parameters:
        //   userId:
        //
        //   claim:

        Task<IdentityResult> RemoveClaimAsync(TKey userId, Claim claim);
        //
        // Summary:
        //     Remove a user from a role.
        //
        // Parameters:
        //   userId:
        //
        //   role:

        Task<IdentityResult> RemoveFromRoleAsync(TKey userId, string role);
        //
        // Summary:
        //     Remove user from multiple roles
        //
        // Parameters:
        //   userId:
        //     user id
        //
        //   roles:
        //     list of role names

        Task<IdentityResult> RemoveFromRolesAsync(TKey userId, params string[] roles);
        //
        // Summary:
        //     Remove a user login
        //
        // Parameters:
        //   userId:
        //
        //   login:

        Task<IdentityResult> RemoveLoginAsync(TKey userId, UserLoginInfo login);
        //
        // Summary:
        //     Remove a user's password
        //
        // Parameters:
        //   userId:

        Task<IdentityResult> RemovePasswordAsync(TKey userId);
        //
        // Summary:
        //     Resets the access failed count for the user to 0
        //
        // Parameters:
        //   userId:

        Task<IdentityResult> ResetAccessFailedCountAsync(TKey userId);
        //
        // Summary:
        //     Reset a user's password using a reset password token
        //
        // Parameters:
        //   userId:
        //
        //   token:
        //
        //   newPassword:

        Task<IdentityResult> ResetPasswordAsync(TKey userId, string token, string newPassword);
        //
        // Summary:
        //     Send an email to the user
        //
        // Parameters:
        //   userId:
        //
        //   subject:
        //
        //   body:

        Task SendEmailAsync(TKey userId, string subject, string body);
        //
        // Summary:
        //     Send a user a sms message
        //
        // Parameters:
        //   userId:
        //
        //   message:

        Task SendSmsAsync(TKey userId, string message);
        //
        // Summary:
        //     Set a user's email
        //
        // Parameters:
        //   userId:
        //
        //   email:

        Task<IdentityResult> SetEmailAsync(TKey userId, string email);
        //
        // Summary:
        //     Sets whether lockout is enabled for this user
        //
        // Parameters:
        //   userId:
        //
        //   enabled:

        Task<IdentityResult> SetLockoutEnabledAsync(TKey userId, bool enabled);
        //
        // Summary:
        //     Sets the when a user lockout ends
        //
        // Parameters:
        //   userId:
        //
        //   lockoutEnd:

        Task<IdentityResult> SetLockoutEndDateAsync(TKey userId, DateTimeOffset lockoutEnd);
        //
        // Summary:
        //     Set a user's phoneNumber
        //
        // Parameters:
        //   userId:
        //
        //   phoneNumber:

        Task<IdentityResult> SetPhoneNumberAsync(TKey userId, string phoneNumber);
        //
        // Summary:
        //     Set whether a user has two factor authentication enabled
        //
        // Parameters:
        //   userId:
        //
        //   enabled:

        Task<IdentityResult> SetTwoFactorEnabledAsync(TKey userId, bool enabled);
        //
        // Summary:
        //     Update a user
        //
        // Parameters:
        //   user:

        Task<IdentityResult> UpdateAsync(TUser user);

        Task<IdentityResult> UpdatePassword(IUserPasswordStore<TUser, TKey> passwordStore, TUser user, string newPassword);
        //
        // Summary:
        //     Generate a new security stamp for a user, used for SignOutEverywhere functionality
        //
        // Parameters:
        //   userId:

        Task<IdentityResult> UpdateSecurityStampAsync(TKey userId);
        //
        // Summary:
        //     Verify the code is valid for a specific user and for a specific phone number
        //
        // Parameters:
        //   userId:
        //
        //   token:
        //
        //   phoneNumber:

        Task<bool> VerifyChangePhoneNumberTokenAsync(TKey userId, string token, string phoneNumber);
        //
        // Summary:
        //     By default, retrieves the hashed password from the user store and calls PasswordHasher.VerifyHashPassword
        //
        // Parameters:
        //   store:
        //
        //   user:
        //
        //   password:

        Task<bool> VerifyPasswordAsync(IUserPasswordStore<TUser, TKey> store, TUser user, string password);
        //
        // Summary:
        //     Verify a two factor token with the specified provider
        //
        // Parameters:
        //   userId:
        //
        //   twoFactorProvider:
        //
        //   token:

        Task<bool> VerifyTwoFactorTokenAsync(TKey userId, string twoFactorProvider, string token);
        //
        // Summary:
        //     Verify a user token with the specified purpose
        //
        // Parameters:
        //   userId:
        //
        //   purpose:
        //
        //   token:

        Task<bool> VerifyUserTokenAsync(TKey userId, string purpose, string token);
    }

}
