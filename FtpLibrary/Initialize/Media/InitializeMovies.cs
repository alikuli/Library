using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.MediaNS;
using System;

namespace Bearer6.Initialize
{
    public class InitializeMovies
    {
        private static MovieDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeMovies(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new MovieDAL(_db, user);
        }

        private void Add(string theName)
        {
            Movie entity = new Movie();
            
            entity.Name = theName;
            entity.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(),DateTime.UtcNow.ToLongDateString());

            try
            {
                dal.Create(entity);
                dal.Save();
            }
            catch (AliKuli.Exceptions.NoDuplicateException)
            { }

            catch
            {
                throw;
            }
        }
        

        public void Initialize()
        {
            Add("Introduction To Company");
            Add("Introduction To Service");
            Add("What We can do for you");
        }
        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.Movies.Remove(item);
            }
            db.SaveChanges();
            
            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.Movies.Remove(item);
            }
            db.SaveChanges();
        }

    }
}