using Bearer.DAL;
using Bearer.Models;
using Bearer6.DAL;
using ModelsClassLibrary.Models.AddressNS;
using ModelsClassLibrary.Models.CommonAndShared;
using ModelsClassLibrary.Models.CountryNS;
using ModelsClassLibrary.Models.People;
using ModelsClassLibrary.Models.People.PlayersNS;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InitializeClassLibrary.InitializeNS
{
    public class InitializeWorker
    {
        private static WorkerDAL _dal;
        private static ApplicationDbContext _db;
        private static string _user;

        //private static UserDAL userDAL;


        public InitializeWorker(ApplicationDbContext dbIn, string user)
        {
            _db = dbIn;
            _user = user;
            _dal = new WorkerDAL(_db, _user);


            //userDAL = new UserDAL(db); ;
        }

        private void Add
            (
            string identityCard,
            SonOfWifeOfDotOfENUM sonOfOrWifeOf,
            string nameOfFatherOrHusband,
            EducationLevelENUM educationLevel,
            BoardingENUM boardingStatus,
            string frontIdPictureUploadUrl,
            string backIdPictureUploadUrl,
            string frontFacePictureUploadUrl,
            string sideFacePictureUploadUrl)


        {
            Worker entity = _dal.Factory();

            entity.User = new UserDAL(_db, _user).FindForIdentityCard(identityCard);

            if (entity.User != null)
                entity.UserId = entity.User.Id;
            else
                throw new Exception("No User record found for the Owner");
            
            entity.Comment = string.Format("Added thru Initialization on {0} dated {1}", DateTime.UtcNow.ToLongTimeString(), DateTime.UtcNow.ToLongDateString());


            try
            {
                _dal.Create(entity);
                _dal.Save();
            }
            catch (AliKuli.ExceptionsNS.NoDuplicateException)
            {

            }
            catch (AliKuli.ExceptionsNS.UserExistsException)
            {

            }
            catch
            {
                throw;
            }
        }

        public void DeleteAll()
        {
            var list =  _dal.FindAll().ToList();
            
            foreach (var item in list)
            {
                _db.Workers.Remove(item);
            }
            _db.SaveChanges();

            list = _dal.FindAll(true).ToList();
            foreach (var item in list)
            {
                _db.Workers.Remove(item);
            }
            _db.SaveChanges();
        }


        public void Initialize()
        {

            string frontIdPictureUploadUrl ="";
            string backIdPictureUploadUrl= "";
            string frontFacePictureUploadUrl ="";
            string sideFacePictureUploadUrl = "";

            Add("1234567890120", SonOfWifeOfDotOfENUM.SonOf,"Parvez Amin",EducationLevelENUM.University, BoardingENUM.Yes, frontIdPictureUploadUrl, backIdPictureUploadUrl,frontFacePictureUploadUrl,sideFacePictureUploadUrl);

        }

    }
}