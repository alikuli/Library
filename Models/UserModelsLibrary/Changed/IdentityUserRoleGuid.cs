using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModelsLibrary.Models.Changed
{
    public class IdentityUserRoleGuid : IdentityUserRole<Guid>
    {
        public IdentityUserRoleGuid()
            : base()
        {

        }
    }
}