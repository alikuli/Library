using Bearer.DAL;
using Bearer.Models;
using ModelsClassLibrary.Models.AddressNS;
using ModelsClassLibrary.Models.CommonAndShared;
using ModelsClassLibrary.Models.People;
using ModelsClassLibrary.Models.PlayersNS;
using System;
using System.Linq;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializePersonComment
    {
        private static PersonCommentDAL dal;
        private static ApplicationDbContext db;
        private string user;

        public InitializePersonComment(ApplicationDbContext _db, string _user)
        {
            db = _db;
            user=_user;
            dal = new PersonCommentDAL(_db, user);
        }

        private void Add(string fromFName, string fromMName, string fromLName, string toFName, string toMName, string toLName, string theComment, int rating)

        {

            //try
            //{
            //    //First find the person
            //    Person fromPerson = new PersonDAL(db, user).FindForName(fromFName, fromMName, fromLName).FirstOrDefault();
            //    Person toPerson = new PersonDAL(db, user).FindForName(toFName, toMName, toLName).FirstOrDefault();
                
            //    //if (person==null)
            //    //{
            //    //    throw new Exception("No person record found for the PersonComment");

            //    //}
            //    if (fromPerson != null && toPerson!=null)
            //    {
            //        PersonComment personComment = new PersonComment();
            //        personComment.FromPerson = fromPerson;
            //        personComment.FromPersonID = fromPerson.Id;

            //        personComment.ToPerson = toPerson;
            //        personComment.ToPersonID = toPerson.Id;

            //        personComment.Rating = rating;
            //        personComment.CommentForPerson = theComment;

            //        personComment.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());

            //        dal.Create(personComment);
            //        dal.Save();
            //    }
            //}
            //catch (AliKuli.Exceptions.MiscNS.NoDuplicateException)
            //{

            //}
            //catch
            //{
            //    throw;
            //}
        }


        public void DeleteAll()
        {
            var list = dal.FindAll();
            foreach (var item in list)
            {
                db.PersonComments.Remove(item);
            }
            db.SaveChanges();

            list = dal.FindAll(true);
            foreach (var item in list)
            {
                db.PersonComments.Remove(item);
            }
            db.SaveChanges();
        }

        public void Initialize()
        {
            try
            {
                Add("Ali", "Kuli", "Aminuddin", "Aila", "", "Azhar", "Ali Kuli is a good guy", 5);
                Add("Ali", "Kuli", "Aminuddin", "Aila", "", "Azhar", "I second that", 2);
                Add("Aila", "", "Azhar", "Ali", "Kuli", "Aminuddin", "Aila is a good person", 4);
                Add("Aila", "", "Azhar", "Ali", "Kuli", "Aminuddin", "Yes she is", 5);
                Add("Aila", "", "Azhar", "Ali", "Kuli", "Aminuddin", "Is a naughty guy", 3);
                Add("Aila", "", "Azhar", "Ali", "Kuli", "Aminuddin", "Yes he is", 1);
            }
            catch
            {
                throw;
            }

        }

    }
}