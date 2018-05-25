//using System.Data.Entity;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
//using TestUserToGuid.Models.Changed;

//namespace TestUserToGuid.Models
//{
//    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
//    public class User : IdentityUserGuid
//    {
//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManagerGuid<User> manager)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//            // Add custom user claims here
//            return userIdentity;
//        }
//    }

//    public class ApplicationDbContext : IdentityDbContextGuid<User>
//    {
//        public ApplicationDbContext()
//            : base("DefaultConnection")
//        //: base("DefaultConnection", throwIfV1Schema: false)
//        { }


//        public static ApplicationDbContext Create()
//        {
//            return new ApplicationDbContext();
//        }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//            modelBuilder.Entity<User>().ToTable("Users");
//            modelBuilder.Entity<IdentityRoleGuid>().ToTable("Roles");
//            modelBuilder.Entity<IdentityUserClaimGuid>().ToTable("UserClaims");
//            modelBuilder.Entity<IdentityUserRoleGuid>().ToTable("UserRoles");
//            modelBuilder.Entity<IdentityUserLoginGuid>().ToTable("UserLogins");

//        }
//    }
//}