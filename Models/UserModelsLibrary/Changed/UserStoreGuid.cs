using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModelsLibrary.Models.Changed
{
    // Summary:
    //     EntityFramework based user store implementation that supports IUserStore,
    //     IUserLoginStore, IUserClaimStore and IUserRoleStore
    //
    // Type parameters:
    //   TUser:
    public class UserStoreGuid<TUser> : UserStore<TUser, IdentityRoleGuid, Guid, IdentityUserLoginGuid, IdentityUserRoleGuid, IdentityUserClaimGuid>, IUserStoreGuid<TUser>, IUserStoreGuid<TUser, Guid>, IDisposable where TUser : IdentityUserGuid
    {
        // Summary:
        //     Default constuctor which uses a new instance of a default EntityyDbContext
        public UserStoreGuid()
            : base(new ApplicationDbContext()) { }
        //
        // Summary:
        //     Constructor
        //
        // Parameters:
        //   context:
        public UserStoreGuid(DbContext context) : base(context) { }
    }
}