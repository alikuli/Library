
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS;

using UserModels.Models;


namespace DalLibrary.DalNS
{
    public class SalepointDAL : Repositry<Salepoint>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public SalepointDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
            Errors.ResetLibAndClass(GetSelfClassName());
        }



    }
}