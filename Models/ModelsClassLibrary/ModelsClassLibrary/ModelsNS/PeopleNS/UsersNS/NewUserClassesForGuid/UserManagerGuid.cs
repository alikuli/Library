using System;
using Microsoft.AspNet.Identity;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS.NewUserClassesForGuid
{
    public class UserManagerGuid<TUser> : UserManager<TUser, Guid> where TUser : class, Microsoft.AspNet.Identity.IUser<Guid>
    {
        public UserManagerGuid(IUserStore<TUser, Guid> userStore)
            : base(userStore)
        {

        }
    }
}