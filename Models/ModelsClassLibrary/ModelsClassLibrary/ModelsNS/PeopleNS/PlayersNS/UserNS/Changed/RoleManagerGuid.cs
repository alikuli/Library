using System;
using Microsoft.AspNet.Identity;

namespace ModelsClassLibrary.ModelsNS.UserModels.Changed
{


    // Summary:
    //     Exposes role related api which will automatically save changes to the RoleStore
    //
    // Type parameters:
    //   TRole:
    public class RoleManagerGuid<TRole> : RoleManager<TRole, Guid> where TRole : class, Microsoft.AspNet.Identity.IRole<Guid>
    {
        // Summary:
        //     Constructor
        //
        // Parameters:
        //   store:
        public RoleManagerGuid(IRoleStore<TRole, Guid> store) : base(store) { }
    }
}
