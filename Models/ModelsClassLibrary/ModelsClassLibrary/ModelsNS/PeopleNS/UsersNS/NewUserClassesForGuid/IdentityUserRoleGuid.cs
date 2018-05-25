using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS
{
    public class IdentityUserRoleGuid : IdentityUserRole<Guid>
    {
        public IdentityUserRoleGuid(): base()
        {

        }
    }
}