using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.NewUserClassesForGuid
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
        public RoleManagerGuid(IRoleStore<TRole, Guid> store): base(store){}
    }
}