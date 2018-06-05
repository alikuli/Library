using Microsoft.AspNet.Identity.EntityFramework;
using UserModels;

namespace ApplicationDbContextNS
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
        //{
        //}

        public static ApplicationDbContext Create()
        {
            var dbx = new ApplicationDbContext();
            //dbx.Configuration.ProxyCreationEnabled = false;
            dbx.Configuration.LazyLoadingEnabled = true;
            return dbx;
        }
    }
}
