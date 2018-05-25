
using Microsoft.AspNet.Identity;
using ModelsClassLibrary.ModelsNS;
using UserModels.Models;


namespace DalLibrary.DalNS
{
    public class WarehouseDAL : Repositry<Warehouse>
    {

        //private ApplicationDbContext _db;
        //private string _user;

        public WarehouseDAL(ApplicationDbContext db, IUser user)
            : base(db, user)
        {
        }



    }
}