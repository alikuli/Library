using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelsClassLibrary.ModelsNS.PeopleNS.UsersNS.NewUserClassesForGuid
{
    public class IdentityDbContextGuid<TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim> :  DbContext
        where TUser : global::Microsoft.AspNet.Identity.EntityFramework.IdentityUser<TKey, TUserLogin, TUserRole, TUserClaim>
        where TRole : global::Microsoft.AspNet.Identity.EntityFramework.IdentityRole<TKey, TUserRole>
        where TUserLogin : global::Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<TKey>
        where TUserRole : global::Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<TKey>
        where TUserClaim : global::Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<TKey>
    {
        // Summary:
        //     Default constructor which uses the "DefaultConnection" connectionString
        public IdentityDbContextGuid() :base(){ }
        //
        // Summary:
        //     Constructs a new context instance using conventions to create the name of
        //     the database to which a connection will be made, and initializes it from
        //     the given model. The by-convention name is the full name (namespace + class
        //     name) of the derived context class. See the class remarks for how this is
        //     used to create a connection.
        //
        // Parameters:
        //   model:
        //     The model that will back this context.
        public IdentityDbContextGuid(DbCompiledModel model) : base(model) { }
        //
        // Summary:
        //     Constructor which takes the connection string to use
        //
        // Parameters:
        //   nameOrConnectionString:
        public IdentityDbContextGuid(string nameOrConnectionString) : base(nameOrConnectionString) { }
        //
        // Summary:
        //     Constructs a new context instance using the existing connection to connect
        //     to a database. The connection will not be disposed when the context is disposed
        //     if contextOwnsConnection is false.
        //
        // Parameters:
        //   existingConnection:
        //     An existing connection to use for the new context.
        //
        //   contextOwnsConnection:
        //     If set to true the connection is disposed when the context is disposed, otherwise
        //     the caller must dispose the connection.
        public IdentityDbContextGuid(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection) { }
        //
        // Summary:
        //     Constructs a new context instance using the given string as the name or connection
        //     string for the database to which a connection will be made, and initializes
        //     it from the given model. See the class remarks for how this is used to create
        //     a connection.
        //
        // Parameters:
        //   nameOrConnectionString:
        //     Either the database name or a connection string.
        //
        //   model:
        //     The model that will back this context.
        public IdentityDbContextGuid(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model) { }
        //
        // Summary:
        //     Constructs a new context instance using the existing connection to connect
        //     to a database, and initializes it from the given model. The connection will
        //     not be disposed when the context is disposed if contextOwnsConnection is
        //     false.
        //
        // Parameters:
        //   existingConnection:
        //     An existing connection to use for the new context.
        //
        //   model:
        //     The model that will back this context.
        //
        //   contextOwnsConnection:
        //     Constructs a new context instance using the existing connection to connect
        //     to a database, and initializes it from the given model. The connection will
        //     not be disposed when the context is disposed if contextOwnsConnection is
        //     false.
        public IdentityDbContextGuid(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection) { }

        // Summary:
        //     If true validates that emails are unique
        public bool RequireUniqueEmail { get; set; }
        //
        // Summary:
        //     IDbSet of Roles
        public virtual IDbSet<TRole> Roles { get; set; }
        //
        // Summary:
        //     IDbSet of Users
        public virtual IDbSet<TUser> Users { get; set; }

        // Summary:
        //     Maps table names, and sets up relationships between the various user entities
        //
        // Parameters:
        //   modelBuilder:
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        //
        // Summary:
        //     Validates that UserNames are unique and case insenstive
        //
        // Parameters:
        //   entityEntry:
        //
        //   items:
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            return base.ValidateEntity(entityEntry, items);
        }

        

    }
}