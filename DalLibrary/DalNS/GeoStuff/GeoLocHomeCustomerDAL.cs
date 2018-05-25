using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS.GeoLocationNS;
using UserModels.Models;

namespace DalLibrary.DalNS
{
    public class GeoLocationDAL : Repositry<GeoLocation>
    {

        public GeoLocationDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {

        }

    }
}
