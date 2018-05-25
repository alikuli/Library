using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS.NewUserClassesForGuid
{

    // Summary:
    //     Interface that exposes basic user management apis
    //
    // Type parameters:
    //   TUser:
    public interface IUserStoreGuid<TUser> : IUserStore<TUser, Guid>, IDisposable where  TUser : IdentityUserGuid
    {
    }

}
