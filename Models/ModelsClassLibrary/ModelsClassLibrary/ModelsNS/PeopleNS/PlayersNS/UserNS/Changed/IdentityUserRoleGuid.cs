using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModelsLibrary.ModelsNS.Changed
{
    public class IdentityUserRoleGuid : IdentityUserRole<Guid>
    {
        public IdentityUserRoleGuid()
            : base()
        {
        }
    }
}