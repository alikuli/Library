using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace UserModelsLibrary.ModelsNS.Changed
{
    public interface IUserStoreGuid<TUser> : IUserStore<TUser, Guid>, IDisposable where TUser : class, Microsoft.AspNet.Identity.IUser<Guid>
    {
    }

    // Summary:
    //     Interface that exposes basic user management apis
    //
    // Type parameters:
    //   TUser:
    //
    //   TKey:
    public interface IUserStoreGuid<TUser, in TKey> : IDisposable where TUser : class, Microsoft.AspNet.Identity.IUser<TKey>
    {
        // Summary:
        //     Insert a new user
        //
        // Parameters:
        //   user:
        Task CreateAsync(TUser user);
        //
        // Summary:
        //     Delete a user
        //
        // Parameters:
        //   user:
        Task DeleteAsync(TUser user);
        //
        // Summary:
        //     Finds a user
        //
        // Parameters:
        //   userId:
        Task<TUser> FindByIdAsync(TKey userId);
        //
        // Summary:
        //     Find a user by name
        //
        // Parameters:
        //   userName:
        Task<TUser> FindByNameAsync(string userName);
        //
        // Summary:
        //     Update a user
        //
        // Parameters:
        //   user:
        Task UpdateAsync(TUser user);
    }
}