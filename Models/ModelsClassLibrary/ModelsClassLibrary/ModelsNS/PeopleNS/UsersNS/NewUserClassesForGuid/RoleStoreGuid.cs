//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS;
//using ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS.NewUserClassesForGuid;

//namespace ModelsClassLibrary.ModelsNS.PeopleNS.NewUserClassesForGuid
//{
//    public class RoleStoreGuid<TRole> : RoleStore<TRole, Guid, IdentityUserRoleGuid>, IQueryableRoleStoreGuid<TRole>, IQueryableRoleStore<TRole, Guid>, IRoleStore<TRole, Guid>, IDisposable where TRole : IdentityRoleGuid, new()
//    {
//        // Summary:
//        //     Constructor
//        public RoleStoreGuid(): base(new ApplicationDbContext()){}
//        //
//        // Summary:
//        //     Constructor
//        //
//        // Parameters:
//        //   context:
//        public RoleStoreGuid(DbContext context) : base(context) { }
//    }
//}