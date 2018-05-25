using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace UserModelsLibrary.ModelsNS.Changed
{
    // Summary:
    //     UserManager for users where the primary key for the User is of type string
    //
    // Type parameters:
    //   TUser:
    public class UserManagerGuid<TUser> : UserManager<TUser, Guid>, IUserManagerGuid<TUser, Guid>
        where TUser : class, Microsoft.AspNet.Identity.IUser<Guid>
    {
        // Summary:
        //     Constructor
        //
        // Parameters:
        //   store:
        public UserManagerGuid(IUserStoreGuid<TUser> store) : base(store) { }



        #region IUserManagerGuid<TUser,Guid> Members


        public new void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public new System.Threading.Tasks.Task<IdentityResult> UpdatePassword(IUserPasswordStore<TUser, Guid> passwordStore, TUser user, string newPassword)
        {
            return base.UpdatePassword(passwordStore, user, newPassword);
        }

        public new System.Threading.Tasks.Task<bool> VerifyPasswordAsync(IUserPasswordStore<TUser, Guid> store, TUser user, string password)
        {
            return VerifyPasswordAsync(store, user, password);
        }

        #endregion
    }
}