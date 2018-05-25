using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS.NewUserClassesForGuid
{
    public class UserValidatorGuid<TUser> : UserValidator<TUser, Guid> where TUser : class, global::Microsoft.AspNet.Identity.IUser<Guid>
    {
              // Summary:
        //     Constructor
        //
        // Parameters:
        //   manager:
        public UserValidatorGuid(UserManager<TUser, Guid> manager) : base(manager){}
    }
}