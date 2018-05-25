using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModelsLibrary.Models.Changed
{
    // Summary:
    //     Represents a Role entity
    public class IdentityRoleGuid : IdentityRole<Guid, IdentityUserRoleGuid>
    {
        // Summary:
        //     Constructor
        public IdentityRoleGuid() : base() { }
        //
        // Summary:
        //     Constructor
        //
        // Parameters:
        //   roleName:
        public IdentityRoleGuid(string roleName)
            : base()
        {
            Name = roleName;
        }
    }

}