using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModelsLibrary.ModelsNS.Changed
{
    public class IdentityUserGuid : IdentityUser<Guid, IdentityUserLoginGuid, IdentityUserRoleGuid, IdentityUserClaimGuid>, IUserGuid, IUserGuid<Guid>
    {
        // Summary:
        //     Constructor which creates a new Guid for the Id
        public IdentityUserGuid()
            : base()
        {
            Id = Guid.NewGuid();
        }
        //
        // Summary:
        //     Constructor that takes a userName
        //
        // Parameters:
        //   userName:
        public IdentityUserGuid(string userName)
            : base()
        {
            UserName = userName;
        }

    }
}