using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserModelsLibrary.Models.Changed
{
    public class IdentityDbContextGuid<TUser> : IdentityDbContext<TUser, IdentityRoleGuid, Guid, IdentityUserLoginGuid, IdentityUserRoleGuid, IdentityUserClaimGuid> where TUser : IdentityUserGuid
    {
        // Summary:
        //     Default constructor which uses the "DefaultConnection" connectionString
        public IdentityDbContextGuid() : base() { }
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
        public IdentityDbContextGuid(DbCompiledModel model)
            : base(model)
        {

        }

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

    }

}