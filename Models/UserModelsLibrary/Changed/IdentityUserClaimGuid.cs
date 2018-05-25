using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModelsLibrary.Models.Changed
{
    public class IdentityUserClaimGuid : IdentityUserClaim<Guid>
    {
        public IdentityUserClaimGuid()
            : base()
        {

        }
    }
}