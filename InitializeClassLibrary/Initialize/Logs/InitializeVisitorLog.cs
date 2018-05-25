using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeVisitorLog
    {
        private static VisitorLogDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeVisitorLog(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user = _user;
            dal = new VisitorLogDAL(_db, user);
        }

        private void Add()
        {
        }

        public void Initialize()
        {
        }

        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.VisitorLogs.Remove(item);
            }
            db.SaveChanges();


            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.VisitorLogs.Remove(item);
            }
            db.SaveChanges();
        }
    }
}