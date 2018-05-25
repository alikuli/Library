using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.NewUserClassesForGuid
{
    // Summary:
    //     Interface that exposes an IQueryable roles
    //
    // Type parameters:
    //   TRole:
    public interface IQueryableRoleStoreGuid<TRole> : IQueryableRoleStore<TRole, Guid>, IRoleStore<TRole, Guid>, IDisposable where TRole : Microsoft.AspNet.Identity.IRole<Guid>
    {
    }
}
