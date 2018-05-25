
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS.NewUserClassesForGuid
{
    public class IdentityRoleGuid : IdentityRole<Guid, IdentityUserRoleGuid>
    {
        // Summary:
        //     Constructor
        public IdentityRoleGuid()
        {
            Id = Guid.NewGuid();
        }
        //
        // Summary:
        //     Constructor
        //
        // Parameters:
        //   roleName:
        public IdentityRoleGuid(string roleName): this()
        {
            Name = roleName;
        }
    }
}
