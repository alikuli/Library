using System;
using DalLibrary.DalNS;
using DbContextLibrary.ModelsNS;
using ModelsClassLibrary.ModelsNS.MediaNS;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeMedia
    {
        private static MediaDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializeMedia(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user = _user;
            dal = new MediaDAL(_db, user);
        }

        private void Add(string theName)
        {
            UploadedFile entity = dal.Factory();

            entity.Name = theName;
            entity.MetaData.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());

            try
            {
                dal.Create(entity);
                dal.Save();
            }
            catch (ErrorHandlerLibrary.ExceptionsNS.NoDuplicateException)
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
                db.Medias.Remove(item);
            }
            db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.Medias.Remove(item);
            }
            db.SaveChanges();
        }

    }
}