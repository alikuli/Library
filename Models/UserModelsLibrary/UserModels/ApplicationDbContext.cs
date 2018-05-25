using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserModelsLibrary.Models.Changed;

namespace UserModelsLibrary.Models
{
    public class ApplicationDbContext : IdentityDbContextGuid<User>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        //: base("DefaultConnection", throwIfV1Schema: false)
        { }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRoleGuid>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaimGuid>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserRoleGuid>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLoginGuid>().ToTable("UserLogins");

        }
    }
}