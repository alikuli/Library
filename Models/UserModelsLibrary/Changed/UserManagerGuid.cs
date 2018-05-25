using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace UserModelsLibrary.Models.Changed
{
    // Summary:
    //     UserManager for users where the primary key for the User is of type string
    //
    // Type parameters:
    //   TUser:
    public class UserManagerGuid<TUser> : UserManager<TUser, Guid> where TUser : class, Microsoft.AspNet.Identity.IUser<Guid>
    {
        // Summary:
        //     Constructor
        //
        // Parameters:
        //   store:
        public UserManagerGuid(IUserStoreGuid<TUser> store) : base(store) { }


    }
}