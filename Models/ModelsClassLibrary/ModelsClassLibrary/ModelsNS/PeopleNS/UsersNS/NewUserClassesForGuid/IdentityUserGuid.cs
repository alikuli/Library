using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS.NewUserClassesForGuid
{
    public class IdentityUserGuid : IdentityUser<Guid, IdentityUserLoginGuid, IdentityUserRoleGuid, IdentityUserClaimGuid>, IUserGuid, IUser<Guid>
    {
        //todo Changed IdentityUserLogin
    }
}